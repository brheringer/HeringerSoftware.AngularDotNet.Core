using MetalSoft.Core.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators.Angular
{
	public class AngularEntryComponentCodeGenerator : CodeGenerator
	{
		private Dictionary<string, string> fieldSnippets = new Dictionary<string, string>();

		public override string GetFileName()
		{
			return AngularNormalizer.NormalizeFileName(this.BaseEntity.EntityName) + "-entry.component.ts";
		}

		public override string GenerateCode()
		{
			PrepareFieldSnippets();
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Template);
			sb.Replace("{{ENTITY}}", this.BaseEntity.EntityName);
			sb.Replace("{{LOWER_ENTITY}}", AngularNormalizer.NormalizeFileName(this.BaseEntity.EntityName));
			sb.Replace("{{LOWER_COLLECTION}}", AngularNormalizer.NormalizeFileName(this.BaseEntity.CollectionName));
			sb.Replace("{{LOWER_COLLECTION}}", AngularNormalizer.NormalizeFileName(this.BaseEntity.CollectionName));
			sb.Replace("{{IMPORT_COMPOSITIONS}}", GenerateCodeForImportingCompositions());
			sb.Replace("{{COMPOSITIONS_METHODS}}", GenerateCodeForCompositionsMethods());
			sb.Replace("{{POSSIBLE_VALUES_FOR_ENUMS}}", GenerateCodeForPossibleValuesForEnums());
			return sb.ToString();
		}

		private string GenerateCodeForImportingCompositions()
		{
			StringBuilder sb = new StringBuilder();
			string snippet = this.fieldSnippets["importCompositions"];
			foreach(var p in this.BaseEntity.Properties)
			{
				if(p.IsCollection)
				{
					string code = snippet;
					code = code.Replace("{{COMPOSITION_ENTITY}}", p.GetParameterizedType());
					code = code.Replace("{{LOWER_COMPOSITION_ENTITY}}", AngularNormalizer.NormalizeFileName(p.GetParameterizedType()));
					sb.Append(code);
				}
			}
			return sb.ToString();
		}

		private string GenerateCodeForCompositionsMethods()
		{
			StringBuilder sb = new StringBuilder();
			string snippet = this.fieldSnippets["compositonsMethods"];
			foreach (var p in this.BaseEntity.Properties)
			{
				if (p.IsCollection)
				{
					string code = snippet;
					code = code.Replace("{{COMPOSITION_ENTITY}}", p.GetParameterizedType());
					code = code.Replace("{{PROPERTY}}", p.Name);
					code = code.Replace("{{LOWER_PROPERTY}}", AngularNormalizer.NormalizePropertyName(p.Name));
					code = Indent(code, 1);
					sb.Append(code);
				}
			}
			return sb.ToString();
		}

		private string GenerateCodeForPossibleValuesForEnums()
		{
			StringBuilder sb = new StringBuilder();
			string snippet = this.fieldSnippets["possibleValuesForEnums"];
			foreach (var p in this.BaseEntity.Properties)
			{
				if (p.IsEnum)
				{
					string code = snippet;
					code = code.Replace("{{ENUM}}", p.Type);
					code = code.Replace("{{PROPERTY}}", p.Name);
					code = Indent(code, 1);
					sb.Append(code);
				}
			}
			return sb.ToString();
		}

		private void PrepareFieldSnippets()
		{
			this.fieldSnippets.Add("importCompositions", ExtractSnippet("{{SNIPPET:IMPORT_COMPOSITIONS}}", "{{END-SNIPPET:IMPORT_COMPOSITIONS}}"));
			this.fieldSnippets.Add("compositonsMethods", ExtractSnippet("{{SNIPPET:COMPOSITIONS_METHODS}}", "{{END-SNIPPET:COMPOSITIONS_METHODS}}"));
			this.fieldSnippets.Add("possibleValuesForEnums", ExtractSnippet("{{SNIPPET:POSSIBLE_VALUES_FOR_ENUMS}}", "{{END-SNIPPET:POSSIBLE_VALUES_FOR_ENUMS}}"));
		}
	}
}