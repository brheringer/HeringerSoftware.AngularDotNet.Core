using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HeringerSoftware.AngularDotNet.CommonLibraries.Math.Tests
{
	[TestClass]
	public class ModulusTest
	{
		[TestMethod]
		public void TestPrepareMultipliersGreaterThan9()
		{
			int[] multiplicadores = Modulus.PrepareMultipliers(11, 9);
			int[] expected = { 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			Assert.AreEqual(11, multiplicadores.Length);
			Assert.AreEqual(11, expected.Length);
			for (int i = 0; i < 11; i++)
				Assert.AreEqual(expected[i], multiplicadores[i]);
		}

		[TestMethod]
		public void TestPrepareMultipliersLesserThan9()
		{
			int[] multiplicadores = Modulus.PrepareMultipliers(5);
			int[] expected = { 6, 5, 4, 3, 2 };
			Assert.AreEqual(5, multiplicadores.Length);
			Assert.AreEqual(5, expected.Length);
			for (int i = 0; i < 5; i++)
				Assert.AreEqual(expected[i], multiplicadores[i]);
		}

		[TestMethod]
		public void TestPrepareMultipliersMajorMultiplier7()
		{
			int[] multiplicadores = Modulus.PrepareMultipliers(10, 7);
			int[] expected = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
			Assert.AreEqual(10, multiplicadores.Length);
			Assert.AreEqual(10, expected.Length);
			for (int i = 0; i < 10; i++)
				Assert.AreEqual(expected[i], multiplicadores[i]);
		}

		[TestMethod]
		public void TestPrepareMultipliersRepeatingPattern()
		{
			int[] multiplicadores = Modulus.PrepareMultipliers(10, new int[] { 3, 1, 9, 7 });
			int[] expected = { 3, 1, 9, 7, 3, 1, 9, 7, 3, 1 };
			Assert.AreEqual(10, multiplicadores.Length);
			Assert.AreEqual(10, expected.Length);
			for (int i = 0; i < 10; i++)
				Assert.AreEqual(expected[i], multiplicadores[i]);
		}

		[TestMethod]
		public void TestModulo_CheckDigit5()
		{
			Assert.AreEqual(5, Modulus.CalculateDigit("12345678", 11, 7));
		}

		[TestMethod]
		public void TestModulo_CheckDigit0()
		{
			Assert.AreEqual(0, Modulus.CalculateDigit("123456785", 11, 7));
		}

		[TestMethod]
		public void TestModuloWithGivenMultipliers()
		{
			string baseCalculo = "000100000000190000021";
			int[] multiplicadores = Modulus.PrepareMultipliers(baseCalculo.Length, new int[] { 3, 1, 9, 7 });
			Assert.AreEqual(8, Modulus.CalculateDigit(baseCalculo, 11, multiplicadores));
		}
	}
}
