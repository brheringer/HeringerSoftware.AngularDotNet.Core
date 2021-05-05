using System;
using System.Text.RegularExpressions;

namespace HeringerSoftware.AngularDotNet.CommonLibraries.Brazil
{
	public static class BrazilianDocumentFactory
	{
		public static BrazilianDocument Create(string number)
		{
			BrazilianDocument document = null;
			if (number != null)
			{
				switch (number.Length)
				{
					case Cpf.QUANTITY_OF_DIGITS:
						document = new Cpf(number);
						break;
					case Cnpj.QUANTITY_OF_DIGITS:
						document = new Cnpj(number);
						break;
				}
			}
			return document;
		}
	}
}
