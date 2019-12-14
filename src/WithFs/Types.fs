namespace With
open System

type ExpectedButGotException(expected:string array, got:string) =
  inherit Exception(String.Format("Expected {0} but got {1}", String.Join(", ", expected), got))
type ExpectedButGotException<'t>(expected:'t array, got:'t) =
  inherit ExpectedButGotException(expected |> Array.map (fun e->e.ToString()), got.ToString())
/// base interface for prepared copy, essentially a write only lens of specified number of parameters
type IPreparedCopy=interface end
/// PreparedCopy returns a new instance based on the target instance
/// but with the property set the parameter value
type IPreparedCopy<'T, 'TValue> = 
    inherit IPreparedCopy
    abstract member Copy : 'T * 'TValue -> 'T
/// PreparedCopy returns a new instance based on the target instance
/// but with the property set the parameter values
type IPreparedCopy<'T, 'TValue1, 'TValue2> =
    inherit IPreparedCopy
    abstract member Copy : 'T * 'TValue1 * 'TValue2 -> 'T
/// PreparedCopy returns a new instance based on the target instance
/// but with the property set the parameter values
type IPreparedCopy<'T, 'TValue1, 'TValue2, 'TValue3> =
    inherit IPreparedCopy
    abstract member Copy : 'T * 'TValue1 * 'TValue2 * 'TValue3 -> 'T
/// PreparedCopy returns a new instance based on the target instance
/// but with the property set the parameter values
type IPreparedCopy<'T, 'TValue1, 'TValue2, 'TValue3, 'TValue4> =
    inherit IPreparedCopy
    abstract member Copy : 'T * 'TValue1 * 'TValue2 * 'TValue3 * 'TValue4 -> 'T
type NameAndValue = System.Collections.Generic.KeyValuePair<string, obj>

type MissingValueException(name:string)=
    inherit Exception(String.Format("Missing value named: {0}", name))

type MissingConstructorParameterException(name:string)=
    inherit Exception(String.Format("Missing constructor parameter for property named: {0}", name))
