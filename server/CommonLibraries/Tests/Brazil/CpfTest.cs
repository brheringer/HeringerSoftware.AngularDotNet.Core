using HeringerSoftware.AngularDotNet.CommonLibraries.Brazil;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HeringerSoftware.AngularDotNet.CommonLibraries.Brazil.Tests
{
	[TestClass]
	public class CpfTest
	{
		[TestMethod]
		public void TestValidCpf()
		{
			Assert.AreEqual("780.958.710-28", new Cpf("78095871028").Format());
		}

		[TestMethod]
		public void TestInvalidCheckDigits()
		{
			try
			{
				new Cpf("78095871099").Format();
			}
			catch(InvalidOperationException)
			{
				return;
			}
			Assert.Fail();
		}

		[TestMethod]
		public void TestInvalidCpf()
		{
			try
			{
				new Cpf("XX0958710XX").Format();
			}
			catch (InvalidOperationException)
			{
				return;
			}
			Assert.Fail();
		}

		[TestMethod]
		public void TestEmptyCpf()
		{
			Assert.AreEqual(string.Empty, new Cpf(string.Empty).Format());
		}

		[TestMethod]
		public void TestNullCpf()
		{
			Assert.AreEqual(string.Empty, new Cpf(null).Format());
		}
	}
}
