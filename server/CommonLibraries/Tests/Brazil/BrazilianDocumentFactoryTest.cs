using HeringerSoftware.AngularDotNet.CommonLibraries.Brazil;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HeringerSoftware.AngularDotNet.CommonLibraries.Brazil.Tests
{
	[TestClass]
	public class BrazilianDocumentFactoryTest
	{
		[TestMethod]
		public void TestCreation()
		{
			Assert.IsInstanceOfType(BrazilianDocumentFactory.Create("11111111111"), typeof(Cpf));
			Assert.IsInstanceOfType(BrazilianDocumentFactory.Create("87564480000100"), typeof(Cnpj));
			Assert.IsNull(BrazilianDocumentFactory.Create("00"));
			Assert.IsNull(BrazilianDocumentFactory.Create(string.Empty));
			Assert.IsNull(BrazilianDocumentFactory.Create(null));
		}
	}
}
