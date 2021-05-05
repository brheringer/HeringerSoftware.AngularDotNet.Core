using System;

namespace HeringerSoftware.AngularDotNet.Core.Model
{
	public class InvalidPropertyValueException
		: PropertyModelException
	{
		public string InvalidValue { get; set; }

		public string ExpectedRule { get; set; }

		public InvalidPropertyValueException(string entity, string property, string value, string rule)
			: this(entity, property, value, rule, null)
		{
		}

		public InvalidPropertyValueException(string entity, string property, string value, string rule, Exception inner)
			: base(Describe(entity, property, value, rule), entity, property, inner)
		{
			this.InvalidValue = value;
			this.ExpectedRule = rule;
		}

		private static string Describe(string entity, string property, string value, string rule)
		{
			string description = string.Format("Invalid value at {0}: {1}. ", property, value);
			if (!string.IsNullOrEmpty(rule))
			{
				description += "Rule: " + rule + ".";
			}
			return description;
		}

	}
}
