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

[<Extension>]
type PreparedCopies() =
    [<Extension>]
    static member Copy (self: IPreparedCopy<'T, Tuple<'TValue1,'TValue2>>, t:'T, value1:'TValue1, value2: 'TValue2) =self.Copy(t,Tuple.Create(value1,value2))
    [<Extension>]
    static member Copy (self: IPreparedCopy<'T, Tuple<'TValue1,Tuple<'TValue2,'TValue3>>>, t:'T, value1:'TValue1, value2: 'TValue2, value3: 'TValue3) =self.Copy(t,Tuple.Create(value1,Tuple.Create(value2,value3)))
    [<Extension>]
    static member Copy (self: IPreparedCopy<'T, Tuple<Tuple<'TValue1,'TValue2>,'TValue3>>, t:'T, value1:'TValue1, value2: 'TValue2, value3: 'TValue3) =self.Copy(t,Tuple.Create(Tuple.Create(value1,value2),value3))
