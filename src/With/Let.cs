using System;
using System.Linq.Expressions;
using With.LetPlumbing;
namespace With
{
	/// <summary>
	/// Scoped assignment. This is a bit weird, but it's basically to be able to set some property for using scope (or for testing).
	/// </summary>
	public class Let
	{
		public class OfExpression<TObject>
		{
			private readonly TObject Object;
			public OfExpression (TObject obj)
			{
				Object = obj;
			}
			public PropertyExpression<TObject,TReturnValue> Member<TReturnValue>( Expression<Func<TObject,TReturnValue>> property)
			{
				return new PropertyExpression<TObject,TReturnValue> (Object, property);
			}
		}

		public class PropertyExpression<TObject,TReturnValueType>
		{
			private readonly TObject Object;
			private readonly Expression<Func<TObject,TReturnValueType>> Property;
			public PropertyExpression (TObject obj, Expression<Func<TObject,TReturnValueType>> property)
			{
				Object = obj;
				Property = property;
			}
			public LetValueBeContext<TObject, TReturnValueType> Be (TReturnValueType value)
			{
				return new LetValueBeContext<TObject, TReturnValueType> (Object, Property, value);
			}
		}

		public class OfPropertyExpression<T>
		{
			private readonly Expression<Func<T>> Property;
			public OfPropertyExpression (Expression<Func<T>> property)
			{
				Property = property;
			}
			public LetValueBeContext<Object, T> Be (T value)
			{
				return new LetValueBeContext<Object, T> (Property,value);
			}
		}

		public static OfExpression<TObject> Object<TObject>(TObject obj)
		{
			return new OfExpression<TObject> (obj);
		}

		public static OfPropertyExpression<T> Member<T>(Expression<Func<T>> property)
		{
			return new OfPropertyExpression<T> (property);
		}
	}

}

