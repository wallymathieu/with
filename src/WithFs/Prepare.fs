namespace With
open System
open System.Linq.Expressions
open With.Lenses
open With.Internals
type Lens<'T>=
    static member Of(expr:Expression<Func<'T, 'TValue>> ) : DataLens<'T,'TValue>= 
        Expressions.withMemberAccess expr 
    static member Of(expr:Expression<Func<'T, 'TValue, bool>> ) : DataLens<'T,'TValue>= 
        Expressions.withEqualEqualOrCall expr 
    static member Of(expr:Expression<Func<'T, 'TValue1, 'TValue2, bool>>) : DataLens<'T,('TValue1*'TValue2)>= 
        Expressions.withEqualEqualOrCall2 expr

type LensBuilder<'T,'U>(built:DataLens<'T,'U>)=
    /// Combine existing lens with expression representing another lens
    member __.And(expr: Expression<Func<'T, 'U2>>) = 
        LensBuilder<'T,('U*'U2)>(DataLens.combine built <| Expressions.withMemberAccess expr)
    /// Combine existing lens with expression representing another lens
    member __.And(expr:Expression<Func<'T, 'U2, bool>> ) = 
        LensBuilder<'T,('U*'U2)>(DataLens.combine built <| Expressions.withEqualEqualOrCall expr)
    /// Combine existing lens with expression representing another lens
    member __.And(expr:Expression<Func<'T, 'U2, 'U3, bool>>) = 
        LensBuilder<'T,('U * ('U2*'U3))>(DataLens.combine built <| Expressions.withEqualEqualOrCall2 expr)
    /// Compose existing lens with expression representing deeper lens
    member __.Then(expr: Expression<Func<'U, 'V>>) =
        let next = Expressions.withMemberAccess expr 
        LensBuilder<'T, 'V>(DataLens.compose next built)

    member __.Build() = built

type LensBuilder<'T>()=
    static member Of(expr:Expression<Func<'T, 'U>> ) = 
        LensBuilder<'T,'U>(Expressions.withMemberAccess expr) 
    static member Of(expr:Expression<Func<'T, 'U, bool>> ) = 
        LensBuilder<'T,'U>(Expressions.withEqualEqualOrCall expr) 
    static member Of(expr:Expression<Func<'T, 'U1, 'U2, bool>>) = 
        LensBuilder<'T,('U1*'U2)>(Expressions.withEqualEqualOrCall2 expr)

type Prepare=
    static member Copy(expr:Expression<Func<'T, 'TValue, bool>> ) = 
        let eqeq=Expressions.withEqualEqualOrCall expr 
        eqeq.ToPreparedCopy()
    static member Copy(expr:Expression<Func<'T, 'TValue1, 'TValue2, bool>> )=
        let eqeq=Expressions.withEqualEqualOrCall2 expr 
        { new IPreparedCopy<'T, 'TValue1,'TValue2> 
          with 
            member __.Copy(t,v1,v2)= DataLens.set (v1,v2) t eqeq }

open System.Runtime.CompilerServices
/// Extensions to simplify usage in c#
[<Extension>]
type LensBuilderExtensions() =
    /// Build prepared copy
    [<Extension>]
    static member BuildPreparedCopy (self: LensBuilder<'T, 'U1>) =
        let lens = self.Build()
        { new IPreparedCopy<'T, 'U1> 
          with 
            member __.Copy(t,v1)= DataLens.set v1 t lens }

    /// Build prepared copy
    [<Extension>]
    static member BuildPreparedCopy (self: LensBuilder<'T, ('U1*'U2)>) =
        let lens = self.Build()
        { new IPreparedCopy<'T, 'U1,'U2> 
          with 
            member __.Copy(t,v1,v2)= DataLens.set (v1,v2) t lens }
    /// Build prepared copy
    [<Extension>]
    static member BuildPreparedCopy (self: LensBuilder<'T, ('U1*('U2*'U3))>) =
        let lens = self.Build()
        let right = DataLens.ofRightTuple()
        let combined = DataLens.compose right lens 
        { new IPreparedCopy<'T, 'U1,'U2, 'U3> 
          with 
            member __.Copy(t,v1,v2,v3)= DataLens.set (v1,v2,v3) t combined }
    /// Build prepared copy
    [<Extension>]
    static member BuildPreparedCopy (self: LensBuilder<'T, (('U1*'U2)*'U3)>) =
        let lens = self.Build()
        let left = DataLens.ofLeftTuple()
        let combined = DataLens.compose left lens 
        { new IPreparedCopy<'T, 'U1,'U2, 'U3> 
          with 
            member __.Copy(t,v1,v2,v3)= DataLens.set (v1,v2,v3) t combined }
    /// Build prepared copy
    [<Extension>]
    static member BuildPreparedCopy (self: LensBuilder<'T, ((('U1*'U2)*'U3)*'U4)>) =
        let lens = self.Build()
        let left = DataLens.ofLeftTuple'()
        let combined = DataLens.compose left lens
        { new IPreparedCopy<'T, 'U1, 'U2, 'U3, 'U4> 
          with 
            member __.Copy(t,v1,v2,v3,v4)= DataLens.set (v1,v2,v3,v4) t combined }
