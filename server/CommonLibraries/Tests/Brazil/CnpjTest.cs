using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HeringerSoftware.AngularDotNet.CommonLibraries.Brazil.Tests
{
	[TestClass]
	public class CnpjTest
	{
		[TestMethod]
		public void TestValidCnpj()
		{
			Assert.AreEqual("87.564.480/0001-00", new Cnpj("87564480000100").Format());
		}

		[TestMethod]
		public void TestInvalidCheckDigits()
		{
			try
			{
				new Cnpj("87564480000199").Format();
			}
			catch(InvalidOperationException)
			{
				return;
			}
			Assert.Fail();
		}

		[TestMethod]
		public void TestInvalidCnpj()
		{
			try
			{
				new Cnpj("XX5644800001XX").Format();
			}
			catch (InvalidOperationException)
			{
				return;
			}
			Assert.Fail();
		}

		[TestMethod]
		public void TestEmptyCnpj()
		{
			Assert.AreEqual(string.Empty, new Cnpj(string.Empty).Format());
		}

		[TestMethod]
		public void TestNullCnpj()
		{
			Assert.AreEqual(string.Empty, new Cnpj(null).Format());
		}
	}
}
