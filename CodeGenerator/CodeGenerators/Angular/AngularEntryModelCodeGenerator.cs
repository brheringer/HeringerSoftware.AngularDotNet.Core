using MetalSoft.Core.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators.Angular
{
	public class AngularEntryModelCodeGenerator : CodeGenerator
	{
		public override string GetFileName()
		{
			return AngularNormalizer.NormalizeFileName(this.BaseEntity.EntityName) + ".model.ts";
		}

		public override string GenerateCode()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Template);
			sb.Replace("{{ENTITY}}", this.BaseEntity.EntityName);
			sb.Replace("{{PROPERTIES}}", GenerateCodeForProperties());
			sb.Replace("{{IMPORT_MODELS}}", GenerateCodeForImports());
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
					if (p.IsCollection)
						sb.AppendFormat("  {0}: Array<{1}> = new Array<{1}>();",
							AngularNormalizer.NormalizePropertyName(p.Name),
							p.GetParameterizedType());
					else if (p.IsEnum)
						sb.AppendFormat("  {0}: string;",
							AngularNormalizer.NormalizePropertyName(p.Name));
					else if (p.IsEntityReference)
						sb.AppendFormat("  {0}: EntityReference;",
							AngularNormalizer.NormalizePropertyName(p.Name));
					else
					{
						if (p.IsStringClob)
							p.Type = "string"; //TODO refactor
						sb.AppendFormat("  {0}: {1};",
							AngularNormalizer.NormalizePropertyName(p.Name),
							AngularNormalizer.NormalizeTypeNameFromCSharp(p.Type));
					}
				}
			}
			return sb.ToString();
		}

		private string GenerateCodeForImports()
		{
			StringBuilder sb = new StringBuilder();

			List<string> importingTypes = new List<string>();

			foreach (Property p in this.BaseEntity.Properties)
			{
				if (!p.IsContainer && p.IsCollection)
				{
					string type = p.GetParameterizedType();

					if (!string.IsNullOrEmpty(type) && !p.IsAutoReference)
					{
						if(!importingTypes.Contains(type))
							importingTypes.Add(type);
					}
				}
			}

			foreach(string type in importingTypes)
			{
				if (sb.Length > 0)
					sb.AppendLine();
				sb.AppendFormat("import {{ {0} }} from './{1}.model';",
					type,
					AngularNormalizer.NormalizeFileName(type));
			}

			return sb.ToString();
		}

	}
}