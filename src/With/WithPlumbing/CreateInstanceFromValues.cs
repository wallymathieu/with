using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using NameAndValue = System.Collections.Generic.KeyValuePair<string,object>;

namespace With.WithPlumbing
{
    using Reflection;
    using Collections;
   
    internal class CreateInstanceFromValues<TDestination>
    {
        private readonly FieldOrProperty[] props;
        private readonly ConstructorInfo ctor;
        public CreateInstanceFromValues()
            :this(typeof(TDestination))
        {
        }
        public CreateInstanceFromValues (Type tSource)
        {
            this.props = tSource.GetFieldsOrProperties();
            ctor = typeof(TDestination).GetConstructorWithMostParameters();
        }

        public TDestination Create(Object parent, IEnumerable<NameAndValue> values)
        {
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
                StringComparer.CurrentCultureIgnoreCase);
            if (unusedKeys.Any ()) 
            {
                throw new Exception(string.Format("Missing constructor parameters for: [{0}]", string.Join(",", unusedKeys)));
            }
            return (TDestination)instance;
        }
    }
}

