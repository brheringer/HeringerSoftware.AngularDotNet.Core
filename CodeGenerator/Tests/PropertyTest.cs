using Microsoft.VisualStudio.TestTools.UnitTesting;
using MetalSoft.Core.CodeGenerator.CodeGenerators;
using MetalSoft.Core.CodeGenerator.CodeGenerators.Angular;
using MetalSoft.Core.CodeGenerator.Model;

namespace UnitTestProject1
{
    [TestClass]
    public class PropertyTest
	{
		[TestMethod]
		public void IsCollectionTest()
		{
			Property p = new Property(new Entity());
			p.Type = "IList<CategoriaRisco>";
			Assert.IsTrue(p.IsCollection);
			Assert.IsFalse(p.IsEntityReference);
			Assert.IsFalse(p.IsEnum);
		}

		[TestMethod]
		public void IsEntityReferenceTest()
		{
			Property p = new Property(new Entity());
			p.Type = "CategoriaRisco";
			p.IsEnum = false;
			Assert.IsFalse(p.IsCollection);
			Assert.IsTrue(p.IsEntityReference);
			Assert.IsFalse(p.IsEnum);
		}

		[TestMethod]
		public void IsEnumTest()
		{
			Property p = new Property(new Entity());
			p.Type = "CategoriaRisco";
			p.IsEnum = true;
			Assert.IsFalse(p.IsCollection);
			Assert.IsFalse(p.IsEntityReference);
			Assert.IsTrue(p.IsEnum);
		}

		[TestMethod]
		public void GetParameterizedTypeTest()
		{
			Property p = new Property(new Entity());
			p.Type = "IList<CategoriaRisco>";
			Assert.AreEqual("CategoriaRisco", p.GetParameterizedType());
		}

		[TestMethod]
		public void NoParameterizedTypeTest()
		{
			Property p = new Property(new Entity());
			p.Type = "CategoriaRisco";
			Assert.AreEqual(string.Empty, p.GetParameterizedType());
		}

		[TestMethod]
		public void IsAutoReferenceTest()
		{
			Property p = new Property(new Entity() { EntityName = "CategoriaRisco" });
			p.Type = "CategoriaRisco";
			Assert.IsTrue(p.IsAutoReference);
		}
	}
}
