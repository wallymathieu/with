namespace With
open System
open System.Linq.Expressions
open With.Lenses
open With.Internals
/// Helper type to create lenses
type Lens<'T>=
    /// Create a lens, created from the expression and the base type
    static member Of(expr:Expression<Func<'T, 'TValue>> ) : DataLens<'T,'TValue>= 
        Expressions.withMemberAccess expr
    /// Create a lens, created from the expression and the base type
    static member Of(expr:Expression<Func<'T, 'TValue, bool>> ) : DataLens<'T,'TValue>= 
        Expressions.withEqualEqualOrCall expr
    /// Create a lens, created from the expression and the base type
    static member Of(expr:Expression<Func<'T, 'TValue1, 'TValue2, bool>>) : DataLens<'T,struct('TValue1*'TValue2)>= 
        Expressions.withEqualEqualOrCall2 expr

/// LensBuilder in order to simplify building lenses in c#
type LensBuilder<'T,'U>(built:DataLens<'T,'U>)=
    /// Combine existing lens with another lens
    member __.And(lens: DataLens<'T, 'U2>) = 
        LensBuilder<'T,struct('U*'U2)>(DataLens.combine built lens)
    /// Combine existing lens with expression representing another lens
    member __.And(expr: Expression<Func<'T, 'U2>>) = 
        LensBuilder<'T,struct('U*'U2)>(DataLens.combine built <| Expressions.withMemberAccess expr)
    /// Combine existing lens with expression representing another lens
    member __.And(expr:Expression<Func<'T, 'U2, bool>> ) = 
        LensBuilder<'T,struct('U*'U2)>(DataLens.combine built <| Expressions.withEqualEqualOrCall expr)
    /// Combine existing lens with expression representing another lens
    member __.And(expr:Expression<Func<'T, 'U2, 'U3, bool>>) = 
        LensBuilder<'T,struct('U * struct('U2*'U3))>(DataLens.combine built <| Expressions.withEqualEqualOrCall2 expr)
    /// Compose existing lens with expression representing deeper lens
    member __.Then(expr: Expression<Func<'U, 'V>>) =
        let next = Expressions.withMemberAccess expr 
        LensBuilder<'T, 'V>(DataLens.compose next built)
    /// Compose existing lens with deeper lens
    member __.Then(next: DataLens<'U, 'V>) =
        LensBuilder<'T, 'V>(DataLens.compose next built)
    /// Current lens
    member __.Current = built

/// LensBuilder in order to simplify building lenses in c#
type LensBuilder<'T>()=
    /// Create a lensbuilder, created from the lens
    static member Of(lens:DataLens<'T, 'U> ) = 
        LensBuilder<'T,'U>(lens)
    /// Create a lensbuilder, created from the expression and the base type
    static member Of(expr:Expression<Func<'T, 'U>> ) = 
        LensBuilder<'T,'U>(Expressions.withMemberAccess expr) 
    /// Create a lensbuilder, created from the expression and the base type
    static member Of(expr:Expression<Func<'T, 'U, bool>> ) = 
        LensBuilder<'T,'U>(Expressions.withEqualEqualOrCall expr) 
    /// Create a lensbuilder, created from the expression and the base type
    static member Of(expr:Expression<Func<'T, 'U1, 'U2, bool>>) = 
        LensBuilder<'T,struct('U1*'U2)>(Expressions.withEqualEqualOrCall2 expr)


open System.Runtime.CompilerServices
/// Extensions to simplify usage in c#
[<Extension>]
type LensBuilderExtensions() =
    /// Build lens
    [<Extension>]
    static member Build (self: LensBuilder<'T, 'U1>) = self.Current

    /// Build 3 tuple lens
    [<Extension>]
    static member Build (self: LensBuilder<'T, struct('U1*struct('U2*'U3))>) =
        let lens = self.Current
        let right = DataLens.ofRightTuple()
        DataLens.compose right lens 
    /// Build 3 tuple lens
    [<Extension>]
    static member Build (self: LensBuilder<'T, struct(struct('U1*'U2)*'U3)>) =
        let lens = self.Current
        let left = DataLens.ofLeftTuple()
        DataLens.compose left lens 
    /// Build 4 tuple lens
    [<Extension>]
    static member Build (self: LensBuilder<'T, struct(struct(struct('U1*'U2)*'U3)*'U4)>) =
        let lens = self.Current
        let left = DataLens.ofLeftTuple2()
        DataLens.compose left lens
    /// Build 5 tuple lens
    [<Extension>]
    static member Build (self: LensBuilder<'T, struct(struct(struct(struct('U1*'U2)*'U3)*'U4)*'U5)>) =
        let lens = self.Current
        let left = DataLens.ofLeftTuple3()
        DataLens.compose left lens
    /// Build 6 tuple lens
    [<Extension>]
    static member Build (self: LensBuilder<'T, struct(struct(struct(struct(struct('U1*'U2)*'U3)*'U4)*'U5)*'U6)>) =
        let lens = self.Current
        let left = DataLens.ofLeftTuple4()
        DataLens.compose left lens
    /// Build 7 tuple lens
    [<Extension>]
    static member Build (self: LensBuilder<'T, struct(struct(struct(struct(struct(struct('U1*'U2)*'U3)*'U4)*'U5)*'U6)*'U7)>) =
        let lens = self.Current
        let left = DataLens.ofLeftTuple5()
        DataLens.compose left lens
    /// Build 8 tuple lens
    [<Extension>]
    static member Build (self: LensBuilder<'T, struct(struct(struct(struct(struct(struct(struct('U1*'U2)*'U3)*'U4)*'U5)*'U6)*'U7)*'U8)>) =
        let lens = self.Current
        let left = DataLens.ofLeftTuple6()
        DataLens.compose left lens
