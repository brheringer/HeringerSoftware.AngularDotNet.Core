using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MetalSoft.Core.CodeGenerator.CodeGenerators.Angular
{
    public static class AngularNormalizer
    {
		private const char SEPARATOR_CHAR = '-';

		public static string NormalizeSelectorName(string name)
		{
			return NormalizeFileName(name);
		}

		public static string NormalizeFileName(string name)
		{
			if (name == null)
				return null;

			string normalized = string.Empty;
			for(int i = 0; i < name.Length; i++)
			{
				char currentChar = name[i];
				if (i > 0)
				{
					char previousChar = name[i - 1];
					if (Char.IsLower(previousChar) && Char.IsUpper(currentChar))
						normalized += SEPARATOR_CHAR;
				}
				normalized += Char.ToLower(currentChar);
			}
			//string[] tokens = Regex.Split(entityName, "[a-z][A-Z]");
			//string normalized = string.Join("-", tokens).ToLower();
			return normalized;
		}

		public static string NormalizePropertyName(string name)
		{
			if (name == null)
				return null;

			string normalized = string.Empty;
			if (!string.IsNullOrEmpty(name))
			{
				if (name.Length == 1)
					normalized = name.ToLower();
				else
					normalized = name.ToLower()[0] + name.Substring(1);
			}
			return normalized;
		}

		public static string NormalizeTypeNameFromCSharp(string name)
		{
			if (name == null)
				return null;

			Dictionary<string, string> map = new Dictionary<string, string>();
			map.Add("char", "string");
			map.Add("Char", "string");
			map.Add("DateTime", "Date");
			map.Add("decimal", "number");
			map.Add("Decimal", "number");
			map.Add("double", "number");
			map.Add("Double", "number");
			map.Add("float", "number");
			map.Add("Single", "number");
			map.Add("int", "number");
			map.Add("Int16", "number");
			map.Add("Int32", "number");
			map.Add("Int64", "number");
			map.Add("long", "number");
			map.Add("short", "number");
			map.Add("string", "string");
			map.Add("String", "string");
			//TODO nullables?
			//TODO array? list?

			return map.ContainsKey(name)
				? map[name]
				: name;
		}
	}
}
