namespace With.Internals
open With
open System.Reflection
open System
open With.Lenses
open System.Linq.Expressions
module internal Object=
    let equals a b=Object.Equals(a,b)
module Expressions=
    [<AutoOpen>]    
    module private Ctx=
        type Context = { Source:ParameterExpression; Parameters: ParameterExpression list }
        let fromExpression (lambda:LambdaExpression)=
            {
                Source=lambda.Parameters |> Seq.head
                Parameters=lambda.Parameters |> Seq.tail |> Seq.toList 
            }
    module private Expr=
        let getMember (memberAccess:MemberExpression)=
            let m = memberAccess.Member
            match m.MemberType with
            | MemberTypes.Field ->
               FieldOrProperty.create (m.DeclaringType.GetTypeInfo().GetField(m.Name))
            | MemberTypes.Property ->
               FieldOrProperty.create (m.DeclaringType.GetTypeInfo().GetProperty(m.Name))
            | _ -> 
               raise (ExpectedButGotException<MemberTypes>([| MemberTypes.Field; MemberTypes.Property |], m.MemberType))
        module FieldOrProperty=
            let rec withMemberAccess (expr:Expression): FieldOrProperty seq=
                match expr.NodeType with
                | ExpressionType.Parameter->
                    Seq.empty
                | ExpressionType.MemberAccess->
                    let memberAccess = expr :?>MemberExpression
                    seq { 
                        yield! withMemberAccess memberAccess.Expression
                        yield getMember(memberAccess)
                    }
                | _ -> raise( ExpectedButGotException<ExpressionType>([| ExpressionType.Parameter; ExpressionType.MemberAccess |], expr.NodeType))

        let rec withMemberAccess<'T,'U> (expr:Expression) : DataLens<'T,'U>=
            match expr.NodeType with
            | ExpressionType.MemberAccess->
                let memberAccess = expr :?>MemberExpression
                if memberAccess.Expression.NodeType = ExpressionType.Parameter then
                    DataLens.fieldOrPropertyToLens<'T,'U> <| getMember memberAccess
                else
                    let inner = withMemberAccess memberAccess.Expression
                    let current = DataLens.fieldOrPropertyToLens<'T,'U> <| getMember memberAccess
                    DataLens.compose inner current
            | _ -> raise( ExpectedButGotException<ExpressionType>([| ExpressionType.MemberAccess |], expr.NodeType));

    module private rec EqEq=
        let binaryExpressionWithMemberAccess<'T,'U> (expr:BinaryExpression) (c:Context): DataLens<'T,'U>=
            let tryFind e = List.tryFind (Object.equals e) c.Parameters
            match tryFind expr.Right, tryFind expr.Left with
            | Some _,None ->
                Expr.withMemberAccess expr.Left
            | None, Some _->
                Expr.withMemberAccess expr.Right
            | None, None ->
                failwithf "1) Expected expression '%O' to yield either a left side member access or a right side member access (%A)" expr c
            | _ ->
                failwithf "2) Expected expression '%O' to yield either a left side member access or a right side member access (%A)" expr c
        let expressionWithMemberAccessOrCall<'T,'U> (lambda:Expression) (c:Context): DataLens<'T,'U>=
            match lambda.NodeType with
            | ExpressionType.Equal ->
                let b=lambda :?> BinaryExpression
                match b.Left.NodeType with
                | ExpressionType.MemberAccess -> EqEq.binaryExpressionWithMemberAccess b c
                | t->raise (ExpectedButGotException<ExpressionType>([| ExpressionType.MemberAccess; |], t)) 
            | t -> raise (ExpectedButGotException<ExpressionType>([| ExpressionType.Equal; |], t))
    module private AndAlso=
        let expressionWithMemberAccessOrCall2<'T,'U1,'U2> (lambda:Expression) (c:Context): DataLens<'T,'U1*'U2>=
            match lambda.NodeType with
            | ExpressionType.AndAlso ->
                let b=lambda :?> BinaryExpression
                DataLens.combine (EqEq.binaryExpressionWithMemberAccess<'T,'U1> (b.Left:?>BinaryExpression) c)
                                 (EqEq.binaryExpressionWithMemberAccess<'T,'U2> (b.Right:?>BinaryExpression) c)
            | t -> raise (ExpectedButGotException<ExpressionType>([| ExpressionType.AndAlso |], t))
    [<CompiledName("WithMemberAccess")>]
    let withMemberAccess<'T,'U> (lambda:Expression<Func<'T, 'U>>) : DataLens<'T,'U>=
        Expr.withMemberAccess lambda.Body
    
    [<CompiledName("WithEqualEqualOrCall")>]
    let withEqualEqualOrCall<'T,'U> (lambda:Expression<Func<'T, 'U, bool>>) : DataLens<'T,'U>=
        let c = fromExpression lambda
        EqEq.expressionWithMemberAccessOrCall lambda.Body c
    [<CompiledName("WithEqualEqualOrCall2")>]
    let withEqualEqualOrCall2<'T,'U1,'U2> (lambda:Expression<Func<'T, 'U1, 'U2, bool>>) : DataLens<'T,'U1*'U2>=
        let c = fromExpression lambda
        AndAlso.expressionWithMemberAccessOrCall2<'T, 'U1, 'U2> lambda.Body c
