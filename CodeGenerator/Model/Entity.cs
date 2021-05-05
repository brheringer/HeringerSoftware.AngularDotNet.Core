using System;
using System.Collections.Generic;

namespace MetalSoft.Core.CodeGenerator.Model
{
	public class Entity
	{
		public Application Application { get; set; }
		public string EntityName { get; set; }
		public string EntityLabel { get; set; }
		public string CollectionName { get; set; }
		public string CollectionLabel { get; set; }
		public List<Property> Properties { get; set; }
		public bool IsComposition { get; set; }

		public Entity()
		{
			this.Properties = new List<Property>();
		}
	}
}
