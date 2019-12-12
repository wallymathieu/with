namespace With.Lenses
open System
open With
/// Copy of Lens definition from <a href="https://github.com/fsprojects/FSharpx.Extras/blob/master/src/FSharpx.Extras/Lens.fs">FSharpx.Extras</a>
/// A lens is sort of like a property for immutable data on steroids.
/// You can compose and combine lenses 
type DataLens<'T, 'U> =
    { /// Get value from 'T
      Get: 'T -> 'U
      /// Get a new instance of 'T with value set to the first parameter
      Set: 'U -> 'T -> 'T }
with
    /// Read the value of t, then apply the function f on that value and set the value into a new instance of t of 'T
    member l.Update f t = l.Set (f (l.Get t)) t
    static member Create(get:Func<'T,'U>,set:Func<'T,'U,'T>)={ Get=(fun t-> get.Invoke(t)); Set = fun u t -> set.Invoke(t,u) }
type internal DataLens = DataLens<obj, obj>

module DataLens =
    /// Get value from 'T
    let inline get a (l: DataLens<_, _>) = l.Get a
    /// Get a new instance of 'T with value set to the first parameter
    let inline set v a (l: DataLens<_, _>) = l.Set v a
    /// Read the value of t, then apply the function f on that value and set the value into a new instance of t of 'T
    let inline update f (l: DataLens<_, _>) = l.Update f
    /// combine two lenses into one where the two lenses operates on the same "record" type
    let inline combine (l1: DataLens<'t, 'v1>) (l2: DataLens<'t, 'v2>) =
        { Get = fun t -> struct(l1.Get t, l2.Get t)
          Set = fun struct(v1, v2) t -> l2.Set v2 (l1.Set v1 t) }
    /// Sequentially composes two lenses. Can be used to "drill down" into an object grap.
    /// For instance to be able to do something <c> t.Customer.Name = v </c> for immutable data.
    let inline compose (l1: DataLens<'TU, 'U>) (l2: DataLens<'T, 'TU>) =
        { Get = l2.Get >> l1.Get
          Set = l1.Set >> l2.Update }
    /// Sequentially composes two lenses in an untyped manner
    let inline internal composeUntyped (l1: DataLens) (l2: DataLens) = compose l1 l2
    /// Split a 2-tuples into a 3-tuple
    let ofTuple(): DataLens<struct(struct('v1 * 'v2) * 'v3),struct( 'v1 * 'v2 * 'v3)> =
        { Get = fun struct(struct(v1, v2), v3) -> (v1, v2, v3)
          Set = fun struct(v1, v2, v3) _ -> ((v1, v2), v3) }

    /// Split a cons combination of 2 2-tuples into a 3-tuple
    let ofLeftTuple(): DataLens<struct(struct('v1 * 'v2) * 'v3), struct( 'v1 * 'v2 * 'v3)> =
        { Get = fun struct(struct(v1, v2), v3) -> (v1, v2, v3)
          Set = fun struct(v1, v2, v3) _ -> ((v1, v2), v3) }
    /// Split a cons combination of 2 2-tuples into a 4-tuple
    let ofLeftTuple2(): DataLens<struct(struct(struct('v1 * 'v2) * 'v3) * 'v4), struct('v1 * 'v2 * 'v3 * 'v4)> =
        { Get = fun struct(struct(struct(v1, v2), v3), v4) -> (v1, v2, v3, v4)
          Set = fun struct(v1, v2, v3, v4) _ -> (((v1, v2), v3), v4) }
    /// Split a cons combination of 3 2-tuples into a 5-tuple
    let ofLeftTuple3(): DataLens<struct(struct(struct(struct('v1 * 'v2) * 'v3) * 'v4)*'v5), struct('v1 * 'v2 * 'v3 * 'v4 * 'v5)> =
        { Get = fun struct(struct(struct(struct(v1, v2), v3), v4),v5) -> (v1, v2, v3, v4, v5)
          Set = fun struct(v1, v2, v3, v4, v5) _ -> ((((v1, v2), v3), v4), v5) }
    /// Split a cons combination of 4 2-tuples into a 6-tuple
    let ofLeftTuple4(): DataLens<struct(struct(struct(struct(struct('v1 * 'v2) * 'v3) * 'v4)*'v5)*'v6), struct('v1 * 'v2 * 'v3 * 'v4 * 'v5 * 'v6)> =
        { Get = fun struct(struct(struct(struct(struct(v1, v2), v3), v4),v5),v6) -> (v1, v2, v3, v4, v5,v6)
          Set = fun struct(v1, v2, v3, v4, v5, v6) _ -> (((((v1, v2), v3), v4),v5),v6) }
    /// Split a cons combination of 5 2-tuples into a 7-tuple
    let ofLeftTuple5(): DataLens<struct(struct(struct(struct(struct(struct('v1 * 'v2) * 'v3) * 'v4)*'v5)*'v6)*'v7), struct('v1 * 'v2 * 'v3 * 'v4 * 'v5 * 'v6 * 'v7)> =
        { Get = fun struct(struct(struct(struct(struct(struct(v1, v2), v3), v4),v5),v6),v7)-> (v1, v2, v3, v4, v5,v6, v7)
          Set = fun struct(v1, v2, v3, v4, v5,v6, v7) _ -> ((((((v1, v2), v3), v4),v5),v6),v7) }
    /// Split a cons combination of 6 2-tuples into a 8-tuple
    let ofLeftTuple6(): DataLens<struct(struct(struct(struct(struct(struct(struct('v1 * 'v2) * 'v3) * 'v4)*'v5)*'v6)*'v7)*'v8), struct('v1 * 'v2 * 'v3 * 'v4 * 'v5 * 'v6 * 'v7 * 'v8)> =
        { Get = fun struct(struct(struct(struct(struct(struct(struct(v1, v2), v3), v4),v5),v6),v7),v8)-> (v1, v2, v3, v4, v5,v6, v7,v8)
          Set = fun struct(v1, v2, v3, v4, v5,v6, v7,v8) _ -> (((((((v1, v2), v3), v4),v5),v6),v7),v8) }
    /// Split a cons combination of 2 2-tuples into a 3-tuple
    let ofRightTuple(): DataLens<struct('v1 * struct('v2 * 'v3)), struct('v1 * 'v2 * 'v3)> =
        { Get = fun struct(v1, struct(v2, v3)) -> (v1, v2, v3)
          Set = fun struct(v1, v2, v3) _ -> (v1, (v2, v3)) }
    /// Unbox an untyped lens into a typed lens
    let internal unbox<'T, 'U> (l: DataLens): DataLens<'T, 'U> =
        { Get = fun t -> unbox (l.Get(box t))
          Set = fun v t -> unbox (l.Set (box v) (box t)) }

type DataLens<'T, 'U> with
    /// combine two lenses into one where the two lenses operates on the same "record" type
    member l1.Combine l2 = DataLens.combine l1 l2
    /// Sequentially composes two lenses
    member l1.Compose l2 = DataLens.compose l1 l2
    /// Set value and return result
    member l.Write(t, v) = DataLens.set v t l
    /// Get value out lens
    member l.Read(t) = DataLens.get t l
