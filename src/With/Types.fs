namespace With
open System

type ExpectedButGotException(expected:string array, got:string) =
  inherit Exception(String.Format("Expected {0} but got {1}", String.Join(", ", expected), got))
type ExpectedButGotException<'t>(expected:'t array, got:'t) =
  inherit ExpectedButGotException(expected |> Array.map (fun e->e.ToString()), got.ToString())

type NameAndValue = System.Collections.Generic.KeyValuePair<string, obj>

type MissingValueException(name:string)=
    inherit Exception(String.Format("Missing value named: {0}", name))

type MissingConstructorParameterException(name:string)=
    inherit Exception(String.Format("Missing constructor parameter for property named: {0}", name))

open System.Runtime.CompilerServices

/// <summary>
/// Common extensions
/// </summary>
[<Extension>]
type CommonExtensions=
    /// <summary>
    /// Execute an action on the object. Return value is the object.
    /// </summary>
    [<Extension>]
    static member Tap<'T>(value:'T, action:Action<'T>) : 'T=
        let isNotNull = not << isNull
        if isNotNull action then action.Invoke(value)
        value

    /// <summary>
    /// Execute a func on the object. Return value is the result from the func.
    /// </summary>
    [<Extension>]
    static member Yield<'T, 'TResult>(value:'T, func:Func<'T, 'TResult>) = func.Invoke(value)
/// <summary>
/// Helper methods for enums. Would be nice if this was in .net base classes instead.
/// There is an open issue for it: https://github.com/dotnet/corefx/issues/15453
/// </summary>
type Enums=
    /// <summary>
    /// Converts the string representation of the name or numeric value of one
    /// or more enumerated constants to an equivalent enumerated object.
    /// Throws an format exception if the value can't parse the value.
    /// </summary>
    /// <param name="value">The string representation of the enumeration name or underlying value to convert.</param>
    /// <typeparam name="TEnum">An enumeration type.</typeparam>
    [<MethodImpl (MethodImplOptions.AggressiveInlining)>]
    static member Parse (value:string): 'TEnum =
        let result = ref (Unchecked.defaultof<'TEnum>)
        if Enum.TryParse<'TEnum> (value, false, result)
        then result.Value
        else raise (FormatException (sprintf "Couldn't parse %s" value))

    /// <summary>
    /// Converts the string representation of the name or numeric value of one
    /// or more enumerated constants to an equivalent enumerated object.
    /// A parameter specifies whether the operation is case-sensitive.
    /// Throws an format exception if the value can't parse the value.
    /// </summary>
    /// <param name="value">The string representation of the enumeration name or underlying value to convert.</param>
    /// <param name="ignoreCase"></param>
    /// <typeparam name="TEnum">An enumeration type.</typeparam>
    [<MethodImpl (MethodImplOptions.AggressiveInlining)>]
    static member Parse (value:string, ignoreCase:bool): 'TEnum =
        let result = ref (Unchecked.defaultof<'TEnum>)
        if Enum.TryParse<'TEnum> (value, ignoreCase, result)
        then result.Value
        else raise (FormatException (sprintf "Couldn't parse %s" value))

    /// <summary>
    /// A string containing the name of the enumerated constant in enumType whose value is value; or null if no such constant is found.
    /// </summary>
    /// <returns>The name.</returns>
    /// <param name="value">The value of a particular enumerated constant in terms of its underlying type.</param>
    /// <typeparam name="TEnum">An enumeration type.</typeparam>
    [<MethodImpl (MethodImplOptions.AggressiveInlining)>]
    static member GetName(value:'TEnum) =
        Enum.GetName (typeof<'TEnum>, value)
/// <summary>
/// Uri extensions
/// </summary>
type Uris=
    /// <summary>
    /// Create a new Uri based on base url and relative path. Throws an
    /// argument exception if the values can't composed to an Uri.
    /// </summary>
    static member Create (baseUrl:string, relativePath:string):Uri=
        let res = ref null
        if (Uri.TryCreate (Uri (baseUrl), relativePath, res)) then res.Value
        else
            raise (ArgumentException (sprintf "Could not create uri! baseUrl: %s relativePath: %s" baseUrl relativePath))
