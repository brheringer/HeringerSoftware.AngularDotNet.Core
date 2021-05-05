using HeringerSoftware.AngularDotNet.CommonLibraries.Math;
using System;

namespace HeringerSoftware.AngularDotNet.CommonLibraries.Brazil
{
	public class Cpf : BrazilianDocument
	{
		public const int QUANTITY_OF_DIGITS = 11;
		public const string MASK = "000\\.000\\.000-00";

		public Cpf(string number) : base(number)
		{
		}

		public override string Format()
		{
			return FormatWithMask(MASK);
		}

		public override bool IsValid()
		{
			return IsValid(QUANTITY_OF_DIGITS);
		}

		protected override bool AreCheckDigitsCorrect()
		{
			int dv1 = Modulus.CalculateDigit(this.Number.Substring(0, this.Number.Length - 2), 11, 11);
			int dv2 = Modulus.CalculateDigit(this.Number.Substring(0, this.Number.Length - 1), 11, 11);
			string dvs = dv1.ToString() + dv2.ToString();
			return this.Number.EndsWith(dvs);
		}
	}
}
