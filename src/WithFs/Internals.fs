namespace With.Internals
open With
open With.Collections
open System
open System.Runtime.CompilerServices
open System.Linq.Expressions
open System.Collections.Generic
open System.Collections
open System.Collections.ObjectModel
open System.Linq
open System.Reflection
    
/// Used internally to represent field or property
type FieldOrProperty=FieldOrProperty of Choice<PropertyInfo,FieldInfo>
with
    [<CompiledName("Create")>]
    static member create(p)=FieldOrProperty (Choice1Of2 p)
    [<CompiledName("Create")>]
    static member create(p)=FieldOrProperty (Choice2Of2 p)
    [<CompiledName("Unwrap")>]
    static member unwrap(FieldOrProperty p)= p
    
    [<CompiledName("_Name")>]
    static member name(FieldOrProperty v)= 
        match v with
        | Choice1Of2 p -> p.Name
        | Choice2Of2 f -> f.Name
    [<CompiledName("_Value")>]
    static member value (FieldOrProperty v) (t:obj)= 
        match v with
        | Choice1Of2 p -> p.GetValue(t, null)
        | Choice2Of2 f -> f.GetValue(t)
    [<CompiledName("_SetValue")>]
    static member setValue (FieldOrProperty v) (t:obj) value= 
        match v with
        | Choice1Of2 p -> p.SetValue (t, value)
        | Choice2Of2 f -> f.SetValue (t, value)
        
    [<CompiledName("_DeclaringType")>]
    static member declaringType(FieldOrProperty v)= 
        match v with
        | Choice1Of2 p -> p.DeclaringType
        | Choice2Of2 f -> f.DeclaringType
    [<CompiledName("_FieldType")>]
    static member fieldType(FieldOrProperty v)= 
        match v with
        | Choice1Of2 p -> p.PropertyType
        | Choice2Of2 f -> f.FieldType
    member this.Name = FieldOrProperty.name this
    member this.Value(t:obj)=FieldOrProperty.value this t
    member this.SetValue(t:obj,v)=FieldOrProperty.setValue this t v
    member this.DeclaringType=FieldOrProperty.declaringType this


