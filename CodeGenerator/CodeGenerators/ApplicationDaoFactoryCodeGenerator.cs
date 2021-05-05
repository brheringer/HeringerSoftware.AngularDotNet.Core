using System;
using System.Text;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators
{
	public class ApplicationDaoFactoryCodeGenerator : CodeGenerator
	{
		public override string GetFileName()
		{
			return string.Format("{0}DAOFactory-TODO-add-{1}.txt",
				this.BaseEntity.Application.Name,
				this.BaseEntity.EntityName);
		}

		public override string GenerateCode()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Template);
			sb.Replace("{{ENTITY}}", this.BaseEntity.EntityName);
			sb.Replace("{{LOWER_ENTITY}}", Lower(this.BaseEntity.EntityName));
			return sb.ToString();
		}

		private string Lower(string entityName)
		{
			if (string.IsNullOrEmpty(entityName))
				return entityName;
			else if (entityName.Length == 1)
				return entityName.ToLower();
			else
				return entityName.Substring(0, 1).ToLower()
					+ entityName.Substring(1);
			//TODO problema: CID -> cID
		}
	}
}