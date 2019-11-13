namespace With
open System
open System.Linq.Expressions
open With.Lenses
open With.Internals
type Lens=
    static member Of(expr:Expression<Func<'T, 'TValue, bool>> ) = 
        Expressions.expressionWithEqualEqualOrCall expr 
    static member Of(expr:Expression<Func<'T, 'TValue1, 'TValue2, bool>>) = 
        Expressions.expressionWithEqualEqualOrCall2 expr
type Prepare=
    static member Copy(expr:Expression<Func<'T, 'TValue, bool>> ) = 
        let eqeq=Expressions.expressionWithEqualEqualOrCall expr 
        eqeq.ToPreparedCopy()
    static member Copy(expr:Expression<Func<'T, 'TValue1, 'TValue2, bool>> )=
        let eqeq=Expressions.expressionWithEqualEqualOrCall2 expr 
        { new IPreparedCopy<'T, 'TValue1,'TValue2> 
          with 
            member __.Copy(t,v1,v2)= DataLens.set (v1,v2) t eqeq }
