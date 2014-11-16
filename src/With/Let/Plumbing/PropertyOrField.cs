using System;
using System.Reflection;
using System.Linq.Expressions;

namespace With.Let.Plumbing
{
	/// <summary>
	/// From automapper, moq
	/// </summary>
	internal class PropertyOrField
	{

		private readonly object target;
		private readonly MemberInfo member;

		public PropertyOrField(object target, MemberInfo member)
		{
			this.target = target;
			this.member = member;
		}

		public void SetMemberValue(object value)
		{
			OnFieldOrProperty(member,
				fi => fi.SetValue (target, value),
				pi => pi.SetValue (target, value, null)
			);
		}

		public object GetMemberValue()
		{
			Object value=null;
			OnFieldOrProperty (member,
				fi => value = fi.GetValue (target),
				pi => value = pi.GetValue (target, null)
			);
			return value;
		}

		public bool IsReadonly ()
		{
			var isPrivate=false;
			OnFieldOrProperty(member,
				fi => isPrivate= fi.IsLiteral||fi.IsInitOnly||fi.IsPrivate,
				pi => {}
			);
			return isPrivate;
		}

		void OnFieldOrProperty (MemberInfo member, Action<FieldInfo> onFieldInfo, Action<PropertyInfo> onPropertyInfo)
		{
			switch (member.MemberType)
			{
			case MemberTypes.Field:
				onFieldInfo((FieldInfo)member);
				break;
			case MemberTypes.Property:
				onPropertyInfo((PropertyInfo)member);
				break;
			default:
				throw new Exception(String.Format("Unexpected member type encountered: {0}",member.MemberType));
			}
		}
	}
}

