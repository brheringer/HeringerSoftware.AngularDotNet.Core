using System;

namespace MetalSoft.Core.CodeGenerator.Model
{
	public class Property
	{
		public Entity Owner { get; private set; }
		public string Name { get; set; }
		public string Label { get; set; }
		public string Tip { get; set; }
		public string Type { get; set; }
		public bool Required { get; set; }
		public bool CriterionForEquals { get; set; }
		public bool IsEnum { get; set; }
		public bool IsContainer { get; set; }
		public string ForeignKeyFieldName { get; set; }
		public Entity CompositionInfo { get; set; }

		public bool IsEntityReference
		{
			get
			{
				//TODO rever
				var x = new System.Collections.Specialized.StringCollection() { "bool", "boolean", "datetime", "decimal", "float", "int", "short", "number", "string", "stringclob" };
				return !x.Contains(this.Type.ToLower())
					&& !IsCollection
					&& !IsEnum;
				//var type = System.Type.GetType(this.Type);
				//return type == null
				//	|| (!type.IsPrimitive 
				//	&& !type.IsValueType
				//	&& !(type is System.Collections.ICollection));
			}
		}

		public bool IsCollection
		{
			get
			{
				return this.Type.Contains("<"); //ex: IList<object>
			}
		}

		public bool IsAutoReference {
			get
			{
				return this.Type == this.Owner.EntityName;
			}
		}

		public bool IsString
		{
			get
			{
				return string.Compare(this.Type, "string", true) == 0;
			}
		}

		public bool IsStringClob
		{
			get
			{
				return string.Compare(this.Type, "StringClob", true) == 0;
			}
		}

		public bool IsDateTime
		{
			get
			{
				return string.Compare(this.Type, "DateTime", true) == 0;
			}
		}

		public Property(Entity owner)
		{
			if (owner == null)
				throw new ArgumentNullException();
			this.Owner = owner;
		}

		public string GetParameterizedType()
		{
			string type = string.Empty;
			if (this.IsCollection)
			{
				int i = this.Type.IndexOf("<");
				int j = this.Type.IndexOf(">");
				type = this.Type.Substring(i + 1, j - i -1);
				//or this.CompositionInfo.EntityName;
			}
			return type;
		}
	}
}
