using MetalSoft.Core.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators.Angular
{
	public class AngularEntryComponentTemplateCodeGenerator : CodeGenerator
	{
		private Dictionary<string, string> fieldSnippets = new Dictionary<string, string>();

		public override string GetFileName()
		{
			return AngularNormalizer.NormalizeFileName(this.BaseEntity.EntityName) + "-entry.component.html";
		}

		public override string GenerateCode()
		{
			PrepareFieldSnippets();
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Template);
			sb.Replace("{{ENTITY}}", this.BaseEntity.EntityName);
			sb.Replace("{{ENTITY_LABEL}}", this.BaseEntity.EntityLabel);
			sb.Replace("{{PROPERTIES}}", GerenerateCodeForFields());
			sb.Replace("{{COMPOSITIONS}}", GerenerateCodeForGrids());
			//sb.Replace("\n\n\n", "\n\n");
			return sb.ToString();
		}

		private void PrepareFieldSnippets()
		{
			this.fieldSnippets.Add("string", ExtractSnippet("<!--FIELD:TEXT-->", "<!--END-FIELD:TEXT-->"));
			this.fieldSnippets.Add("StringClob", ExtractSnippet("<!--FIELD:TEXT-AREA-->", "<!--END-FIELD:TEXT-AREA-->"));
			this.fieldSnippets.Add("number", ExtractSnippet("<!--FIELD:NUMBER-->", "<!--END-FIELD:NUMBER-->"));
			this.fieldSnippets.Add("selectEntity", ExtractSnippet("<!--FIELD:SELECT-->", "<!--END-FIELD:SELECT-->"));
			this.fieldSnippets.Add("selectPrimitive", ExtractSnippet("<!--FIELD:SELECT-PRIMITIVE-->", "<!--END-FIELD:SELECT-PRIMITIVE-->"));
			this.fieldSnippets.Add("smartSearch", ExtractSnippet("<!--FIELD:SMART-SEARCH-->", "<!--END-FIELD:SMART-SEARCH-->"));
			this.fieldSnippets.Add("Date", ExtractSnippet("<!--FIELD:DATE-->", "<!--END-FIELD:DATE-->"));
			this.fieldSnippets.Add("collection", ExtractSnippet("<!--SNIPPET:COMPOSITION-->", "<!--END-SNIPPET:COMPOSITION-->"));
		}

		private string GerenerateCodeForFields()
		{
			StringBuilder sb = new StringBuilder();
			foreach(Property p in this.BaseEntity.Properties)
			{
				sb.Append(GenerateCodeForField(p));
			}
			return sb.ToString();
		}

		private string GenerateCodeForField(Property p)
		{
			string code = string.Empty;
			if (!p.IsCollection)
			{
				string snippet = GetSnippetForProperty(p);
				snippet = snippet.Replace("{{LOWER_PROPERTY}}", AngularNormalizer.NormalizePropertyName(p.Name));
				snippet = snippet.Replace("{{UPPER_PROPERTY}}", p.Name);
				snippet = snippet.Replace("{{PROPERTY_LABEL}}", p.Label);
				snippet = snippet.Replace("{{PROPERTY_TIP}}", p.Tip);
				snippet = snippet.Replace("{{PROPERTY_REQUIRED}}", p.Required.ToString().ToLower());
				snippet = snippet.Replace("{{TARGET_SERVICE}}", p.Type);
				code = Indent(snippet, 2);
			}
			return code;
		}

		private string GerenerateCodeForGrids()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Property p in this.BaseEntity.Properties)
			{
				if (p.IsCollection)
				{
					string snippet = GetSnippetForProperty(p);
					snippet = snippet.Replace("{{LOWER_PROPERTY}}", AngularNormalizer.NormalizePropertyName(p.Name));
					snippet = snippet.Replace("{{UPPER_PROPERTY}}", p.Name);
					snippet = snippet.Replace("{{PROPERTY_LABEL}}", p.Label);
					snippet = snippet.Replace("{{GRID_HEADERS}}", GenerateCodeForGridHeaders(p.CompositionInfo));
					snippet = snippet.Replace("{{GRID_CELLS}}", GenerateCodeForGridCells(p.CompositionInfo));
					sb.Append(snippet);
				}
			}
			return sb.ToString();
		}

		private string GetSnippetForProperty(Property p)
		{
			string type = AngularNormalizer.NormalizeTypeNameFromCSharp(p.Type);
			if (this.fieldSnippets.ContainsKey(type))
				return this.fieldSnippets[type];
			else if (p.IsEnum)
				return this.fieldSnippets["selectPrimitive"];
			else if (p.IsCollection)
				return this.fieldSnippets["collection"];
			else
				return this.fieldSnippets["smartSearch"];
		}

		private string GenerateCodeForGridHeaders(Entity compositionInfo)
		{
			if (compositionInfo == null)
				throw new ArgumentNullException("missing composition info for property");
			StringBuilder sb = new StringBuilder();
			foreach(Property p in compositionInfo.Properties)
			{
				if (!p.IsContainer)
				{
					sb.AppendFormat("          <th>{0}</th>", p.Label).AppendLine();
				}
			}
			return sb.ToString();
		}

		private string GenerateCodeForGridCells(Entity compositionInfo)
		{
			if (compositionInfo == null)
				throw new ArgumentNullException("missing composition info for property");
			StringBuilder sb = new StringBuilder();
			foreach (Property p in compositionInfo.Properties)
			{
				if (!p.IsContainer)
				{
					string code = GenerateCodeForGridField(p);
					sb.AppendFormat(code);
				}
			}
			return sb.ToString();
		}

		private string GenerateCodeForGridField(Property p)
		{
			string code = string.Empty;
			if (!p.IsCollection)
			{
				string snippet = GetSnippetForProperty(p);
				snippet = snippet.Replace("{{LOWER_PROPERTY}}", AngularNormalizer.NormalizePropertyName(p.Name));
				snippet = snippet.Replace("{{UPPER_PROPERTY}}", p.Name);
				snippet = snippet.Replace("{{PROPERTY_LABEL}}", "");
				snippet = snippet.Replace("{{PROPERTY_TIP}}", p.Tip);
				snippet = snippet.Replace("{{PROPERTY_REQUIRED}}", p.Required.ToString().ToLower());
				snippet = snippet.Replace("{{TARGET_SERVICE}}", p.Type);
				snippet = snippet.Replace("<div class=\"form-group col-lg-10\">", "<td>"); //TODO refactor
				snippet = snippet.Replace("</div>", "</td>");
				snippet = snippet.Replace("model.", "child.");
				code = Indent(snippet, 6);
			}
			return code;
		}

	}
}