namespace With.Internals
open With
open System.Reflection
open System
open With.Lenses
open System.Linq.Expressions
module internal Object=
    let equals a b=Object.Equals(a,b)


module Expressions=
    module private Ctx=
        type Context = { Source:ParameterExpression; Parameters: ParameterExpression list }
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
        let rec ofMemberAccessUntyped (expr:Expression) : DataLens=

            match expr.NodeType with
            | ExpressionType.MemberAccess->
                let memberAccess = expr :?>MemberExpression
                if memberAccess.Expression.NodeType = ExpressionType.Parameter then
                    FieldOrProperty.toLensUntyped <| FieldOrProperty.ofMemberExpression memberAccess
                else
                    let current = ofMemberAccessUntyped memberAccess.Expression
                    let next = FieldOrProperty.toLensUntyped <| FieldOrProperty.ofMemberExpression memberAccess
                    DataLens.composeUntyped next current
            | _ -> raise( ExpectedButGotException<ExpressionType>([| ExpressionType.MemberAccess |], expr.NodeType));

        /// turn member access in expression into typed DataLens
        let rec ofMemberAccess<'T,'U> (expr:Expression) : DataLens<'T,'U>=
            match expr.NodeType with
            | ExpressionType.MemberAccess->
                let memberAccess = expr :?>MemberExpression
                if memberAccess.Expression.NodeType = ExpressionType.Parameter then
                    FieldOrProperty.toLens<'T,'U> <| FieldOrProperty.ofMemberExpression memberAccess
                else
                    // unable to continue with typed expressions:
                    let l=ofMemberAccessUntyped expr
                    DataLens.unbox<'T,'U> l
            | _ -> raise( ExpectedButGotException<ExpressionType>([| ExpressionType.MemberAccess |], expr.NodeType));

        /// turn binary expression with member expression into typed DataLens, the assumption being that the binary expression is an equals
        let expectLeftAndRightMemberAccessInBinaryExpression<'T,'U> (expr:BinaryExpression) (c:Context): DataLens<'T,'U>=
            let tryFind e = List.tryFind (Object.equals e) c.Parameters
            match tryFind expr.Right, tryFind expr.Left with
            | Some _,None ->
                ofMemberAccess expr.Left
            | None, Some _->
                ofMemberAccess expr.Right
            | None, None ->
                failwithf "1) Expected expression '%O' to yield either a left side member access or a right side member access (%A)" expr c
            | _ ->
                failwithf "2) Expected expression '%O' to yield either a left side member access or a right side member access (%A)" expr c
        /// turn binary expression with member expression into typed DataLens
        let ofBinaryExpressionEquals<'T,'U> (lambda:Expression) (c:Context): DataLens<'T,'U>=
            match lambda.NodeType with
            | ExpressionType.Equal ->
                let b=lambda :?> BinaryExpression
                match b.Left.NodeType with
                | ExpressionType.MemberAccess -> expectLeftAndRightMemberAccessInBinaryExpression b c
                | t->raise (ExpectedButGotException<ExpressionType>([| ExpressionType.MemberAccess; |], t)) 
            | t -> raise (ExpectedButGotException<ExpressionType>([| ExpressionType.Equal; |], t))
        let ofAndAlsoThenLeftRightMemberAccessExpression<'T,'U1,'U2> (lambda:Expression) (c:Context): DataLens<'T,'U1*'U2>=
            match lambda.NodeType with
            | ExpressionType.AndAlso ->
                let b=lambda :?> BinaryExpression
                let left = expectLeftAndRightMemberAccessInBinaryExpression<'T,'U1> (b.Left:?>BinaryExpression) c
                let right = expectLeftAndRightMemberAccessInBinaryExpression<'T,'U2> (b.Right:?>BinaryExpression) c
                DataLens.combine left right
            | t -> raise (ExpectedButGotException<ExpressionType>([| ExpressionType.AndAlso |], t))
    /// create a data lens out an expression that looks like : 
    /// <c>t=>t.Property</c>
    /// or in f# terms:
    /// <c>fun t -> t.Property</c>
    [<CompiledName("WithMemberAccess")>]
    let withMemberAccess<'T,'U> (lambda:Expression<Func<'T, 'U>>) : DataLens<'T,'U>=
        DataLens.ofMemberAccess lambda.Body
    /// create a data lens out an expression that looks like : 
    /// <c>(t,v)=>t.Property == v</c>
    /// or in f# terms:
    /// <c>fun (t,v)-> t.Property = v</c>
    [<CompiledName("WithEqualEqualOrCall")>]
    let withEqualEqualOrCall<'T,'U> (lambda:Expression<Func<'T, 'U, bool>>) : DataLens<'T,'U>=
        let c = Ctx.ofExpression lambda
        DataLens.ofBinaryExpressionEquals lambda.Body c
    /// create a data lens out an expression that looks like : 
    /// <c>(t,v1,v2)=>t.Property1 == v1 && t.Property2 == v2</c>
    /// or in f# terms:
    /// <c>fun (t,v1,v2)-> t.Property1 = v1 && t.Property2 = v2</c>
    [<CompiledName("WithEqualEqualOrCall2")>]
    let withEqualEqualOrCall2<'T,'U1,'U2> (lambda:Expression<Func<'T, 'U1, 'U2, bool>>) : DataLens<'T,'U1*'U2>=
        let c = Ctx.ofExpression lambda
        DataLens.ofAndAlsoThenLeftRightMemberAccessExpression<'T, 'U1, 'U2> lambda.Body c
