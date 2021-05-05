using MetalSoft.Core.CodeGenerator.Model;
using System;
using System.Text;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators
{
	public class DtoCodeGenerator : CodeGenerator
	{
		public override string GetFileName()
		{
			return this.BaseEntity.EntityName + "Dto.cs";
		}

		public override string GenerateCode()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Template);
			sb.Replace("{{APPLICATION}}", this.BaseEntity.Application.Name);
			sb.Replace("{{ENTITY}}", this.BaseEntity.EntityName);
			sb.Replace("{{PROPERTIES}}", GenerateCodeForProperties());
			sb.Replace("{{COLLECTIONS_INITIALIZATION}}", GenerateCodeForConstructor());
			return sb.ToString();
		}

		private string GenerateCodeForProperties()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Property p in this.BaseEntity.Properties)
			{
				if (!p.IsContainer)
				{
					if (sb.Length > 0)
						sb.AppendLine();

					string type = p.Type;
					if (p.IsEnum || p.IsStringClob)
						type = "string";
					else if (p.IsEntityReference)
						type = "EntityReferenceDto";
					else if (p.IsCollection)
						type = InsertDtoSufix(type);

					sb.AppendFormat("\t\tpublic {0} {1} {{ get; set; }}", type, p.Name);
				}
			}
			return sb.ToString();
		}

		private static string InsertDtoSufix(string type)
		{
			return type.Insert(type.IndexOf(">"), "Dto");
		}

		private string GenerateCodeForConstructor()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Property p in this.BaseEntity.Properties)
			{
				if (sb.Length > 0)
					sb.AppendLine();

				if (p.IsCollection)
				{
					sb.AppendFormat("\t\t\tthis.{0} = new {1}();", 
						p.Name, 
						GetConcreteType(InsertDtoSufix(p.Type)));
				}
			}
			return sb.ToString();
		}

		private string GetConcreteType(string itype)
		{
			//TODO rever, refatorar
			return itype.StartsWith("I")
				? itype.Substring(1)
				: itype;
		}
	}
}