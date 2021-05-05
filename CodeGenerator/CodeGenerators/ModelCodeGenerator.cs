using MetalSoft.Core.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators
{
	public class ModelCodeGenerator : CodeGenerator
	{
		public override string GetFileName()
		{
			return this.BaseEntity.EntityName + ".cs";
		}

		public override string GenerateCode()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Template);
			sb.Replace("{{APPLICATION}}", this.BaseEntity.Application.Name);
			sb.Replace("{{ENTITY}}", this.BaseEntity.EntityName);
			sb.Replace("{{PROPERTIES}}", GenerateCodeForProperties());
			sb.Replace("{{PROPERTIES_VALIDATION_METHODS_CALLS}}", GenerateCodeForPropertiesValidationMethodsCalls());
			sb.Replace("{{PROPERTIES_VALIDATION_METHODS}}", GenerateCodeForPropertiesValidationMethods());
			sb.Replace("{{EQUALS}}", GenerateCodeForEquals());
			sb.Replace("{{GETHASHCODE}}", GenerateCodeForGetHashCode());
			//TODO GetHashCode
			//TODO ToString?
			return sb.ToString();
		}

		private string GenerateCodeForProperties()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Property p in this.BaseEntity.Properties)
			{
				string type = p.Type;
				if (p.IsStringClob)
					type = "string"; //TODO refactor
				sb.AppendFormat("\t\tpublic virtual {0} {1} {{ get; set; }}", type, p.Name)
					.AppendLine();
			}
			return sb.ToString();
		}

		private string GenerateCodeForPropertiesValidationMethodsCalls()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Property p in this.BaseEntity.Properties)
			{
				if (sb.Length > 0)
					sb.AppendLine();
				sb.AppendFormat("\t\t\tValidate{0}();", p.Name);
			}
			return sb.ToString();
		}

		private string GenerateCodeForPropertiesValidationMethods()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Property p in this.BaseEntity.Properties)
			{
				if (sb.Length > 0)
					sb.AppendLine();
				sb.AppendFormat("\t\tpublic virtual void Validate{0}()", p.Name).AppendLine();
				sb.AppendFormat("\t\t{{", p.Name).AppendLine();
				if(p.Required)
					sb.AppendFormat("\t\t\tValidateRequiredProperty(() => this.{0});", p.Name).AppendLine();
				sb.AppendFormat("\t\t}}", p.Name).AppendLine();
			}
			return sb.ToString();
		}

		private string GenerateCodeForEquals()
		{
			StringBuilder sb = new StringBuilder();
			var criteria = this.BaseEntity.Properties.FindAll(p => p.CriterionForEquals);
			if (criteria.Count == 0)
			{
				sb.AppendLine("return base.Equals(obj);");
			}
			else
			{
				sb.AppendFormat("return EntityHelper.EqualsByReferenceByType(obj, this) ?? ");

				List<string> lista = new List<string>();
				foreach (Property p in criteria)
					lista.Add(string.Format(
						"string.Equals(this.{0}, (({1})obj).{0})",
						p.Name,
						this.BaseEntity.EntityName));

				string complement = string.Join(System.Environment.NewLine + "&& ", lista);
				sb.Append(complement)
					.Append(";");
			}
			return sb.ToString();
		}

		private string GenerateCodeForGetHashCode()
		{
			StringBuilder sb = new StringBuilder();
			var criteria = this.BaseEntity.Properties.FindAll(p => p.CriterionForEquals);
			if (criteria.Count == 0)
			{
				sb.AppendLine("return base.GetHashCode();");
			}
			else
			{
				List<string> lista = new List<string>();
				foreach (Property p in criteria)
					lista.Add(p.Name);

				string parameters = string.Join(", ", lista);
				sb.AppendFormat("return EntityHelper.GetHashCode({0});", parameters);
			}
			return sb.ToString();
		}
	}
}