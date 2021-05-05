using System;
using System.Text.RegularExpressions;

namespace HeringerSoftware.AngularDotNet.CommonLibraries.Brazil
{
	public abstract class BrazilianDocument
	{
		public string Number { get; private set; }

		protected BrazilianDocument(string number)
		{
			this.Number = RemoveNonDigits(number);
		}

		private static string RemoveNonDigits(string number)
		{
			string digitsOnly = string.Empty;
			if (number != null)
			{
				digitsOnly = Regex.Replace(number, "[^0-9]", string.Empty);
			}
			return digitsOnly;
		}

		public abstract string Format();

		protected string FormatWithMask(string mask)
		{
			string formatted = string.Empty;
			if (!string.IsNullOrEmpty(this.Number))
			{
				if (IsValid())
				{
					formatted = Convert.ToInt64(this.Number).ToString(mask);
				}
				else
				{
					throw new InvalidOperationException("Cannot format an invalid document number: " + this.Number);
				}
			}
			return formatted;
		}

		public abstract bool IsValid();

		protected virtual bool IsValid(int quantityOfDigits)
		{
			return !string.IsNullOrEmpty(this.Number)
				&& this.Number.Length == quantityOfDigits
				&& AreCheckDigitsCorrect();
		}

		protected abstract bool AreCheckDigitsCorrect();

		public static string Format(string number)
		{
			return BrazilianDocumentFactory.Create(number)?.Format();
		}
	}
}
