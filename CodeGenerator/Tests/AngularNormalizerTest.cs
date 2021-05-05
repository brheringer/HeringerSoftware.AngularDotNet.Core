using Microsoft.VisualStudio.TestTools.UnitTesting;
using MetalSoft.Core.CodeGenerator.CodeGenerators.Angular;

namespace UnitTestProject1
{
	[TestClass]
	public class AngularNormalizerTest
	{
		[TestMethod]
		public void TestNormalizeFileName()
		{
			Assert.AreEqual("test", AngularNormalizer.NormalizeFileName("Test"));
			Assert.AreEqual("test-three-words", AngularNormalizer.NormalizeFileName("TestThreeWords"));
			Assert.AreEqual("test-cid", AngularNormalizer.NormalizeFileName("TestCID"));
			Assert.AreEqual("a-test", AngularNormalizer.NormalizeFileName("aTest"));
			Assert.AreEqual("atest", AngularNormalizer.NormalizeFileName("ATest"));
			Assert.AreEqual("test", AngularNormalizer.NormalizeFileName("TEST"));
			Assert.AreEqual("test", AngularNormalizer.NormalizeFileName("test"));
			Assert.AreEqual(string.Empty, AngularNormalizer.NormalizeFileName(string.Empty), "The normalization of an empty name is empty by definition.");
			Assert.AreEqual(null, AngularNormalizer.NormalizeFileName(null), "The normalization of null name is null by definition.");
		}

		[TestMethod]
		public void TestNormalizePropertyName()
		{
			Assert.AreEqual("test", AngularNormalizer.NormalizePropertyName("Test"));
			Assert.AreEqual("testThreeWords", AngularNormalizer.NormalizePropertyName("TestThreeWords"));
			Assert.AreEqual("myCID", AngularNormalizer.NormalizePropertyName("MyCID"));
			Assert.AreEqual("cID", AngularNormalizer.NormalizePropertyName("CID"));
			Assert.AreEqual("t", AngularNormalizer.NormalizePropertyName("T"));
			Assert.AreEqual(string.Empty, AngularNormalizer.NormalizePropertyName(string.Empty), "The normalization of an empty name is empty by definition.");
			Assert.AreEqual(null, AngularNormalizer.NormalizePropertyName(null), "The normalization of null name is null by definition.");
		}

		[TestMethod]
		public void TestNormalizeTypeNameFromCSharp()
		{
			//TODO nullables? array? list?
			Assert.AreEqual("string", AngularNormalizer.NormalizeTypeNameFromCSharp("char"));
			Assert.AreEqual("string", AngularNormalizer.NormalizeTypeNameFromCSharp("Char"));
			Assert.AreEqual("Date", AngularNormalizer.NormalizeTypeNameFromCSharp("DateTime"));
			Assert.AreEqual("number", AngularNormalizer.NormalizeTypeNameFromCSharp("int"));
			Assert.AreEqual("number", AngularNormalizer.NormalizeTypeNameFromCSharp("Int16"));
			Assert.AreEqual("number", AngularNormalizer.NormalizeTypeNameFromCSharp("Int32"));
			Assert.AreEqual("number", AngularNormalizer.NormalizeTypeNameFromCSharp("Int64"));
			Assert.AreEqual("number", AngularNormalizer.NormalizeTypeNameFromCSharp("long"));
			Assert.AreEqual("number", AngularNormalizer.NormalizeTypeNameFromCSharp("short"));
			Assert.AreEqual("number", AngularNormalizer.NormalizeTypeNameFromCSharp("float"));
			Assert.AreEqual("number", AngularNormalizer.NormalizeTypeNameFromCSharp("Single"));
			Assert.AreEqual("number", AngularNormalizer.NormalizeTypeNameFromCSharp("decimal"));
			Assert.AreEqual("number", AngularNormalizer.NormalizeTypeNameFromCSharp("Decimal"));
			Assert.AreEqual("number", AngularNormalizer.NormalizeTypeNameFromCSharp("double"));
			Assert.AreEqual("number", AngularNormalizer.NormalizeTypeNameFromCSharp("Double"));
			Assert.AreEqual("string", AngularNormalizer.NormalizeTypeNameFromCSharp("string"));
			Assert.AreEqual("string", AngularNormalizer.NormalizeTypeNameFromCSharp("String"));
		}
	}
}
