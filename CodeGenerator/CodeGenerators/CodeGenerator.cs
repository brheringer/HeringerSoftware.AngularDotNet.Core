using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MetalSoft.Core.CodeGenerator.Model;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators
{
	public abstract class CodeGenerator
	{
		public Entity BaseEntity { get; set; }
		public string Template { get; set; }

		public abstract string GenerateCode();

		public abstract string GetFileName();

		protected virtual string ExtractSnippet(string initialMarkUp, string finalMarkUp)
		{
			string snippet = string.Empty;
			int outerStart = this.Template.IndexOf(initialMarkUp);
			int outerEnd = this.Template.IndexOf(finalMarkUp) + finalMarkUp.Length;
			int innerStart = this.Template.IndexOf(initialMarkUp) + initialMarkUp.Length;
			int innerEnd = this.Template.IndexOf(finalMarkUp) - 1;
			if (outerStart >= 0 && outerEnd > outerStart)
			{
				snippet = this.Template.Substring(innerStart, innerEnd - innerStart + 1);
				this.Template = this.Template.Remove(outerStart, outerEnd - outerStart + System.Environment.NewLine.Length);
			}
			return snippet;
		}

		protected string Indent(string snippet, int tabs)
		{
			const int TAB_LENGTH = 2;
			string space = new string(' ', tabs * TAB_LENGTH);
			return space + snippet.Replace(System.Environment.NewLine,
				System.Environment.NewLine + space);
		}
	}
}
