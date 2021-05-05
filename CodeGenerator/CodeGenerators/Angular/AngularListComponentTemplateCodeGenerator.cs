using MetalSoft.Core.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators.Angular
{
	public class AngularListComponentTemplateCodeGenerator : CodeGenerator
	{
		private Dictionary<string, string> fieldSnippets = new Dictionary<string, string>();

		public override string GetFileName()
		{
			return AngularNormalizer.NormalizeFileName(this.BaseEntity.CollectionName) + "-list.component.html";
		}

		public override string GenerateCode()
		{
			PrepareFieldSnippets();
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Template);
			sb.Replace("{{ENTITY}}", this.BaseEntity.EntityName);
			sb.Replace("{{COLLECTION_LABEL}}", this.BaseEntity.CollectionLabel);
			sb.Replace("{{PROPERTIES}}", GerenerateCodeForFields());
			sb.Replace("{{GRID_HEADERS}}", GerenerateCodeForGridColumns());
			sb.Replace("{{GRID_CELLS}}", GerenerateCodeForGridCells());
			return sb.ToString();
		}

		private void PrepareFieldSnippets()
		{
			string textFieldSnippet = ExtractSnippet("<!--FIELD:TEXT-->", "<!--END-FIELD:TEXT-->");
			this.fieldSnippets.Add("string", textFieldSnippet);
			this.fieldSnippets.Add("number", ExtractSnippet("<!--FIELD:NUMBER-->", "<!--END-FIELD:NUMBER-->"));
			this.fieldSnippets.Add("selectEntity", ExtractSnippet("<!--FIELD:SELECT-->", "<!--END-FIELD:SELECT-->"));
			this.fieldSnippets.Add("selectPrimitive", ExtractSnippet("<!--FIELD:SELECT-PRIMITIVE-->", "<!--END-FIELD:SELECT-PRIMITIVE-->"));
			this.fieldSnippets.Add("smartSearch", ExtractSnippet("<!--FIELD:SMART-SEARCH-->", "<!--END-FIELD:SMART-SEARCH-->"));
			this.fieldSnippets.Add("Date", ExtractSnippet("<!--FIELD:DATE-->", "<!--END-FIELD:DATE-->"));
		}

		private string GerenerateCodeForFields()
		{
			StringBuilder sb = new StringBuilder();
			foreach(Property p in this.BaseEntity.Properties)
			{
				if (!p.IsCollection && !p.IsStringClob)
				{
					string snippet = GetSnippetForProperty(p);
					snippet = snippet.Replace("{{PROPERTY}}", p.Name);
					snippet = snippet.Replace("{{PROPERTY_LABEL}}", p.Label);
					snippet = snippet.Replace("{{PROPERTY_TIP}}", p.Tip);
					snippet = snippet.Replace("{{PROPERTY_REQUIRED}}", p.Required.ToString().ToLower());
					snippet = snippet.Replace("{{TARGET_SERVICE}}", p.Type);
					sb.Append(snippet);
				}
			}
			return sb.ToString();
		}

		private string GetSnippetForProperty(Property p)
		{
			//TODO text vs text-area
			string type = AngularNormalizer.NormalizeTypeNameFromCSharp(p.Type);
			if (this.fieldSnippets.ContainsKey(type))
				return this.fieldSnippets[type];
			else if (p.IsEnum)
				return this.fieldSnippets["selectPrimitive"];
			else
				return this.fieldSnippets["smartSearch"];
		}

		private string GerenerateCodeForGridColumns()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Property p in this.BaseEntity.Properties)
			{
				if (!p.IsCollection)
				{
					if (sb.Length > 0)
						sb.AppendLine();
					sb.AppendFormat("          <th scope=\"col\">{0}</th>", p.Label);
				}
			}
			return sb.ToString();
		}

		private string GerenerateCodeForGridCells()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Property p in this.BaseEntity.Properties)
			{
				if (!p.IsCollection)
				{
					if (sb.Length > 0)
						sb.AppendLine();
					if (p.IsEntityReference)
						sb.AppendFormat("          <td>{{{{i.{0}?.presentation}}}}</td>", AngularNormalizer.NormalizePropertyName(p.Name));
					else if(p.IsDateTime)
						sb.AppendFormat("          <td>{{{{i.{0} | metalsoftDateFormat}}}}</td>", AngularNormalizer.NormalizePropertyName(p.Name));
					else
						sb.AppendFormat("          <td>{{{{i.{0}}}}}</td>", AngularNormalizer.NormalizePropertyName(p.Name));
				}
			}
			return sb.ToString();
		}

	}
}