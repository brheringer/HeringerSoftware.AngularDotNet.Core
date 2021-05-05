using MetalSoft.Core.CodeGenerator.Model;
using System;
using System.Text;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators.Angular
{
	public class AngularEntryComponentCssCodeGenerator : CodeGenerator
	{
		public override string GetFileName()
		{
			return AngularNormalizer.NormalizeFileName(this.BaseEntity.EntityName) + "-entry.component.css";
		}

		public override string GenerateCode()
		{
			return this.Template;
		}

	}
}