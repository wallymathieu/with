using System;
using System.Runtime.CompilerServices;

namespace With
{
    /// <summary>
    /// Helper methods for enums. Would be nice if this was in .net base classes instead.
    /// </summary>
    public static class Enums
    {
        /// <summary>
        /// Converts the string representation of the name or numeric value of one 
        /// or more enumerated constants to an equivalent enumerated object.
        /// Throws an format exception if the value can't parse the value.
        /// </summary>
        /// <param name="value">The string representation of the enumeration name or underlying value to convert.</param>
        /// <typeparam name="TEnum">An enumeration type.</typeparam>
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static TEnum Parse<TEnum> (string value) where TEnum : struct =>
            (Enum.TryParse<TEnum> (value, out var result)) ? result : throw new FormatException ($"Couldn't parse {value}");

        /// <summary>
        /// Converts the string representation of the name or numeric value of one 
        /// or more enumerated constants to an equivalent enumerated object.
        /// A parameter specifies whether the operation is case-sensitive.
        /// Throws an format exception if the value can't parse the value.
        /// </summary>
        /// <param name="value">The string representation of the enumeration name or underlying value to convert.</param>
        /// <typeparam name="TEnum">An enumeration type.</typeparam>
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static TEnum Parse<TEnum> (string value, bool ignoreCase) where TEnum : struct =>
            (Enum.TryParse<TEnum> (value, ignoreCase, out var result)) ? result : throw new FormatException ($"Couldn't parse {value}");

        /// <summary>
        /// A string containing the name of the enumerated constant in enumType whose value is value; or null if no such constant is found.
        /// </summary>
        /// <returns>The name.</returns>
        /// <param name="value">The value of a particular enumerated constant in terms of its underlying type.</param>
        /// <typeparam name="TEnum">An enumeration type.</typeparam>
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static string GetName<TEnum> (TEnum value) where TEnum : struct =>
            Enum.GetName (typeof (TEnum), value);

    }
}
