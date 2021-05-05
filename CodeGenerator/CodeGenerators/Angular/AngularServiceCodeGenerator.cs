using MetalSoft.Core.CodeGenerator.Model;
using System;
using System.Text;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators.Angular
{
	public class AngularServiceCodeGenerator : CodeGenerator
	{
		public override string GetFileName()
		{
			return AngularNormalizer.NormalizeFileName(this.BaseEntity.EntityName) + ".service.ts";
		}

		public override string GenerateCode()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Template);
			sb.Replace("{{ENTITY}}", this.BaseEntity.EntityName);
			sb.Replace("{{LOWER_ENTITY}}", AngularNormalizer.NormalizeFileName(this.BaseEntity.EntityName));
			sb.Replace("{{COLLECTION}}", this.BaseEntity.CollectionName);
			sb.Replace("{{LOWER_COLLECTION}}", AngularNormalizer.NormalizeFileName(this.BaseEntity.CollectionName));
			return sb.ToString();
		}
		
	}
}