using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using With.Collections;
using With.Reflection;

namespace With.WithPlumbing
{
    class ApplyOperation
    {
        Dictionary<string, Func<object, object[], object>> operations = new Dictionary<string, Func<object, object[], object>>()
        {
            { "Add", DoAdd },
            { "AddRange", DoAddRange },
            { "Remove", DoRemove },
            { "Replace", DoReplace }
        };

        public object Apply(MethodCallExpression expr, object memberValue, object[] paramValues)
        {
            var name = expr.Method.Name;
            if (!operations.ContainsKey(name))
            {
                throw new ExpectedButGotException(operations.Keys.ToArray(),
                    name);
            }
            return operations[name](memberValue, paramValues);
        }

        private static object DoReplace(object memberValue, object[] paramValues)
        {
            object value;
            switch (paramValues.Length)
            {
                case 2:
                    value = memberValue.ToDictionaryT().Tap(d =>
                    {
                        d[paramValues[0]] = paramValues[1];
                    }).ToDictionary();
                    break;
                default:
                    throw new NotImplementedException();
            }
            return value;
        }

        private static object DoAdd(object memberValue, object[] paramValues)
        {
            object value;
            switch (paramValues.Length)
            {
                case 1:
                    value = ((IEnumerable)memberValue).ToListT().Tap(l => 
                        l.Add(paramValues[0]));
                    break;
                case 2:
                    value = memberValue.ToDictionaryT().Tap(d =>
                        d.Add(paramValues[0], paramValues[1])
                    ).ToDictionary();
                    break;
                default:
                    throw new NotImplementedException();
            }
            return value;
        }
        private static object DoAddRange(object memberValue, object[] paramValues)
        {
            var l = ((IEnumerable)memberValue).ToListT();
            foreach (var item in (IEnumerable)paramValues.Single())
            {
                l.Add(item);
            }
            return l;
        }
        private static object DoRemove(object memberValue, object[] paramValues)
        {
            switch (paramValues.Length)
            {
                case 1:
                    if (memberValue.GetType().IsDictionaryType())
                    {
                        return memberValue.ToDictionaryT().Tap(d =>
                            d.Remove(paramValues[0])
                        ).ToDictionary();
                    }
                    else
                    {
                        return ((IEnumerable)memberValue).ToListT().Tap(l =>
                            l.Remove(paramValues[0])
                        );
                    }
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
