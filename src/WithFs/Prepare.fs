namespace With
open System
open System.Linq.Expressions
open System.Runtime.CompilerServices
open With.Internals
type Prepare=
    static member Copy(expr:Expression<Func<'T, 'TValue, bool>> ) = 
        let eqeq=Expressions.expressionWithEqualEqualOrCall expr 
        { new IPreparedCopy<'T,'TValue> 
          with 
            member __.Copy(t,v)=
                let values = eqeq |> FieldOrPropertyMap.withValues [| v |]
                CreateInstanceFromValues.Create<'T>(t, values)}
    static member Copy(expr:Expression<Func<'T, 'TValue1, 'TValue2, bool>> ) = 
        let eqeq=Expressions.expressionWithEqualEqualOrCall expr 
        { new IPreparedCopy<'T,'TValue1,'TValue2> 
          with 
            member __.Copy(t,v1,v2)=
                let values = eqeq |> FieldOrPropertyMap.withValues [| v1;v2 |]
                CreateInstanceFromValues.Create<'T>(t, values)}
    static member Copy(expr:Expression<Func<'T, 'TValue1, 'TValue2, 'TValue3, bool>> ) = 
        let eqeq=Expressions.expressionWithEqualEqualOrCall expr 
        { new IPreparedCopy<'T,'TValue1,'TValue2, 'TValue3> 
          with 
            member __.Copy(t,v1,v2,v3)=
                let values = eqeq |> FieldOrPropertyMap.withValues [| v1;v2;v3 |]
                CreateInstanceFromValues.Create<'T>(t, values)}
    static member Copy(expr:Expression<Func<'T, 'TValue1, 'TValue2, 'TValue3, 'TValue4, bool>> ) = 
        let eqeq=Expressions.expressionWithEqualEqualOrCall expr 
        { new IPreparedCopy<'T,'TValue1,'TValue2, 'TValue3, 'TValue4>
          with 
            member __.Copy(t,v1,v2,v3,v4)=
                let values = eqeq |> FieldOrPropertyMap.withValues [| v1;v2;v3;v4 |]
                CreateInstanceFromValues.Create<'T>(t, values)}
