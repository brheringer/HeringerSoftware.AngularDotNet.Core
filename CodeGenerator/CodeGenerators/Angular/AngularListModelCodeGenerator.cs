using MetalSoft.Core.CodeGenerator.Model;
using System;
using System.Linq;
using System.Text;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators.Angular
{
	public class AngularListModelCodeGenerator : CodeGenerator
	{
		public override string GetFileName()
		{
			return AngularNormalizer.NormalizeFileName(this.BaseEntity.CollectionName) + ".model.ts";
		}

		public override string GenerateCode()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Template);
			sb.Replace("{{ENTITY}}", this.BaseEntity.EntityName);
			sb.Replace("{{LOWER_ENTITY}}", AngularNormalizer.NormalizeFileName(this.BaseEntity.EntityName));
			sb.Replace("{{COLLECTION}}", this.BaseEntity.CollectionName);
			sb.Replace("{{FILTERS}}", GenerateCodeForFilters());
			return sb.ToString();
		}

		private string GenerateCodeForFilters()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Property p in this.BaseEntity.Properties)
			{
				if (!p.IsCollection && !p.IsStringClob)
				{
					if (sb.Length > 0)
						sb.AppendLine();
					if (p.IsEnum)
						sb.AppendFormat($"  filter{p.Name}: string;");
					else if(p.IsEntityReference)
						sb.AppendFormat($"  filter{p.Name}: EntityReference;");
					else
						sb.AppendFormat("  filter{0}: {1};",
							p.Name,
							AngularNormalizer.NormalizeTypeNameFromCSharp(p.Type));
				}
			}
			return sb.ToString();
		}

	}
}