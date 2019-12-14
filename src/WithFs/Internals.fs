namespace With.Internals

open With
open With.Collections
open System
open System.Runtime.CompilerServices
open System.Linq.Expressions
open System.Reflection

/// Used internally to represent field or property
type FieldOrProperty =
    | FieldOrProperty of Choice<PropertyInfo, FieldInfo>

    [<CompiledName("Create")>]
    static member create (p) = FieldOrProperty(Choice1Of2 p)

    [<CompiledName("Create")>]
    static member create (p) = FieldOrProperty(Choice2Of2 p)

    [<CompiledName("_Name")>]
    static member name (FieldOrProperty v) =
        match v with
        | Choice1Of2 p -> p.Name
        | Choice2Of2 f -> f.Name

    [<CompiledName("_DeclaringType")>]
    static member declaringType (FieldOrProperty v) =
        match v with
        | Choice1Of2 p -> p.DeclaringType
        | Choice2Of2 f -> f.DeclaringType

    [<CompiledName("_FieldType")>]
    static member fieldType (FieldOrProperty v) =
        match v with
        | Choice1Of2 p -> p.PropertyType
        | Choice2Of2 f -> f.FieldType

    member this.Name = FieldOrProperty.name this
    member this.DeclaringType = FieldOrProperty.declaringType this


module Reflection =

    [<CompiledName("WeakMemoize")>]
    let weakMemoize (construct) =
        let table = ConditionalWeakTable<'TKey, 'TValue>()
        fun (key: 'TKey) ->
            lock table (fun () ->
                match table.TryGetValue key with
                | true, value -> value
                | false, _ ->
                    let value = construct (key)
                    table.Add(key, value)
                    value)

    [<CompiledName("GetConstructorWithMostParameters")>]
    let getConstructorWithMostParameters: Type -> ConstructorInfo =
        let create (tp: Type) =
            let ctors = tp.GetTypeInfo().GetConstructors()
            match ctors.Length with
            | 1 -> ctors.[0]
            | _ -> ctors |> Array.maxBy (fun ctor -> ctor.GetParameters().Length)
        weakMemoize create

    let getPublicFields (typ: Type) = typ.GetTypeInfo().GetFields(BindingFlags.Public ||| BindingFlags.Instance)
    let getPublicProperties (typ: Type) = typ.GetTypeInfo().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)

    let fieldsOrProperties =
        let ctor (typ:Type) =
            seq {
                yield! getPublicFields (typ) |> Array.map FieldOrProperty.create
                yield! getPublicProperties (typ) |> Array.map FieldOrProperty.create
            }
        weakMemoize (ctor)


