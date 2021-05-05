using MetalSoft.Core.CodeGenerator.Model;
using System;
using System.Text;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators
{
	public class CollectionDtoCodeGenerator : CodeGenerator
	{
		public override string GetFileName()
		{
			return this.BaseEntity.CollectionName + "Dto.cs";
		}

		public override string GenerateCode()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Template);
			sb.Replace("{{APPLICATION}}", this.BaseEntity.Application.Name);
			sb.Replace("{{ENTITY}}", this.BaseEntity.EntityName);
			sb.Replace("{{COLLECTION}}", this.BaseEntity.CollectionName);
			sb.Replace("{{PROPERTIES}}", GenerateCodeForProperties());
			return sb.ToString();
		}

		private string GenerateCodeForProperties()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Property p in this.BaseEntity.Properties)
			{
				if (!p.IsCollection && !p.IsStringClob)
				{
					if (sb.Length > 0)
						sb.AppendLine();

					string type = p.Type;
					if (p.IsEnum)
						type = "string";
					else if (p.IsEntityReference)
						type = "EntityReferenceDto";
					else if(!p.IsString)
						type = p.Type + "?";

					sb.AppendFormat("\t\tpublic {0} Filter{1} {{ get; set; }}", type, p.Name);
				}
			}
			return sb.ToString();
		}
	}
}