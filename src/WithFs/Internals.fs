namespace With.Internals
open With
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
    static member Create(p)=FieldOrProperty (Choice1Of2 p)
    static member Create(p)=FieldOrProperty (Choice2Of2 p)

    member this.Name =
        let getName = fun (FieldOrProperty c)->
                    match c with
                    | Choice1Of2 p -> p.Name
                    | Choice2Of2 f -> f.Name
        getName this 
    member this.Value(t:obj)=
        let getValue = fun (FieldOrProperty c) t ->
                        match c with
                        | Choice1Of2 p -> p.GetValue(t, null)
                        | Choice2Of2 f -> f.GetValue(t)
        getValue this t

[<Extension>]
type public FSharpFuncUtil = 
    [<Extension>] 
    static member ToFSharpFunc<'a,'b> (func:System.Func<'a,'b>) = fun x -> func.Invoke(x)
    [<Extension>] 
    static member ToFSharpFunc<'a,'b,'c> (func:System.Func<'a,'b,'c>) = fun x y -> func.Invoke(x,y)
    [<Extension>] 
    static member ToFSharpFunc<'a,'b,'c,'d> (func:System.Func<'a,'b,'c,'d>) = fun x y z -> func.Invoke(x,y,z)

module Reflection = 
   open System.Collections.Generic
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
