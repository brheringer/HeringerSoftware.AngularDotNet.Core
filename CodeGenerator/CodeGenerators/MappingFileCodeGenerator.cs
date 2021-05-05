using MetalSoft.Core.CodeGenerator.Model;
using System;
using System.Text;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators
{
	public class MappingFileCodeGenerator : CodeGenerator
	{
		public override string GetFileName()
		{
			return this.BaseEntity.EntityName + ".hbm.xml";
		}

		public override string GenerateCode()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Template);
			sb.Replace("{{APPLICATION}}", this.BaseEntity.Application.Name);
			sb.Replace("{{ENTITY}}", this.BaseEntity.EntityName);
			sb.Replace("{{PROPERTIES}}", GenerateCodeForProperties());
			return sb.ToString();
		}

		private string GenerateCodeForProperties()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Property p in this.BaseEntity.Properties)
			{
				if (sb.Length > 0)
					sb.AppendLine();

				if (p.IsCollection)
					sb.Append(GenerateBagTag(p));
				else if (p.IsEntityReference)
					sb.Append(GenerateManyToOneTag(p));
				else
					sb.Append(GeneratePropertyTag(p));
			}
			return sb.ToString();
		}

		private string GeneratePropertyTag(Property p)
		{
			return string.Format("\t\t<property name=\"{0}\" not-null=\"{1}\"{2}{3}/>",
				p.Name,
				p.Required.ToString().ToLower(),
				p.IsStringClob //TODO refactor
					? $" type=\"{p.Type}\""
					: string.Empty,
				p.CriterionForEquals 
					? string.Format(" unique-key=\"{0}_NaturalKey\"", this.BaseEntity.EntityName) 
					: string.Empty);
		}

		private string GenerateManyToOneTag(Property p)
		{
			return string.Format("\t\t<many-to-one name=\"{0}\" not-null=\"{1}\"{2}{3}/>",
				p.Name,
				p.Required.ToString().ToLower(),
				p.CriterionForEquals
					? string.Format(" unique-key=\"{0}_NaturalKey\"", this.BaseEntity.EntityName)
					: string.Empty,
				p.IsContainer
					? string.Format(" index=\"{0}_{1}_Index\"", this.BaseEntity.EntityName, p.Name)
					: string.Empty);
		}

		private string GenerateBagTag(Property p)
		{
			string line1 = "\t\t<bag name=\"{0}\" inverse=\"true\" lazy=\"true\" cascade=\"delete-orphan\">";
			string line2 = "\t\t\t<key column=\"{1}\" />";
			string line3 = "\t\t\t<one-to-many class=\"{2}\" />";
			string line4 = "\t\t</bag>";
			string allLines = string.Join(System.Environment.NewLine, line1, line2, line3, line4);
			string type = p.GetParameterizedType();
			return string.Format(allLines,
				p.Name,
				p.ForeignKeyFieldName,
				type);
		}
	}
}