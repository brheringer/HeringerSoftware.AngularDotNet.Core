using System;
using System.Linq;
using HeringerSoftware.AngularDotNet.CommonLibraries.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeringerSoftware.AngularDotNet.CommonLibraries.Tests.Math
{
	[TestClass]
	public class FinancialMathTest
	{
		[TestMethod]
		public void TestCalculateCompoundInterestRate_Constant()
		{
			Assert.AreEqual(0.0804632806584M, FinancialMath.CalculateCompoundInterestRate(0.00647M, 12));
		}

		[TestMethod]
		public void TestCalculateCompoundInterestRate_Array()
		{
			decimal[] rates = new decimal[12];
			for (int j = 0; j < rates.Length; j++)
				rates[j] = 0.00647M;

			Assert.AreEqual(0.0804632806584007M, FinancialMath.CalculateCompoundInterestRate(rates));
		}

		//TODO conversion methods: monthly to annually, daily to monthly, and vice-versa
	}
}
