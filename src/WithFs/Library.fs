namespace With.Internals
open With
open System.Reflection
open System
open System.Collections
open System.Linq.Expressions

module Expressions=

    let getMember (memberAccess:MemberExpression)=
        let m = memberAccess.Member
        match m.MemberType with
        | MemberTypes.Field ->
           FieldOrProperty.Create (m.DeclaringType.GetTypeInfo().GetField(m.Name))
        | MemberTypes.Property ->
           FieldOrProperty.Create (m.DeclaringType.GetTypeInfo().GetProperty(m.Name))
        | _ -> 
           raise (ExpectedButGotException<MemberTypes>([| MemberTypes.Field; MemberTypes.Property |], m.MemberType))

    let unaryResultExpression (expr: MethodCallExpression) (lambda:LambdaExpression) (_object:obj)=
        let memberAccess = expr.Arguments |> Seq.head :?> MemberExpression 
        failwith "!"
    [<CompiledName("ExpressionWithMemberAccess")>]
    let rec expressionWithMemberAccess (expr:Expression) : FieldOrProperty seq=
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
