using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HeringerSoftware.AngularDotNet.Core.Model
{
	public static class EntityHelper
	{
		public static int GetHashCode(object obj1)
		{
			return Tuple.Create(obj1).GetHashCode();
		}

		public static int GetHashCode(object obj1, object obj2)
		{
			return Tuple.Create(obj1, obj2).GetHashCode();
		}

		public static int GetHashCode(object obj1, object obj2, object obj3)
		{
			return Tuple.Create(obj1, obj2, obj3).GetHashCode();
		}

		public static int GetHashCode(object obj1, object obj2, object obj3, object obj4)
		{
			return Tuple.Create(obj1, obj2, obj3, obj4).GetHashCode();
		}

		public static int GetHashCode(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			return Tuple.Create(obj1, obj2, obj3, obj4, obj5).GetHashCode();
		}

		public static int GetHashCode(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			return Tuple.Create(obj1, obj2, obj3, obj4, obj5, obj6).GetHashCode();
		}

		public static string ToString(object obj)
		{
			return obj != null
				? obj.ToString()
				: string.Empty;
		}

		/// <summary>
		/// Analisa as referências e os tipos dos objetos para saber se é possível
		/// garantir que eles são iguais ou se são diferentes. Se os objetos iguais
		/// por referência, então com certeza eles são iguais (retorna true). Se os
		/// objetos têm tipos diferentes, então com certeza são diferentes (retorna
		/// false). Se um objeto é nulo e o outro não é nulo, então com certeza
		/// são diferentes. Outros casos (ref. dif. e tipo igual) retornam nulo.
		/// 
		/// True: obj1 e obj2 são iguais por referência.
		/// False: obj1 e obj2 têm tipos diferentes.
		/// False: um objeto é nulo e o outro não.
		/// </summary>
		/// <param name="obj1"></param>
		/// <param name="obj2"></param>
		/// <remarks>
		/// TODO rever o nome e o propósito desse método
		/// </remarks>
		/// <returns></returns>
		public static bool? EqualsByReferenceByType(object obj1, object obj2)
		{
			bool? igual = null;
			if (obj1 == null ^ obj2 == null)
				igual = false;
			else if (object.ReferenceEquals(obj1, obj2))
				igual = true;
			else if (obj1 is Entity && obj2 is Entity)
			{
				if (((Entity)obj1).GetTypeUnproxied() != ((Entity)obj2).GetTypeUnproxied())
					igual = false;
				//else: return null
			}
			else if (obj1.GetType() != obj2.GetType())
				igual = false;
			return igual;
		}

		public static string GetClassName<T>(Expression<Func<T>> propertyExpression)
		{
			return ParseMemberExpression(propertyExpression).Member.ReflectedType.Name;
		}

		public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
		{
			return ParseMemberExpression(propertyExpression).Member.Name;
		}

		private static MemberExpression ParseMemberExpression<T>(Expression<Func<T>> propertyExpression)
		{
			if (propertyExpression == null)
				throw new ArgumentNullException();

			var memberExpression = propertyExpression.Body as MemberExpression;

			if (memberExpression == null)
				throw new ArgumentException("expression must be a property expression");

			return memberExpression;
		}
	}
}
