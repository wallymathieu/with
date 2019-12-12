namespace With
open System

type ExpectedButGotException(expected:string array, got:string) =
  inherit Exception(String.Format("Expected {0} but got {1}", String.Join(", ", expected), got))
type ExpectedButGotException<'t>(expected:'t array, got:'t) =
  inherit ExpectedButGotException(expected |> Array.map (fun e->e.ToString()), got.ToString())

type NameAndValue = System.Collections.Generic.KeyValuePair<string, obj>

type MissingValueException(name:string)=
    inherit Exception(String.Format("Missing value named: {0}", name))
