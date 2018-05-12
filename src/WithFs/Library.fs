namespace With.Internals
open With
open System.Reflection
open System
open System.Collections
open System.Linq.Expressions

module E=
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
    let getFromExpr (e: Expression<Func<'T, 'TValue,bool>>)=
        failwith "!"    

