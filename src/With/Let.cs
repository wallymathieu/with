﻿using System;
using System.Linq.Expressions;
using With.LetPlumbing;
namespace With
{
	/// <summary>
	/// Scoped assignment. This is a bit weird, but it's basically to be able to set some property for using scope (or for testing).
	/// </summary>
	public class Let
	{
		public static LetObject<TObject> Object<TObject>(TObject obj)
		{
			return new LetObject<TObject> (obj);
		}

		public static LetMember<T> Member<T>(Expression<Func<T>> property)
		{
			return new LetMember<T> (property);
		}
	}

}

