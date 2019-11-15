namespace With.Lenses
open With
open With.Internals
///Copy of Lens definition from FSharpx.Extras : 
// https://github.com/fsprojects/FSharpx.Extras/blob/master/src/FSharpx.Extras/Lens.fs
type DataLens<'T,'U>={ Get: 'T -> 'U
                       Set: 'U -> 'T -> 'T }
with  
     member l.Update f a = l.Set (f (l.Get a)) a
module DataLens =
   let inline get a (l: DataLens<_,_>) = l.Get a
   let inline set v a (l: DataLens<_,_>) = l.Set v a
   let inline update f (l: DataLens<_,_>) = l.Update f
   let inline combine (l1: DataLens<'t,'v1>) (l2: DataLens<'t,'v2>) = 
        { Get = fun t->(l1.Get t, l2.Get t)
          Set = fun (v1,v2) t ->  l2.Set v2 (l1.Set v1 t) }
   /// Sequentially composes two lenses
   let inline compose (l1: DataLens<_,_>) (l2: DataLens<_,_>) = 
                  { Get = l2.Get >> l1.Get
                    Set = l1.Set >> l2.Update }
   let fieldOrPropertyToLens<'T,'U> v : DataLens<'T,'U>=
       let typ = typeof<'T>
       let n = FieldOrProperty.name v
       { Get = fun t-> FieldOrProperty.value v t :?> 'U
         Set = fun v t -> Reflection.create typ typ t [NameAndValue(n,v)] :?> 'T }
   /// Split a combination of 2 2-tuples into a 3-tuple
   let ofLeftTuple() : DataLens<(('v1*'v2)*'v3),('v1*'v2*'v3)>=
       { Get = fun ((v1,v2),v3)->(v1,v2,v3)
         Set = fun (v1,v2,v3) _ -> ((v1,v2),v3)}
   /// Split a combination of 2 2-tuples into a 3-tuple
   let ofLeftTuple'() : DataLens<((('v1*'v2)*'v3)*'v4),('v1*'v2*'v3*'v4)>=
        { Get = fun (((v1,v2),v3),v4)->(v1,v2,v3,v4)
          Set = fun (v1,v2,v3,v4) _  ->(((v1,v2),v3),v4)}
   /// Split a combination of 2 2-tuples into a 3-tuple
   let ofRightTuple() : DataLens<('v1*('v2*'v3)),('v1*'v2*'v3)>=
       { Get = fun (v1,(v2,v3))->(v1,v2,v3)
         Set = fun (v1,v2,v3) _ -> (v1,(v2,v3))}
type DataLens<'T,'U> with
    member l1.Combine l2 = DataLens.combine l1 l2
    /// Sequentially composes two lenses
    member l1.Compose l2 = DataLens.compose l1 l2
    /// 
    member l.ToPreparedCopy ()=
            { new IPreparedCopy<'T,'U> 
              with 
                member __.Copy(t,v1)= DataLens.set v1 t l }
    /// Set value and return result
    member l.Write (t, v) = DataLens.set v t l
    /// Get value out lens
    member l.Read (t) = DataLens.get t l