module Reflection = 
   open System.Collections.Generic
   open System.Collections.ObjectModel
   [<CompiledName("WeakMemoize")>]
   let weakMemoize (construct) =
        let table =ConditionalWeakTable<'TKey, 'TValue>()
        fun (key:'TKey) ->
            lock table (fun ()->
                match table.TryGetValue key with
                | true, value->value
                | false, _ ->
                    let value = construct(key)
                    table.Add (key, value)
                    value
                )
   [<CompiledName("IsDictionaryType")>]
   let isDictionaryType (t:Type)=
       t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() = typeof<IDictionary<_,_>>
   [<CompiledName("IsEnumerableType")>]        
   let isEnumerableType (t:Type)=
       t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() = typeof<IEnumerable<_>>

   [<CompiledName("GetIDictionaryTypeParameters")>]
   let getIDictionaryTypeParameters=
       let dic = ConditionalWeakTable<Type, Type[]>()
       let create (tp:Type)=
           let t = seq{
                        yield tp
                        yield! (tp.GetTypeInfo().GetInterfaces()) 
                    } |> Seq.tryFind (isDictionaryType)
           match t with 
            | Some t'->t'.GetTypeInfo().GetGenericArguments()
            | _ -> Array.empty
       weakMemoize create 
        
   [<CompiledName("GetIEnumerableTypeParameter")>]
   let getIEnumerableTypeParameter =
       let create (tp:Type)=
           let t = seq{
                        yield tp
                        yield! (tp.GetTypeInfo().GetInterfaces()) 
                    } |> Seq.tryFind (isEnumerableType)
           match t with 
            | Some t'->t'.GetTypeInfo().GetGenericArguments()
            | _ -> Array.empty
       weakMemoize create
   [<CompiledName("GetConstructorWithMostParameters")>]
   let getConstructorWithMostParameters : (Type->ConstructorInfo) =
       let create (tp:Type)=
            let ctors = tp.GetTypeInfo().GetConstructors()
            match ctors.Length with
            | 1 -> ctors.[0]
            | _ -> ctors |> Array.maxBy (fun ctor->ctor.GetParameters().Length)
       weakMemoize create

   [<CompiledName("DictionaryTypeCtor")>]
   let dictionaryTypeCtor = 
       let create t= 
            typeof<Dictionary<_,_>>.MakeGenericType(getIDictionaryTypeParameters t)
                                   .GetTypeInfo()
                                   .GetConstructor(Array.empty)
       weakMemoize create 
   [<CompiledName("ReadOnlyDictionaryTypeCtor")>]
   let readOnlyDictionaryTypeCtor = 
       let create t= 
            typeof<ReadOnlyDictionary<_,_>>.MakeGenericType(getIDictionaryTypeParameters t)
                                   .GetTypeInfo()
                                   .GetConstructor(Array.empty)
       weakMemoize create
   [<CompiledName("ReadOnlyCollectionTypeCtor")>]
   let readOnlyCollectionTypeCtor : (Type->ConstructorInfo) = 
       let create t= 
            typeof<ReadOnlyCollection<_>>.MakeGenericType(getIEnumerableTypeParameter t)
                                   .GetTypeInfo()
                                   .GetConstructor(Array.empty)
       weakMemoize create

   let private enumerableToList = typeof<Enumerable>.GetTypeInfo().GetMethod("ToList")
   [<CompiledName("ToListOfTypeT")>]
   let toListOfTypeT (that:IEnumerable)=
       let typ = that.GetType()
       enumerableToList.MakeGenericMethod(typ).Invoke(null, [| that |]) :?> IList
   [<CompiledName("ToDictionaryOfTypeT")>]
   let toDictionaryOfTypeT (that:IDictionary)=
       let typ = that.GetType()
       let dic = (dictionaryTypeCtor typ).Invoke(Array.empty)
       let dictionary =  dic :?> IDictionary
       for item in that.Keys do
           dictionary.[item] <- that.[item]
       dictionary
   let getPublicFields (typ:Type)=
           typ.GetTypeInfo().GetFields (BindingFlags.Public ||| BindingFlags.Instance)
   let getPublicProperties (typ:Type)=
           typ.GetTypeInfo().GetProperties (BindingFlags.Public ||| BindingFlags.Instance)
           
   let fieldsOrProperties =
       let ctor typ = seq{
           yield! getPublicFields(typ) |> Array.map FieldOrProperty.create
           yield! getPublicProperties(typ) |> Array.map FieldOrProperty.create
       }
       weakMemoize(ctor)
   let enumerableCast = typeof<Enumerable>.GetTypeInfo().GetMethod ("Cast")
   let coerce (v:obj) (parameterType:Type)=
       if not <| isNull v 
          && typeof<IEnumerable>.GetTypeInfo().IsAssignableFrom (parameterType)
          && not <| parameterType.GetTypeInfo().IsAssignableFrom(v.GetType ())
       then
           let typeParam = getIEnumerableTypeParameter parameterType
           if not <| isNull typeParam
              && not <| parameterType.GetTypeInfo().IsAssignableFrom (v.GetType()) 
           then 
               enumerableCast.MakeGenericMethod(typeParam).Invoke(null, [| v |])
           else
               failwithf "parameter type %s is not assignable from %s" parameterType.Name (v.GetType().Name)
       else
           v
     
   let internal getConstructorParameterValues (t:obj) (specifiedValues:IReadOnlyDictionary<string, obj>) (props: FieldOrProperty array) (ctor:ConstructorInfo)=
       let ctorParams = ctor.GetParameters()
       let map (param:ParameterInfo) =
          match specifiedValues.TryGetValue(param.Name) with
          | true, v ->
              coerce v (param.ParameterType)
          | false, _ ->
              match props |> Array.tryFind (fun p->p.Name.Equals(param.Name, StringComparison.OrdinalIgnoreCase)) with
              | Some p ->
                  coerce (p.Value t) (param.ParameterType)
              | None ->
                  raise (MissingValueException param.Name)

       ctorParams |> Array.map map

   [<CompiledName("FSharpCreate")>]
   let create (tSource:Type) (tDest:Type) (parent:obj) (values: NameAndValue seq) =
        let props = fieldsOrProperties tSource |> Seq.toArray
        let ctor = getConstructorWithMostParameters tDest
        let usedKeys = ResizeArray()
        let dictionaryOfParameters = 
            Collections.readOnlyDictionaryUsage<string, obj>(values.ToDictionary ((fun nameAndValue -> nameAndValue.Key),
                                                                                  (fun nameAndValue -> nameAndValue.Value),
                                                                                  StringComparer.CurrentCultureIgnoreCase), Action<_,_>( fun key value-> usedKeys.Add(key)))
        let ctorValues = getConstructorParameterValues parent dictionaryOfParameters props ctor
        let instance = ctor.Invoke ctorValues
        let unusedKeys = dictionaryOfParameters.Keys.Except(usedKeys, StringComparer.CurrentCultureIgnoreCase).ToArray()
        if unusedKeys.Any() then
            failwithf "Missing constructor parameters on '%s' for: [%s]" tDest.Name (String.Join(",", unusedKeys))
        else
            instance
type CreateInstanceFromValues=
   static member Create (tSource,tDest,parent,values)=Reflection.create tSource tDest parent values
   static member Create<'TDestination> (parent,values) :'TDestination = 
        Reflection.create (parent.GetType()) typeof<'TDestination> parent values :?> 'TDestination
   static member Create<'TSource,'TDestination> (parent:'TSource,values) :'TDestination = 
        Reflection.create typeof<'TSource> typeof<'TDestination> parent values :?> 'TDestination

