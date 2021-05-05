using System;
using System.Collections.Generic;
using System.Linq;

namespace HeringerSoftware.AngularDotNet.CommonLibraries.Math
{
	public static class FinancialMath
	{
		public static decimal CalculateCompoundInterestRate(decimal i, int n)
		{
			return (decimal)System.Math.Pow(1 + (double)i, n) - 1;
		}

		public static decimal CalculateCompoundInterestRate(decimal[] i)
		{
			IEnumerable<double> doubles = i.Select(dec => (double)dec);
			return (decimal)doubles.AsEnumerable().Aggregate((a, b) => (1 + a) * (1 + b) - 1);
;		}
	}
}
