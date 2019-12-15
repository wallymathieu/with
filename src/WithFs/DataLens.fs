namespace With.Lenses
open System
open With
open System.ComponentModel
/// Data Lens abstraction
type IDataLens<'T,'U> = interface
    /// Get value out lens
    abstract member Get: 'T -> 'U
    /// Set value and return result
    abstract member Set: 'T*'U -> 'T
end

/// Copy of Lens definition from <a href="https://github.com/fsprojects/FSharpx.Extras/blob/master/src/FSharpx.Extras/Lens.fs">FSharpx.Extras</a>
/// A lens is sort of like a property for immutable data on steroids.
/// You can compose and combine lenses
type DataLens<'T, 'U> =
    { /// Get value from 'T
      [<CompiledName("FSharpGet"); EditorBrowsable(EditorBrowsableState.Never)>]
      get: 'T -> 'U
      /// Get a new instance of 'T with value set to the first parameter
      [<CompiledName("FSharpSet"); EditorBrowsable(EditorBrowsableState.Never)>]
      set: 'U -> 'T -> 'T }
with
    static member Create(get:Func<'T,'U>,set:Func<'T,'U,'T>)={ get=(fun t-> get.Invoke(t)); set = fun u t -> set.Invoke(t,u) }
    interface IDataLens<'T,'U> with
        member l.Get (t)=l.get t
        member l.Set (t,v)=l.set v t

type internal DataLens = DataLens<obj, obj>
/// DataLens operations intended for f# code
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module DataLens =
    /// change the IDataLens to a DataLens in order to work with it
    let internal coerce(l:IDataLens<'T,'U>) =
        match l with
        | :? DataLens<'T,'U> as lr ->lr
        | _ -> { get=(fun t-> l.Get(t)); set = fun u t -> l.Set(t,u) }
    /// Read the value of t, then apply the function f on that value and set the value into a new instance of t of 'T
    let update f t l = l.set (f (l.get t)) t

    /// Get value from 'T
    let inline get a (l: DataLens<_, _>) = l.get a
    /// Get a new instance of 'T with value set to the first parameter
    let inline set v a (l: DataLens<_, _>) = l.set v a
    /// combine two lenses into one where the two lenses operates on the same "record" type
    let inline combine (l1: DataLens<'t, 'v1>) (l2: DataLens<'t, 'v2>) =
        { get = fun t -> struct(l1.get t, l2.get t)
          set = fun struct(v1, v2) t -> l2.set v2 (l1.set v1 t) }
    /// Sequentially composes two lenses. Can be used to "drill down" into an object grap.
    /// For instance to be able to do something <c> t.Customer.Name = v </c> for immutable data.
    let inline compose (l1: DataLens<'TU, 'U>) (l2: DataLens<'T, 'TU>) =
        { get = l2.get >> l1.get
          set = l1.set >> fun f t -> update f t l2 }
    /// Sequentially composes two lenses in an untyped manner
    let inline internal composeUntyped (l1: DataLens) (l2: DataLens) = compose l1 l2
    /// Split a 2-tuples into a 3-tuple
    let ofTuple(): DataLens<struct(struct('v1 * 'v2) * 'v3),struct( 'v1 * 'v2 * 'v3)> =
        { get = fun struct(struct(v1, v2), v3) -> (v1, v2, v3)
          set = fun struct(v1, v2, v3) _ -> ((v1, v2), v3) }

    /// Split a cons combination of 2 2-tuples into a 3-tuple
    let ofLeftTuple(): DataLens<struct(struct('v1 * 'v2) * 'v3), struct( 'v1 * 'v2 * 'v3)> =
        { get = fun struct(struct(v1, v2), v3) -> (v1, v2, v3)
          set = fun struct(v1, v2, v3) _ -> ((v1, v2), v3) }
    /// Split a cons combination of 2 2-tuples into a 4-tuple
    let ofLeftTuple2(): DataLens<struct(struct(struct('v1 * 'v2) * 'v3) * 'v4), struct('v1 * 'v2 * 'v3 * 'v4)> =
        { get = fun struct(struct(struct(v1, v2), v3), v4) -> (v1, v2, v3, v4)
          set = fun struct(v1, v2, v3, v4) _ -> (((v1, v2), v3), v4) }
    /// Split a cons combination of 3 2-tuples into a 5-tuple
    let ofLeftTuple3(): DataLens<struct(struct(struct(struct('v1 * 'v2) * 'v3) * 'v4)*'v5), struct('v1 * 'v2 * 'v3 * 'v4 * 'v5)> =
        { get = fun struct(struct(struct(struct(v1, v2), v3), v4),v5) -> (v1, v2, v3, v4, v5)
          set = fun struct(v1, v2, v3, v4, v5) _ -> ((((v1, v2), v3), v4), v5) }
    /// Split a cons combination of 4 2-tuples into a 6-tuple
    let ofLeftTuple4(): DataLens<struct(struct(struct(struct(struct('v1 * 'v2) * 'v3) * 'v4)*'v5)*'v6), struct('v1 * 'v2 * 'v3 * 'v4 * 'v5 * 'v6)> =
        { get = fun struct(struct(struct(struct(struct(v1, v2), v3), v4),v5),v6) -> (v1, v2, v3, v4, v5,v6)
          set = fun struct(v1, v2, v3, v4, v5, v6) _ -> (((((v1, v2), v3), v4),v5),v6) }
    /// Split a cons combination of 5 2-tuples into a 7-tuple
    let ofLeftTuple5(): DataLens<struct(struct(struct(struct(struct(struct('v1 * 'v2) * 'v3) * 'v4)*'v5)*'v6)*'v7), struct('v1 * 'v2 * 'v3 * 'v4 * 'v5 * 'v6 * 'v7)> =
        { get = fun struct(struct(struct(struct(struct(struct(v1, v2), v3), v4),v5),v6),v7)-> (v1, v2, v3, v4, v5,v6, v7)
          set = fun struct(v1, v2, v3, v4, v5,v6, v7) _ -> ((((((v1, v2), v3), v4),v5),v6),v7) }
    /// Split a cons combination of 6 2-tuples into a 8-tuple
    let ofLeftTuple6(): DataLens<struct(struct(struct(struct(struct(struct(struct('v1 * 'v2) * 'v3) * 'v4)*'v5)*'v6)*'v7)*'v8), struct('v1 * 'v2 * 'v3 * 'v4 * 'v5 * 'v6 * 'v7 * 'v8)> =
        { get = fun struct(struct(struct(struct(struct(struct(struct(v1, v2), v3), v4),v5),v6),v7),v8)-> (v1, v2, v3, v4, v5,v6, v7,v8)
          set = fun struct(v1, v2, v3, v4, v5,v6, v7,v8) _ -> (((((((v1, v2), v3), v4),v5),v6),v7),v8) }
    /// Split a cons combination of 2 2-tuples into a 3-tuple
    let ofRightTuple(): DataLens<struct('v1 * struct('v2 * 'v3)), struct('v1 * 'v2 * 'v3)> =
        { get = fun struct(v1, struct(v2, v3)) -> (v1, v2, v3)
          set = fun struct(v1, v2, v3) _ -> (v1, (v2, v3)) }
    /// Unbox an untyped lens into a typed lens
    let internal unbox<'T, 'U> (l: DataLens): DataLens<'T, 'U> =
        { get = fun t -> unbox (l.get(box t))
          set = fun v t -> unbox (l.set (box v) (box t)) }
// Additional member functions inteded for c# code
type DataLens<'T, 'U> with
    /// combine two lenses into one where the two lenses operates on the same "record" type
    member l1.Combine l2 = DataLens.combine l1 l2
    /// Sequentially composes two lenses
    member l1.Compose l2 = DataLens.compose l1 l2
    /// Sequentially composes two lenses. Same as compose but in the opposite direction.
    member l1.AndThen l2 = DataLens.compose l2 l1
    /// Set value and return new instance
    member l.Set(t, v) = DataLens.set v t l
    /// Get value from instance
    member l.Get(t) = DataLens.get t l
    /// Read the value of t, then apply the function f on that value and set the value into a new instance of t of 'T
    member l.Update (f:Func<'U,'U>, t) = DataLens.update (f.Invoke) t l
open System.Runtime.CompilerServices
// Additional member functions intended for c# code
[<Extension>]
type DataLensExtensions=
    /// combine two lenses into one where the two lenses operates on the same "record" type
    [<Extension>] static member Combine (l1:IDataLens<'T, 'U>,l2:IDataLens<_, _>) = DataLens.combine (DataLens.coerce l1) (DataLens.coerce l2)
    /// Sequentially composes two lenses
    [<Extension>] static member Compose (l1:IDataLens<'T, 'U>,l2:IDataLens<_, _>) = DataLens.compose (DataLens.coerce l1) (DataLens.coerce l2)
    /// Sequentially composes two lenses. Same as compose but in the opposite direction.
    [<Extension>] static member AndThen (l1:IDataLens<'T, 'U>,l2:IDataLens<_, _>) = DataLens.compose (DataLens.coerce l2) (DataLens.coerce l1)
    /// Read the value of t, then apply the function f on that value and set the value into a new instance of t of 'T
    [<Extension>] static member Update (l1:IDataLens<'T, 'U>, f:Func<'U,'U>, t) = DataLens.update (f.Invoke) t (DataLens.coerce l1)
