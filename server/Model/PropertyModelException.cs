using System;

namespace HeringerSoftware.AngularDotNet.Core.Model
{
	public class PropertyModelException
		: ModelException
	{
		public string Entity { get; set; }

		public string Property { get; set; }

		public PropertyModelException(string msg, string entity, string property)
			: this(msg, entity, property, null)
		{
		}

		public PropertyModelException(string msg, string entity, string property, Exception innerException)
			: base(msg, innerException)
		{
			this.Entity = entity;
			this.Property = property;
		}

	}
}
