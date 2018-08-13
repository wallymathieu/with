namespace With.Internals
open With
open System.Reflection
open System
open System.Collections
open System.Linq.Expressions
module Object=
    let equals a b=Object.Equals(a,b)
module Expressions=
    [<CompiledName("GetMember")>]
    let getMember (memberAccess:MemberExpression)=
        let m = memberAccess.Member
        match m.MemberType with
        | MemberTypes.Field ->
           FieldOrProperty.create (m.DeclaringType.GetTypeInfo().GetField(m.Name))
        | MemberTypes.Property ->
           FieldOrProperty.create (m.DeclaringType.GetTypeInfo().GetProperty(m.Name))
        | _ -> 
           raise (ExpectedButGotException<MemberTypes>([| MemberTypes.Field; MemberTypes.Property |], m.MemberType))
    [<CompiledName("ExpressionWithMemberAccess")>]
    let rec expressionWithMemberAccess (expr:Expression): FieldOrProperty seq=
        match expr.NodeType with
        | ExpressionType.Parameter->
            Seq.empty
        | ExpressionType.MemberAccess->
            let memberAccess = expr :?>MemberExpression
            seq { 
                yield! expressionWithMemberAccess memberAccess.Expression
                yield getMember(memberAccess)
            }
        | _ -> raise( ExpectedButGotException<ExpressionType>([| ExpressionType.Parameter; ExpressionType.MemberAccess |], expr.NodeType));

    module private rec EqEq=

        type Context = { Source:ParameterExpression; Parameters: ParameterExpression list }
        let rec expressionWithMemberAccess (expr:Expression) =
            match expr.NodeType with
            | ExpressionType.MemberAccess->
                let memberAccess = expr :?>MemberExpression
                if memberAccess.Expression.NodeType = ExpressionType.Parameter then
                    DataLens.fieldOrPropertyToLens <| getMember memberAccess
                else
                    let inner = expressionWithMemberAccess memberAccess.Expression
                    let current = DataLens.fieldOrPropertyToLens <| getMember memberAccess
                    DataLens.compose inner current
            | _ -> raise( ExpectedButGotException<ExpressionType>([| ExpressionType.MemberAccess |], expr.NodeType));
            
        let binaryExpressionWithMemberAccess<'T,'U> (expr:BinaryExpression) (c:Context): DataLens<'T,'U>=
            let tryFind e = List.tryFind (Object.equals e) c.Parameters
            match tryFind expr.Right, tryFind expr.Left with
            | Some right,None ->
                expressionWithMemberAccess expr.Left
                //yield (index, )
            | None, Some left->
                expressionWithMemberAccess expr.Right
                //yield (index, )
            | _ -> 
                failwithf "! %A %A !" expr c

    [<CompiledName("ExpressionWithEqualEqualOrCall")>]
    let expressionWithEqualEqualOrCall<'T,'U> (lambda:Expression<Func<'T, 'U, bool>>) : DataLens<'T,'U>=
        let c = {
            EqEq.Source=lambda.Parameters |> Seq.head
            EqEq.Parameters=lambda.Parameters |> Seq.tail |> Seq.toList 
        }
        match lambda.Body.NodeType with
        | ExpressionType.Equal ->
            let b=lambda.Body :?> BinaryExpression
            match b.Left.NodeType with
            | ExpressionType.MemberAccess -> EqEq.binaryExpressionWithMemberAccess b c
            | t->raise (ExpectedButGotException<ExpressionType>([| ExpressionType.MemberAccess; |], t)) 
        | t -> raise (ExpectedButGotException<ExpressionType>([| ExpressionType.Equal; |], t))
    [<CompiledName("ExpressionWithEqualEqualOrCall2")>]
    let expressionWithEqualEqualOrCall2<'T,'U1,'U2> (lambda:Expression<Func<'T, 'U1, 'U2, bool>>) : DataLens<'T,'U1*'U2>=
        let c = {
            EqEq.Source=lambda.Parameters |> Seq.head
            EqEq.Parameters=lambda.Parameters |> Seq.tail |> Seq.toList 
        }
        match lambda.Body.NodeType with
        | ExpressionType.AndAlso ->
            let b=lambda.Body :?> BinaryExpression
            DataLens.compose (EqEq.binaryExpressionWithMemberAccess (b.Left:?>BinaryExpression) c)
                             (EqEq.binaryExpressionWithMemberAccess (b.Right:?>BinaryExpression) c)
        | t -> raise (ExpectedButGotException<ExpressionType>([| ExpressionType.AndAlso |], t))

