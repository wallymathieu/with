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
           FieldOrProperty.Create (m.DeclaringType.GetTypeInfo().GetField(m.Name))
        | MemberTypes.Property ->
           FieldOrProperty.Create (m.DeclaringType.GetTypeInfo().GetProperty(m.Name))
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
        let expressionWithMemberAccess (expr:Expression): FieldOrProperty =
            match expr.NodeType with
            | ExpressionType.MemberAccess->
                let memberAccess = expr :?>MemberExpression
                getMember(memberAccess)
            | _ -> raise( ExpectedButGotException<ExpressionType>([| ExpressionType.MemberAccess |], expr.NodeType));
            
        let unaryResultExpression (lambda:LambdaExpression) (c:Context) = 
            //let memberAccess = expr.Arguments |> Seq.head :?> MemberExpression 
            failwithf "! %A Not implemented" lambda
        let binaryExpressionWithMemberAccess (expr:BinaryExpression) (c:Context) : (int*FieldOrProperty) seq=seq { 
            match List.tryFindIndex (Object.equals expr.Right) c.Parameters with
            | Some index->
                yield (index, expressionWithMemberAccess expr.Left)
            | None -> 
                failwithf "! %A %A !" expr c
        }

        let binaryExpressionAndOrEqualOrMemberAccess (expr:BinaryExpression) (c:Context): (int*FieldOrProperty) seq=
            match expr.Left.NodeType with
            | ExpressionType.AndAlso
            | ExpressionType.Equal ->
                binaryExpressionAndOrEqual expr c
            | ExpressionType.MemberAccess ->
                binaryExpressionWithMemberAccess expr c
            | _ ->
               raise (ExpectedButGotException<ExpressionType>([| ExpressionType.Equal; ExpressionType.AndAlso; ExpressionType.MemberAccess |], expr.Left.NodeType))
    
        let binaryExpressionAndOrEqual (expr:BinaryExpression) (c:Context): (int*FieldOrProperty) seq=seq{
            yield! binaryExpressionAndOrEqualOrMemberAccess (expr.Left :?> BinaryExpression) c
            match expr.Right.NodeType with
            | ExpressionType.AndAlso
            | ExpressionType.Equal ->
                yield! binaryExpressionAndOrEqualOrMemberAccess (expr.Right :?> BinaryExpression) c
            | _ ->
               raise (ExpectedButGotException<ExpressionType>([| ExpressionType.Equal; ExpressionType.AndAlso |], expr.Right.NodeType))
        }
    [<CompiledName("ExpressionWithEqualEqualOrCall")>]
    let expressionWithEqualEqualOrCall (lambda:LambdaExpression) : Map<int,FieldOrProperty>=
        let c = {
            EqEq.Source=lambda.Parameters |> Seq.head
            EqEq.Parameters=lambda.Parameters |> Seq.tail |> Seq.toList 
        }
        let indexSeq= 
            match lambda.Body.NodeType with
            | ExpressionType.Equal
            | ExpressionType.AndAlso ->
                EqEq.binaryExpressionAndOrEqualOrMemberAccess (lambda.Body :?> BinaryExpression) c
            | ExpressionType.Call ->
                EqEq.unaryResultExpression lambda c
            | _ ->
               raise (ExpectedButGotException<ExpressionType>([| ExpressionType.Equal; ExpressionType.AndAlso; ExpressionType.Call  |], lambda.Body.NodeType))
        Map indexSeq

