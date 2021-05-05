using System;

namespace HeringerSoftware.AngularDotNet.Core.Model
{
	public class RequiredPropertyException
		: PropertyModelException
	{
		public RequiredPropertyException(string entity, string property)
			: base(Describe(entity, property), entity, property)
		{
		}

		private static string Describe(string entity, string property)
		{
			return string.Format("Required field: {0}: {1}", entity, property);
		}
	}
}
