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
    member this.Name = FieldOrProperty.name this
    member this.Value(t:obj)=FieldOrProperty.value this t
    member this.SetValue(t:obj,v)=FieldOrProperty.setValue this t v
    member this.DeclaringType=FieldOrProperty.declaringType this
        
module FieldOrPropertyMap=
    let withValues (values:obj array) (map: Map<int,FieldOrProperty>) : NameAndValue array=
       values |> Array.mapi (fun i v-> let fieldOrProperty= map |> Map.find i
                                       NameAndValue(fieldOrProperty.Name, v))


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

///Copy of Lens definition from FSharpx.Extras : 
// https://github.com/fsprojects/FSharpx.Extras/blob/master/src/FSharpx.Extras/Lens.fs
type DataLens<'T,'U>={ Get: 'T -> 'U
                       Set: 'U -> 'T -> 'T } 
                     member l.Update f a = l.Set (f (l.Get a)) a
module DataLens =
   let inline get a (l: DataLens<_,_>) = l.Get a
   let inline set v a (l: DataLens<_,_>) = l.Set v a
   let inline update f (l: DataLens<_,_>) = l.Update f
   let inline combine (l1: DataLens<'t,'v1>) (l2: DataLens<'t,'v2>) = 
        { Get = fun t->(l1.Get t, l2.Get t)
          Set = fun (v1,v2) t ->  l2.Set v2 (l1.Set v1 t) }
   /// Sequentially composes two lenses
   let inline compose (l1: DataLens<_,_>) (l2: DataLens<_,_>) = 
                  { Get = l2.Get >> l1.Get
                    Set = l1.Set >> l2.Update }
   let fieldOrPropertyToLens<'T,'U> v : DataLens<'T,'U>=
       let typ = FieldOrProperty.declaringType v
       let n = FieldOrProperty.name v
       { Get = fun t-> FieldOrProperty.value v t :?> 'U
         Set = fun v t -> Reflection.create typ typ t [NameAndValue(n,v)] :?> 'T }

   let fieldOrPropertiesToLens (fields:FieldOrProperty list)=
       match fields |> List.tryHead with
       | Some field ->
           let typ =FieldOrProperty.declaringType field
           let names =fields |> List.map FieldOrProperty.name  
           { Get= fun t-> fields |> List.map (fun f-> f.Value t) 
             Set = fun v t -> Reflection.create typ typ t (List.zip names v |> List.map NameAndValue) }
       | None ->
           { Get= fun t-> [] 
             Set = fun _ t -> t }
   
   //let combine = DataLensBuilder()
module ApplyOperation=

    open Reflection
    let private doReplace (memberValue:obj) (paramValues:obj array) :obj= 
        match paramValues.Length with
        | 2 -> 
            let d= toDictionaryOfTypeT (memberValue :?> IDictionary)
            d.[paramValues.[0]] <- paramValues.[1]
            let ctor=readOnlyDictionaryTypeCtor (memberValue.GetType())
            ctor.Invoke([| d |])
        | _ -> raise (NotImplementedException())
    let private doAdd (memberValue:obj) (paramValues:obj array) :obj= 
            match paramValues.Length with
            | 1 -> 
                let d= toListOfTypeT (memberValue :?> IEnumerable)
                d.Add paramValues.[0] |> ignore
                let ctor = readOnlyCollectionTypeCtor (memberValue.GetType())
                ctor.Invoke([| d |])
            | 2 ->
                let d= toDictionaryOfTypeT (memberValue :?> IDictionary)
                d.Add(paramValues.[0], paramValues.[1])
                let ctor=readOnlyDictionaryTypeCtor (memberValue.GetType())
                ctor.Invoke([| d |])
            | _ -> raise (NotImplementedException())
    let private doAddRange (memberValue:obj) (paramValues:obj array) :obj= 
            let d= toListOfTypeT (memberValue :?> IEnumerable)
            for item in (paramValues.[0] :?> IEnumerable) do
                d.Add item |> ignore
            let ctor = readOnlyCollectionTypeCtor (memberValue.GetType())
            ctor.Invoke([| d |])
    let private doRemove (memberValue:obj) (paramValues:obj array) :obj= 
            match paramValues.Length with
            | 1 -> 
                match memberValue with
                | :? IDictionary as dictionary->
                    let d= toDictionaryOfTypeT (memberValue :?> IDictionary)
                    d.Remove paramValues.[0] 
                    let ctor = readOnlyDictionaryTypeCtor (memberValue.GetType())
                    ctor.Invoke([| d |])
                | :? IEnumerable as enumerable-> 
                    let d= toListOfTypeT (memberValue :?> IEnumerable)
                    d.Remove paramValues.[0] 
                    let ctor = readOnlyCollectionTypeCtor (memberValue.GetType())
                    ctor.Invoke([| d |])
                | _ ->raise (NotImplementedException())
            | _ -> raise (NotImplementedException())
   
    let private operations : Map<string,(obj->obj array->obj)> = 
                     Map.ofList [
                        ("Add", doAdd)
                        ("AddRange", doAddRange)
                        ("Remove", doRemove)
                        ("Replace", doReplace) 
                     ]
    [<CompiledName("Apply")>]
    let apply (expr:MethodCallExpression , memberValue:obj, paramValues:obj array)=
        let name = expr.Method.Name
        match Map.tryFind name operations with
        | Some op->op memberValue paramValues
        | None ->raise (ExpectedButGotException (operations |> Map.toArray |> Array.map fst, name))
