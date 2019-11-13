namespace With
open System
open System.Linq.Expressions
open With.Lenses
open With.Internals
type Lens<'T>=
    static member Of(expr:Expression<Func<'T, 'TValue, bool>> ) : DataLens<'T,'TValue>= 
        Expressions.expressionWithEqualEqualOrCall expr 
    static member Of(expr:Expression<Func<'T, 'TValue1, 'TValue2, bool>>) : DataLens<'T,('TValue1*'TValue2)>= 
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

type LensBuilder<'T,'U>(built:DataLens<'T,'U>)=
    member __.And(expr:Expression<Func<'T, 'U2, bool>> ) = 
        LensBuilder<'T,('U*'U2)>(DataLens.combine built <| Expressions.expressionWithEqualEqualOrCall expr)
    member __.And(expr:Expression<Func<'T, 'U2, 'U3, bool>>) = 
        LensBuilder<'T,('U * ('U2*'U3))>(DataLens.combine built <| Expressions.expressionWithEqualEqualOrCall2 expr)
    member __.Build() = built
    member __.BuildPreparedCopy()=built.ToPreparedCopy()

type LensBuilder<'T>()=
    static member Of(expr:Expression<Func<'T, 'U, bool>> ) = 
        LensBuilder<'T,'U>(Expressions.expressionWithEqualEqualOrCall expr) 
    static member Of(expr:Expression<Func<'T, 'U1, 'U2, bool>>) = 
        LensBuilder<'T,('U1*'U2)>(Expressions.expressionWithEqualEqualOrCall2 expr)
