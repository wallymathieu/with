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
            member __.Copy(t,v)= DataLens.set v t eqeq }
    static member Copy(expr:Expression<Func<'T, 'TValue1, 'TValue2, bool>> )=
        let eqeq=Expressions.expressionWithEqualEqualOrCall2 expr 
        { new IPreparedCopy<'T,'TValue1,'TValue2> 
          with 
            member __.Copy(t,v1,v2)= DataLens.set (v1,v2) t eqeq }
