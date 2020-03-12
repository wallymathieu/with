namespace With.Internals

open With
open System
open System.Runtime.CompilerServices
open System.Linq.Expressions
open System.Reflection
open With.Lenses

/// Used internally to represent field or property
type internal FieldOrProperty =
    | FieldOrProperty of Choice<PropertyInfo, FieldInfo>

    static member create (p) = FieldOrProperty(Choice1Of2 p)

    static member create (p) = FieldOrProperty(Choice2Of2 p)

    static member name (FieldOrProperty v) =
        match v with
        | Choice1Of2 p -> p.Name
        | Choice2Of2 f -> f.Name

    static member declaringType (FieldOrProperty v) =
        match v with
        | Choice1Of2 p -> p.DeclaringType
        | Choice2Of2 f -> f.DeclaringType

    static member fieldType (FieldOrProperty v) =
        match v with
        | Choice1Of2 p -> p.PropertyType
        | Choice2Of2 f -> f.FieldType

    member this.Name = FieldOrProperty.name this
    member this.DeclaringType = FieldOrProperty.declaringType this


module internal Reflection =

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


module internal InternalExpressions=
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

    let internal fieldOrPropertyToSetT (options:DataLensOptions) (tSource: Type) (tDest: Type) (value: FieldOrProperty) =
        /// compare parameter and field or property and make sure that they have the same Name (ignoring case)
        let haveSameName (p1:ParameterInfo) (p2:FieldOrProperty) =
            let p1Name = p1.Name
            let p2Name = p2.Name
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
        let noKnwownConversion sourceT destT (p:ParameterInfo)=failwithf "No known conversion from %A to %A, please make sure that property named %s has a type that fits the constructor argument with same name" sourceT destT (p.Name)

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
                match props |> Array.tryFind (fun p ->haveSameName param p) with
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
    let fieldOrPropertyToSetUntyped opt (tSource: Type) (tDest: Type) (value: FieldOrProperty) =
        let (block,parameters)= fieldOrPropertyToSetT opt tSource tDest value
        Expression.Lambda(block, parameters)

    let fieldOrPropertyToSet<'T,'V> opt (tSource: Type) (tDest: Type) (value: FieldOrProperty) =
        let (block,parameters)= fieldOrPropertyToSetT opt tSource tDest value
        Expression.Lambda<Func<'V,'T,'T>>(block, parameters)
    let fieldOrPropertyToGetT (tSource: Type) (value: FieldOrProperty) =
        let parameterT = Expression.Parameter(tSource, "t")
        let expressions = [Expression.PropertyOrField(parameterT,value.Name) :> Expression]
        (Expression.Block(expressions),[parameterT])

    let fieldOrPropertyToGetUntyped (tSource: Type) (value: FieldOrProperty) =
        let (block,parameters)=fieldOrPropertyToGetT tSource value
        Expression.Lambda(block,parameters)

    let fieldOrPropertyToGet<'T,'V> (value: FieldOrProperty) =
        let (block,parameters)=fieldOrPropertyToGetT typeof<'T> value
        Expression.Lambda<Func<'T,'V>>(block,parameters)

module internal FieldOrProperty=
    /// Given field or property access, return lens implemented through reflection and expression compile
    let toLens<'T, 'U> opt v: DataLens<'T, 'U> =
        let typ = typeof<'T>
        let compiledSetter =
            let lens = InternalExpressions.fieldOrPropertyToSet<'T,'U> opt typ typ v
            lens.Compile()
        let compiledGetter =
            let lens = InternalExpressions.fieldOrPropertyToGet<'T,'U> v
            lens.Compile()
        { get = fun t -> compiledGetter.Invoke(t)
          set = fun v t -> compiledSetter.Invoke(v,t) }
    ///
    let toLensUntyped opt v: DataLens =
        let typ = FieldOrProperty.declaringType v
        let compiledSetter =
            let lens = InternalExpressions.fieldOrPropertyToSetUntyped opt typ typ v
            lens.Compile()
        let compiledGetter =
            let lens = InternalExpressions.fieldOrPropertyToGetUntyped typ v
            lens.Compile()
        { get = fun t -> compiledGetter.DynamicInvoke(t)
          set = fun v t -> compiledSetter.DynamicInvoke(v,t) }

module internal Object=
    let equals a b=Object.Equals(a,b)


