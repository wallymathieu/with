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
        let ctor typ =
            seq {
                yield! getPublicFields (typ) |> Array.map FieldOrProperty.create
                yield! getPublicProperties (typ) |> Array.map FieldOrProperty.create
            }
        weakMemoize (ctor)


module InternalExpressions=
    let internal fieldOrPropertyToSetT (tSource: Type) (tDest: Type) (value: FieldOrProperty) =
        let props = Reflection.fieldsOrProperties tSource |> Seq.toArray
        let ctor = Reflection.getConstructorWithMostParameters tDest
        let valueType =FieldOrProperty.fieldType value
        let parameterValue=Expression.Parameter(valueType,"v")
        let parameterT = Expression.Parameter(tSource,"t")
        let mapParamToExpressionParam (param:ParameterInfo) : Expression =
            match value.Name.Equals(param.Name, StringComparison.CurrentCultureIgnoreCase) with
            | true -> //coerce v (param.ParameterType)
                parameterValue :> Expression
            | false ->
                match props |> Array.tryFind (fun p -> p.Name.Equals(param.Name, StringComparison.OrdinalIgnoreCase)) with
                | Some p -> 
                    Expression.PropertyOrField(parameterT,p.Name) :> Expression
                | None -> raise (MissingValueException param.Name)

        let parameters : Expression list = 
            ctor.GetParameters() |> Array.map mapParamToExpressionParam |> Array.toList
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
