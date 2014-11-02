using System;
using System.Reflection;
using System.Linq.Expressions;

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
			public PropertyExpression<TObject,TReturnValue> Property<TReturnValue>( Expression<Func<TObject,TReturnValue>> property)
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
			public Let<TObject, TReturnValueType> Be (TReturnValueType value)
			{
				return new Let<TObject, TReturnValueType> (Object, Property, value);
			}
		}

		public class OfPropertyExpression<T>
		{
			private readonly Expression<Func<T>> Property;
			public OfPropertyExpression (Expression<Func<T>> property)
			{
				Property = property;
			}
			public Let<Object, T> Be (T value)
			{
				return new Let<Object, T> (Property,value);
			}
		}

		public static OfExpression<TObject> Of<TObject>(TObject obj)
		{
			return new OfExpression<TObject> (obj);
		}

		public static OfPropertyExpression<T> Of<T>(Expression<Func<T>> property)
		{
			return new OfPropertyExpression<T> (property);
		}
	}

	/// <summary>
	/// This code is mostly fetched from automapper and moq.
	/// </summary>
	public class Let<TObj,T> : IDisposable
	{
		private readonly PropertyOrAttribute _property;
		private readonly object _oldvalue;

		public Let(TObj target, Expression<Func<TObj,T>> property, T value)
		{
			_property = new PropertyOrAttribute(target,property);
			_oldvalue = _property.GetMemberValue();
			_property.SetMemberValue(value);
		}
		public Let(Expression<Func<T>> property, T value)
		{
			_property = new PropertyOrAttribute(null, property);
			_oldvalue = _property.GetMemberValue();
			_property.SetMemberValue(value);
		}

		public void Dispose()
		{
			_property.SetMemberValue(_oldvalue);
		}

		/// <summary>
		/// From automapper, moq
		/// </summary>
		private class PropertyOrAttribute
		{
			private readonly object target;
			private readonly MemberInfo member;

			public PropertyOrAttribute(object target, LambdaExpression lambdaExpression)
			{
				this.target = target;
				this.member = FindProperty(lambdaExpression);

			}

			private static MemberInfo FindProperty(LambdaExpression lambdaExpression)
			{
				Expression expressionToCheck = lambdaExpression;

				bool done = false;

				while (!done)
				{
					switch (expressionToCheck.NodeType)
					{
					case ExpressionType.Convert:
						expressionToCheck = ((UnaryExpression)expressionToCheck).Operand;
						break;
					case ExpressionType.Lambda:
						expressionToCheck = ((LambdaExpression)expressionToCheck).Body;
						break;
					case ExpressionType.MemberAccess:
						var memberExpression = ((MemberExpression)expressionToCheck);

						if (memberExpression.Expression != null)
						{
							if (memberExpression.Expression.NodeType != ExpressionType.Parameter &&
								memberExpression.Expression.NodeType != ExpressionType.Convert)
							{
								throw new ArgumentException(String.Format("Expression '{0}' must resolve to top-level member.", 
									lambdaExpression), "lambdaExpression");
							}
						}

						MemberInfo member = memberExpression.Member;

						return member;
					default:
						done = true;
						break;
					}
				}
				throw new ArgumentException("Failed!");
			}

			public void SetMemberValue(object value)
			{
				switch (member.MemberType)
				{
				case MemberTypes.Field:
					((FieldInfo)member).SetValue(target, value);
					break;
				case MemberTypes.Property:
					((PropertyInfo)member).SetValue(target, value, null);
					break;
				default:
					throw new ArgumentException("member");
				}
			}

			public object GetMemberValue()
			{
				switch (member.MemberType)
				{
				case MemberTypes.Field:
					return ((FieldInfo)member).GetValue(target);
				case MemberTypes.Property:
					return ((PropertyInfo)member).GetValue(target, null);
				default:
					throw new ArgumentException("member");
				}
			}
		}
	}
}

