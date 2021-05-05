using MetalSoft.Core.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators
{
	public class EntityControllerCodeGenerator : CodeGenerator
	{
		private Dictionary<string, string> fieldSnippets = new Dictionary<string, string>();

		public override string GetFileName()
		{
			return this.BaseEntity.EntityName + "Controller.cs";
		}

		public override string GenerateCode()
		{
			PrepareFieldSnippets();
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Template);
			sb.Replace("{{APPLICATION}}", this.BaseEntity.Application.Name);
			sb.Replace("{{ENTITY}}", this.BaseEntity.EntityName);
			sb.Replace("{{COLLECTION}}", this.BaseEntity.CollectionName);
			sb.Replace("{{SOLVE_PROXIES}}", GenerateCodeToSolveProxies());
			sb.Replace("{{SOLVE_PROXIES_FOR_SEARCH}}", GenerateCodeToSolveProxiesForSearch());
			sb.Replace("{{SOLVE_ENUM_FOR_SEARCH}}", GenerateCodeToSolveEnumForSearch());
			sb.Replace("{{FILTERS}}", GenerateCodeForFilters());
			sb.Replace("{{COMPOSITIONS}}", GenerateCodeForCompositions("composition"));
			sb.Replace("{{COMPOSITIONS-BUSINESS}}", GenerateCodeForCompositions("compositionBusiness"));
			sb.Replace("{{COMPOSITIONS-PERSISTENCE}}", GenerateCodeForCompositions("compositionPersistence"));
			sb.Replace("{{COMPOSITIONS-DELIVERY}}", GenerateCodeForCompositions("compositionDelivery"));
			return sb.ToString();
		}

		private void PrepareFieldSnippets()
		{
			this.fieldSnippets.Add("proxy", ExtractSnippet("<!--SNIPPET:SOLVE_PROXY-->", "<!--END-SNIPPET:SOLVE_PROXY-->"));
			this.fieldSnippets.Add("proxyForSearch", ExtractSnippet("<!--SNIPPET:SOLVE_PROXY_FOR_SEARCH-->", "<!--END-SNIPPET:SOLVE_PROXY_FOR_SEARCH-->"));
			this.fieldSnippets.Add("enumForSearch", ExtractSnippet("<!--SNIPPET:SOLVE_ENUM_FOR_SEARCH-->", "<!--END-SNIPPET:SOLVE_ENUM_FOR_SEARCH-->"));
			this.fieldSnippets.Add("composition", ExtractSnippet("<!--SNIPPET:COMPOSITION-->", "<!--END-SNIPPET:COMPOSITION-->"));
			this.fieldSnippets.Add("compositionBusiness", ExtractSnippet("<!--SNIPPET:COMPOSITION-BUSINESS-->", "<!--END-SNIPPET:COMPOSITION-BUSINESS-->"));
			this.fieldSnippets.Add("compositionPersistence", ExtractSnippet("<!--SNIPPET:COMPOSITION-PERSISTENCE-->", "<!--END-SNIPPET:COMPOSITION-PERSISTENCE-->"));
			this.fieldSnippets.Add("compositionDelivery", ExtractSnippet("<!--SNIPPET:COMPOSITION-DELIVERY-->", "<!--END-SNIPPET:COMPOSITION-DELIVERY-->"));
		}

		private string GenerateCodeToSolveProxies()
		{
			return GenerateCodeToSolveProxy(this.fieldSnippets["proxy"]);
		}

		private string GenerateCodeToSolveProxiesForSearch()
		{
			return GenerateCodeToSolveProxy(this.fieldSnippets["proxyForSearch"]);
		}

		private string GenerateCodeToSolveProxy(string snippet)
		{
			List<string> solvers = new List<string>();
			foreach (Property p in this.BaseEntity.Properties)
			{
				if (p.IsEntityReference)
				{
					solvers.Add(snippet
						.Replace("{{PROPERTY}}", p.Name)
						.Replace("{{PROPERTY-TYPE}}", p.Type));
				}
			}
			return string.Join(System.Environment.NewLine, solvers.ToArray());
		}

		private string GenerateCodeToSolveEnumForSearch()
		{
			string snippet = this.fieldSnippets["enumForSearch"];
			List<string> solvers = new List<string>();
			foreach (Property p in this.BaseEntity.Properties)
			{
				if (p.IsEnum)
				{
					solvers.Add(snippet
						.Replace("{{PROPERTY}}", p.Name)
						.Replace("{{PROPERTY-TYPE}}", p.Type));
				}
			}
			return string.Join(System.Environment.NewLine, solvers.ToArray());
		}

		private string GenerateCodeForCompositions(string snippetKeySelector)
		{
			StringBuilder sb = new StringBuilder();
			string snippet = this.fieldSnippets[snippetKeySelector];
			foreach (Property p in this.BaseEntity.Properties)
			{
				if (p.IsCollection)
				{
					sb.Append(snippet
						.Replace("{{COMPOSITION-ENTITY}}", p.GetParameterizedType())
						.Replace("{{PROPERTY}}", p.Name));
				}
			}
			return sb.ToString();
		}

		private string GenerateCodeForFilters()
		{
			List<string> filters = new List<string>();
			foreach (Property p in this.BaseEntity.Properties)
			{
				if (!p.IsCollection)
				{
					if (p.IsEnum || p.IsEntityReference)
						filters.Add("filter" + p.Name);
					else
						filters.Add("dto.Filter" + p.Name);
				}
			}
			return string.Join(", ", filters.ToArray());
		}

	}
}