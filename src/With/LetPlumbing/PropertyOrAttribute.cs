using System;
using System.Reflection;
using System.Linq.Expressions;

namespace With.LetPlumbing
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

