using System;
using System.Linq;
using System.Collections.Generic;
using NameAndValue = System.Collections.Generic.KeyValuePair<string,object>;

namespace With.WithPlumbing
{
    using Reflections;
    using Collections;
   
    internal static class CreateInstanceFromValues
    {
        public static TDestination Create<TDestination>(Object parent, IEnumerable<NameAndValue> values)
        {
            return (TDestination)Create(parent.GetType(), typeof(TDestination), parent, values);
        }
        public static TDestination Create<TSource,TDestination>(TSource parent, IEnumerable<NameAndValue> values)
        {
            return (TDestination)Create(typeof(TSource), typeof(TDestination), parent, values);
        }
        public static Object Create(Type tSource, Type tDest, Object parent, IEnumerable<NameAndValue> values)
        {
            var props = tSource.GetFieldsOrProperties();
            var ctor = Internals.Reflection.GetConstructorWithMostParameters.Invoke(tDest);

            var usedKeys = new List<string> ();

            var dictionaryOfParameters = new ReadOnlyDictionaryUsage<string, object> (
                values.ToDictionary (
                    nameAndValue => nameAndValue.Key,
                    nameAndValue => nameAndValue.Value,
                    StringComparer.CurrentCultureIgnoreCase),
                (key,value)=>usedKeys.Add(key)
            );
            var instance = ctor.Invoke (
                GetConstructorParameterValues.GetValues(parent, 
                    dictionaryOfParameters, props, ctor)
            );
            var unusedKeys = dictionaryOfParameters.Keys.Except(usedKeys,
                StringComparer.CurrentCultureIgnoreCase).ToArray();
            if (unusedKeys.Any ()) 
            {
                throw new Exception(string.Format("Missing constructor parameters on '{1}' for: [{0}]", string.Join(",", unusedKeys), tDest.Name));
            }
            return instance;
        }
    }
}