module internal Expressions=
    module private Ctx=
        type Context = { Source:ParameterExpression; Parameters: ParameterExpression list; }
        let ofExpression (lambda:LambdaExpression)=
            {
                Source=lambda.Parameters |> Seq.head
                Parameters=lambda.Parameters |> Seq.tail |> Seq.toList
            }
    module private FieldOrProperty=
        /// return field or property given member expression
        let ofMemberExpression (memberAccess:MemberExpression)=
            let m = memberAccess.Member
            match m.MemberType with
            | MemberTypes.Field ->
               FieldOrProperty.create (m.DeclaringType.GetTypeInfo().GetField(m.Name))
            | MemberTypes.Property ->
               FieldOrProperty.create (m.DeclaringType.GetTypeInfo().GetProperty(m.Name))
            | _ ->
               raise (ExpectedButGotException<MemberTypes>([| MemberTypes.Field; MemberTypes.Property |], m.MemberType))
        /// given expression, return field or property
        let rec ofExpression (expr:Expression): FieldOrProperty seq=
            match expr.NodeType with
            | ExpressionType.Parameter->
                Seq.empty
            | ExpressionType.MemberAccess->
                let memberAccess = expr :?>MemberExpression
                seq {
                    yield! ofExpression memberAccess.Expression
                    yield ofMemberExpression(memberAccess)
                }
            | _ -> raise( ExpectedButGotException<ExpressionType>([| ExpressionType.Parameter; ExpressionType.MemberAccess |], expr.NodeType))

    module private DataLens=
        open Ctx
        /// turn member access in expression into untyped DataLens
        let rec ofMemberAccessUntyped opt (expr:Expression) : DataLens=

            match expr.NodeType with
            | ExpressionType.MemberAccess->
                let memberAccess = expr :?>MemberExpression
                if memberAccess.Expression.NodeType = ExpressionType.Parameter then
                    FieldOrProperty.toLensUntyped opt <| FieldOrProperty.ofMemberExpression memberAccess
                else
                    let current = ofMemberAccessUntyped opt memberAccess.Expression
                    let next = FieldOrProperty.toLensUntyped opt <| FieldOrProperty.ofMemberExpression memberAccess
                    DataLens.composeUntyped next current
            | _ -> raise( ExpectedButGotException<ExpressionType>([| ExpressionType.MemberAccess |], expr.NodeType));

        /// turn member access in expression into typed DataLens
        let rec ofMemberAccess<'T,'U> opt (expr:Expression) : DataLens<'T,'U>=
            match expr.NodeType with
            | ExpressionType.MemberAccess->
                let memberAccess = expr :?>MemberExpression
                if memberAccess.Expression.NodeType = ExpressionType.Parameter then
                    FieldOrProperty.toLens<'T,'U> opt <| FieldOrProperty.ofMemberExpression memberAccess
                else
                    // unable to continue with typed expressions:
                    let l=ofMemberAccessUntyped opt expr
                    DataLens.unbox<'T,'U> l
            | _ -> raise( ExpectedButGotException<ExpressionType>([| ExpressionType.MemberAccess |], expr.NodeType));

        /// turn binary expression with member expression into typed DataLens, the assumption being that the binary expression is an equals
        let expectLeftAndRightMemberAccessInBinaryExpression<'T,'U> opt (expr:BinaryExpression) (c:Context): DataLens<'T,'U>=
            let tryFind e = List.tryFind (Object.equals e) c.Parameters
            match tryFind expr.Right, tryFind expr.Left with
            | Some _,None ->
                ofMemberAccess opt expr.Left
            | None, Some _->
                ofMemberAccess opt expr.Right
            | None, None ->
                failwithf "1) Expected expression '%O' to yield either a left side member access or a right side member access (%A)" expr c
            | _ ->
                failwithf "2) Expected expression '%O' to yield either a left side member access or a right side member access (%A)" expr c
        /// turn binary expression with member expression into typed DataLens
        let ofBinaryExpressionEquals<'T,'U> opt (lambda:Expression) (c:Context): DataLens<'T,'U>=
            match lambda.NodeType with
            | ExpressionType.Equal ->
                let b=lambda :?> BinaryExpression
                match b.Left.NodeType with
                | ExpressionType.MemberAccess -> expectLeftAndRightMemberAccessInBinaryExpression opt b c
                | t->raise (ExpectedButGotException<ExpressionType>([| ExpressionType.MemberAccess; |], t))
            | t -> raise (ExpectedButGotException<ExpressionType>([| ExpressionType.Equal; |], t))
        let ofAndAlsoThenLeftRightMemberAccessExpression<'T,'U1,'U2> opt (lambda:Expression) (c:Context): DataLens<'T,struct('U1*'U2)>=
            match lambda.NodeType with
            | ExpressionType.AndAlso ->
                let b=lambda :?> BinaryExpression
                let left = expectLeftAndRightMemberAccessInBinaryExpression<'T,'U1> opt (b.Left:?>BinaryExpression) c
                let right = expectLeftAndRightMemberAccessInBinaryExpression<'T,'U2> opt (b.Right:?>BinaryExpression) c
                DataLens.combine left right
            | t -> raise (ExpectedButGotException<ExpressionType>([| ExpressionType.AndAlso |], t))
    /// create a data lens out an expression that looks like :
    /// <c>t=>t.Property</c>
    /// or in f# terms:
    /// <c>fun t -> t.Property</c>
    let withMemberAccess<'T,'U> opt (lambda:Expression<Func<'T, 'U>>) : DataLens<'T,'U>=
        DataLens.ofMemberAccess opt lambda.Body
    /// create a data lens out an expression that looks like :
    /// <c>(t,v)=>t.Property == v</c>
    /// or in f# terms:
    /// <c>fun (t,v)-> t.Property = v</c>
    let withEqualEqualOrCall<'T,'U> opt (lambda:Expression<Func<'T, 'U, bool>>) : DataLens<'T,'U>=
        let c = Ctx.ofExpression lambda
        DataLens.ofBinaryExpressionEquals opt lambda.Body c
    /// create a data lens out an expression that looks like :
    /// <c>(t,v1,v2)=>t.Property1 == v1 && t.Property2 == v2</c>
    /// or in f# terms:
    /// <c>fun (t,v1,v2)-> t.Property1 = v1 && t.Property2 = v2</c>
    let withEqualEqualOrCall2<'T,'U1,'U2> opt (lambda:Expression<Func<'T, 'U1, 'U2, bool>>) : DataLens<'T,struct('U1*'U2)>=
        let c = Ctx.ofExpression lambda
        DataLens.ofAndAlsoThenLeftRightMemberAccessExpression<'T, 'U1, 'U2> opt lambda.Body c
