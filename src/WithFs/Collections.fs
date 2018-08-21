[<CompilationRepresentation (CompilationRepresentationFlags.ModuleSuffix)>]
module With.Collections
open System
open System.Collections
open System.Collections.Generic
open System.Runtime.InteropServices
[<CompiledName("ReadOnlyDictionaryUsage")>]
let readOnlyDictionaryUsage<'TKey,'TValue>(data:IReadOnlyDictionary<'TKey, 'TValue>, onUsage:Action<'TKey, 'TValue>)=
    { new IReadOnlyDictionary<'TKey, 'TValue> with
        member __.ContainsKey(key)=data.ContainsKey key
        member __.TryGetValue(key, ([<Out>]value:byref<'TValue>))=
            let ok, r = data.TryGetValue(key)
            if ok then
                value <- r
                onUsage.Invoke(key, value)
            ok
        member __.Count = data.Count
        member __.Item with get(key) = onUsage.Invoke(key, data.[key])
                                       data.[key]
        member __.Keys = data.Keys
        
        member __.Values = let enumerator ()= let mutable i = -1
                                              let idx = data |> Seq.toArray
                                              let mutable current = KeyValuePair<'TKey, 'TValue>()
                                              let getCurrent () =
                                                  onUsage.Invoke(current.Key, current.Value)
                                                  current.Value
                                              { new IEnumerator<'TValue>
                                                with
                                                    member __.Current with get()= getCurrent() 
                                                    member self.Dispose() = ()
                                                    
                                                interface IEnumerator with
                                                    member __.MoveNext() = 
                                                        i <- i+1
                                                        if i >= idx.Length then 
                                                            false
                                                        else
                                                            current <- idx.[i]
                                                            true
                                                    member __.Reset() = i <- -1
                                                    member __.Current with get()= getCurrent() |> box
                                              }
                           { new IEnumerable<'TValue> 
                             with
                                member __.GetEnumerator() = enumerator ()
                             interface IEnumerable with
                                member __.GetEnumerator() = enumerator () :> IEnumerator
                           }
      interface IEnumerable with member __.GetEnumerator() = raise (NotImplementedException "Usage for IEnumerator not implemented")
      interface IEnumerable<KeyValuePair<'TKey,'TValue>> with member __.GetEnumerator() = raise (NotImplementedException "Usage for IEnumerator not implemented")
    }
