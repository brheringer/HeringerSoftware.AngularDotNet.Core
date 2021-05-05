using MetalSoft.Core.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators
{
	public class NhEntityDaoCodeGenerator : CodeGenerator
	{
		private Dictionary<string, string> fieldSnippets = new Dictionary<string, string>();

		public override string GetFileName()
		{
			return "Nh" + this.BaseEntity.EntityName + "DAO.cs";
		}

		public override string GenerateCode()
		{
			PrepareFieldSnippets();
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Template);
			sb.Replace("{{APPLICATION}}", this.BaseEntity.Application.Name);
			sb.Replace("{{ENTITY}}", this.BaseEntity.EntityName);
			sb.Replace("{{FILTERS}}", GenerateCodeForFilters());
			sb.Replace("{{APPLY_FILTERS}}", GerenerateCodeForApplyFilters());
			return sb.ToString();
		}

		private void PrepareFieldSnippets()
		{
			string snippet = ExtractSnippet("/*BEGIN_FILTER_TEMPLATE_STRING_ANYWHERE*/", "/*END_FILTER_TEMPLATE_STRING_ANYWHERE*/");
			this.fieldSnippets.Add("string", snippet);
			this.fieldSnippets.Add("StringClob", snippet);
			this.fieldSnippets.Add("*", ExtractSnippet("/*BEGIN_FILTER_TEMPLATE_EQUAL*/", "/*END_FILTER_TEMPLATE_EQUAL*/"));
		}

		private string GenerateCodeForFilters()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var p in this.BaseEntity.Properties)
			{
				if (!p.IsCollection && !p.IsStringClob)
				{
					string type = p.Type;
					if (sb.Length > 0)
						sb.Append(", ");
					if (!p.IsEntityReference && !p.IsString)
						type = type + "?";
					sb.Append(type).Append(" filter").Append(p.Name);
				}
			}
			return sb.ToString();
		}

		private string GerenerateCodeForApplyFilters()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var p in this.BaseEntity.Properties)
			{
				if (!p.IsCollection && !p.IsStringClob)
				{
					string snippet = GetSnippetForProperty(p);
					snippet = snippet.Replace("{{PROPERTY}}", p.Name);
					sb.Append(snippet);
				}
			}
			return sb.ToString();
		}

		private string GetSnippetForProperty(Property p)
		{
			if (this.fieldSnippets.ContainsKey(p.Type))
				return this.fieldSnippets[p.Type];
			else
				return this.fieldSnippets["*"];
		}

	}
}