module InternalExpressions=
    module internal T=
        open System.Collections.Generic
        open System.Collections.ObjectModel
        /// is assignable to type (where type is generic with 1 type parameter)
        let isAssignableToT (genT:Type) (t:TypeInfo) =
            t.IsGenericType && genT.GetTypeInfo().GetGenericArguments().Length = t.GetGenericArguments().Length &&
                genT.MakeGenericType(t.GetGenericArguments()).GetTypeInfo().IsAssignableFrom( t )
        let eqToT (genT:Type) (t:TypeInfo)=
            t.IsGenericType && genT.GetTypeInfo().GetGenericArguments().Length = t.GetGenericArguments().Length &&
                genT.MakeGenericType(t.GetGenericArguments()).GetTypeInfo().Equals( t )
        module KV=
            let typ = typedefof<KeyValuePair<_,_>>
            let keyExprT t expr=
                let gtyp = typ.MakeGenericType(t).GetTypeInfo()
                Expression.Property(expr,gtyp.GetProperty("Key"))
            let valueExprT t expr=
                let gtyp = typ.MakeGenericType(t).GetTypeInfo()
                Expression.Property(expr,gtyp.GetProperty("Value"))

        module Enumerable=
            let ityp = typedefof<seq<_>>
            let toList = typeof<Linq.Enumerable>.GetTypeInfo().GetMethod("ToList")
            let toDic = typeof<Linq.Enumerable>.GetTypeInfo().GetMethods()
                        |> Array.find (fun m->
                                            let prms = m.GetParameters()
                                            m.Name = "ToDictionary"
                                                && prms.Length=3
                                                && prms.[1].Name="keySelector"
                                                && prms.[2].Name="elementSelector"
                                      )
            let tryFindSeqT (sourceT:TypeInfo)=
                          sourceT.GetInterfaces()
                          |> Array.tryFind (fun t-> t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() = ityp)

            let exprToListT (t:Type) parameterValue=Expression.Call(toList.MakeGenericMethod(t), [|parameterValue|]) :> Expression
            let exprToDicT (t:Type array) parameterValue=
                let parameterT = Expression.Parameter(KV.typ.MakeGenericType t,"kv")
                let toKey =
                    let key=KV.keyExprT t parameterT
                    Expression.Lambda(key,parameterT) :>Expression
                let toValue =
                    let value=KV.valueExprT t parameterT
                    Expression.Lambda(value,parameterT) :>Expression
                let tSource = KV.typ.MakeGenericType(t)
                Expression.Call(toDic.MakeGenericMethod(Array.append [|tSource|] t), [|parameterValue; toKey; toValue |]) :> Expression

        module ReadOnlyCollection=
            let typ = typedefof<ReadOnlyCollection<_>>
            let ityp = typedefof<IReadOnlyCollection<_>>
            let ctorT (t:Type) = typ.MakeGenericType(t).GetTypeInfo().GetConstructors() |> Seq.find (fun c->c.GetParameters().Length =1)
        module List=
            let typ = typedefof<List<_>>
            let ityp = typedefof<IList<_>>
        module Dic=
            let typ = typedefof<Dictionary<_,_>>
            let ityp = typedefof<IDictionary<_,_>>
        module ReadOnlyDic=
            let typ = typedefof<ReadOnlyDictionary<_,_>>
            let ityp = typedefof<IReadOnlyDictionary<_,_>>
            let ctorT (t:Type) = typ.MakeGenericType(t).GetTypeInfo().GetConstructors() |> Seq.find (fun c->c.GetParameters().Length =1)

    let internal fieldOrPropertyToSetT (tSource: Type) (tDest: Type) (value: FieldOrProperty) =
        /// return value of Name property of an entity
        let inline getName(r:^a) = ( ^a : ( member get_Name: unit->String ) (r) )
        /// compare two named things and make sure that they have the same Name (ignoring case)
        let inline haveSameName p1 p2 =
            let p1Name = getName p1
            let p2Name = getName p2
            p1Name.Equals(p2Name, StringComparison.CurrentCultureIgnoreCase)
        /// fun (s:seq) -> s.ToList()
        let trySeqToList listT (sourceT:TypeInfo) (destT:TypeInfo) (expr:Expression) : Expression option=
            if T.isAssignableToT T.Enumerable.ityp sourceT && T.eqToT listT destT then
                let t=destT.GenericTypeArguments |> Array.head
                T.Enumerable.exprToListT t expr |> Some
            else None
        /// fun (l:ResizeArray) -> ReadOnlyCollection(l)
        let tryListToReadonlyCollection readOnlyT (sourceT:TypeInfo) (destT:TypeInfo) (expr:Expression) : Expression option=
            if T.isAssignableToT T.List.typ sourceT && T.eqToT readOnlyT destT then
                let t=destT.GenericTypeArguments |> Array.head
                let ctor=T.ReadOnlyCollection.ctorT t
                Expression.New(ctor, [| expr |]) :> Expression |> Some
            else None
        /// fun (s:seq) -> ReadOnlyCollection(s.ToList())
        let trySeqToReadonlyCollection readOnlyT (sourceT:TypeInfo) (destT:TypeInfo) (expr:Expression) : Expression option=
            if T.isAssignableToT T.Enumerable.ityp sourceT && T.eqToT readOnlyT destT then
                let t=destT.GenericTypeArguments |> Array.head
                let ctor=T.ReadOnlyCollection.ctorT t
                Expression.New(ctor, [| T.Enumerable.exprToListT t expr |]) :> Expression |> Some
            else None
        /// fun (s:Dictionary) -> ReadOnlyDictionary(s)
        let tryDicToReadonlyDic readOnlyT (sourceT:TypeInfo) (destT:TypeInfo) (expr:Expression) : Expression option=
            if T.isAssignableToT T.Dic.ityp sourceT && T.eqToT readOnlyT destT then
                let t=destT.GenericTypeArguments |> Array.head
                let ctor=T.ReadOnlyDic.ctorT t
                Expression.New(ctor, [| expr |]) :> Expression |> Some
            else None
        /// fun (s:seq<KeyValuePair>) -> s.ToDictionary(fun kv->kv.Key,fun kv->kv.Value)
        let trySeqKVToDic dicT (sourceT:TypeInfo) (destT:TypeInfo) (expr:Expression) : Expression option=
            let seqT = T.Enumerable.tryFindSeqT sourceT
            match T.eqToT dicT destT, seqT with
            | true, Some s->
                let seqTArg=s.GetTypeInfo().GetGenericArguments() |> Array.head
                if T.eqToT T.KV.typ (seqTArg.GetTypeInfo()) then
                    let kvTArgs = seqTArg.GetTypeInfo().GetGenericArguments()
                    T.Enumerable.exprToDicT kvTArgs expr |> Some
                else None
            | _,_-> None
        /// fun (s:'A) -> s :> 'B
        let tryConvertAssignable (sourceT:TypeInfo) (destT:TypeInfo) (expr:Expression) : Expression option=
            if destT.IsAssignableFrom(sourceT) then
                Expression.Convert(expr, destT.AsType()) :> Expression |> Some
            else None
        /// fun (s:'A) -> s
        let tryNoConvert (sourceT:TypeInfo) (destT:TypeInfo) (expr:Expression) : Expression option=
            if destT.Equals(sourceT) then
                expr |> Some
            else None
        let tryChangeTypeFromTo (sourceT:TypeInfo) (destT:TypeInfo) (parameterValue:#Expression) =
                let tryOut = [tryNoConvert; tryConvertAssignable;
                    tryDicToReadonlyDic T.ReadOnlyDic.ityp; tryDicToReadonlyDic T.ReadOnlyDic.typ;
                    trySeqKVToDic T.Dic.ityp; trySeqKVToDic T.Dic.typ;
                    tryListToReadonlyCollection T.ReadOnlyCollection.typ; tryListToReadonlyCollection T.ReadOnlyCollection.ityp;
                    trySeqToReadonlyCollection T.ReadOnlyCollection.typ; trySeqToReadonlyCollection T.ReadOnlyCollection.ityp;
                    trySeqToList T.List.typ; trySeqToList T.List.ityp]
                tryOut |> List.tryPick (fun f->f sourceT destT parameterValue)
        let props = Reflection.fieldsOrProperties tSource |> Seq.toArray
        let ctor = Reflection.getConstructorWithMostParameters tDest
        let parameterValue=Expression.Parameter(FieldOrProperty.fieldType value,"v")
        let parameterT = Expression.Parameter(tSource,"t")
        let inline noKnwownConversion sourceT destT p=failwithf "No known conversion from %A to %A, please make sure that property named %s has a type that fits the constructor argument with same name" sourceT destT (getName p)

        let mutable usedValue=false
        let mapParamToExpressionParam (param:ParameterInfo) : Expression =
            if haveSameName param value then
                usedValue <- true
                let sourceT = FieldOrProperty.fieldType value
                let destT = param.ParameterType
                match parameterValue |> tryChangeTypeFromTo ( sourceT.GetTypeInfo() ) ( destT.GetTypeInfo() ) with
                | Some v -> v
                | None-> noKnwownConversion sourceT destT param
            else
                match props |> Array.tryFind (fun p ->haveSameName p param) with
                | Some p ->
                    let sourceT = FieldOrProperty.fieldType p
                    let destT = param.ParameterType
                    let propertyValue = Expression.PropertyOrField(parameterT,p.Name) :> Expression
                    match propertyValue |> tryChangeTypeFromTo ( sourceT.GetTypeInfo() ) ( destT.GetTypeInfo() ) with
                    | Some v -> v
                    | None-> noKnwownConversion sourceT destT param
                | None -> raise (MissingValueException param.Name)

        let parameters : Expression list =
            ctor.GetParameters() |> Array.map mapParamToExpressionParam |> Array.toList
        if not usedValue then raise (MissingConstructorParameterException value.Name)
        let expressions : Expression list = [Expression.New(ctor, parameters)]
        (Expression.Block(expressions),[parameterValue;parameterT])
    [<CompiledName("FieldOrPropertyToSetUntyped")>]
    let fieldOrPropertyToSetUntyped (tSource: Type) (tDest: Type) (value: FieldOrProperty) =
        let (block,parameters)= fieldOrPropertyToSetT tSource tDest value
        Expression.Lambda(block, parameters)

    [<CompiledName("FieldOrPropertyToSet")>]
    let fieldOrPropertyToSet<'T,'V> (tSource: Type) (tDest: Type) (value: FieldOrProperty) =
        let (block,parameters)= fieldOrPropertyToSetT tSource tDest value
        Expression.Lambda<Func<'V,'T,'T>>(block, parameters)
    let internal fieldOrPropertyToGetT (tSource: Type) (value: FieldOrProperty) =
        let parameterT = Expression.Parameter(tSource, "t")
        let expressions = [Expression.PropertyOrField(parameterT,value.Name) :> Expression]
        (Expression.Block(expressions),[parameterT])

    [<CompiledName("FieldOrPropertyToGetUntyped")>]
    let fieldOrPropertyToGetUntyped (tSource: Type) (value: FieldOrProperty) =
        let (block,parameters)=fieldOrPropertyToGetT tSource value
        Expression.Lambda(block,parameters)

    [<CompiledName("FieldOrPropertyToGet")>]
    let fieldOrPropertyToGet<'T,'V> (value: FieldOrProperty) =
        let (block,parameters)=fieldOrPropertyToGetT typeof<'T> value
        Expression.Lambda<Func<'T,'V>>(block,parameters)
