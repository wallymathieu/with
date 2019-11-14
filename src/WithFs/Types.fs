namespace With
open System

type ExpectedButGotException(expected:string array, got:string) =
  inherit Exception(String.Format("Expected {0} but got {1}", String.Join(", ", expected), got))
type ExpectedButGotException<'t>(expected:'t array, got:'t) =
  inherit ExpectedButGotException(expected |> Array.map (fun e->e.ToString()), got.ToString())

type IPreparedCopy=interface end
type IPreparedCopy<'T, 'TValue> = 
    inherit IPreparedCopy
    abstract member Copy : 'T * 'TValue -> 'T
type IPreparedCopy<'T, 'TValue1, 'TValue2> =
    inherit IPreparedCopy
    abstract member Copy : 'T * 'TValue1 * 'TValue2 -> 'T
type IPreparedCopy<'T, 'TValue1, 'TValue2, 'TValue3> =
    inherit IPreparedCopy
    abstract member Copy : 'T * 'TValue1 * 'TValue2 * 'TValue3 -> 'T
type NameAndValue = System.Collections.Generic.KeyValuePair<string, obj>

exception MissingValueException of string
open System.Runtime.CompilerServices

/// Extensions to simplify usage in c#
[<Extension>]
type PreparedCopies() =
    /// Invoke a 2 tupled prepared copy as a function of 2 parameters
    [<Extension>]
    static member Copy (self: IPreparedCopy<'T, Tuple<'U1,'U2>>, t:'T, v1:'U1, v2: 'U2) =self.Copy(t,Tuple.Create(v1,v2))
