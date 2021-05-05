using System;
using System.Collections.Generic;
using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence.Tests
{
	[TestClass]
	public class FilterHelperTest
	{
		[TestMethod]
		public void IsFilterUsedCollectionTest()
		{
			Assert.IsTrue(FilterHelper.IsFilterUsed<string>(new List<string>() { "a" }));
			Assert.IsFalse(FilterHelper.IsFilterUsed<string>(new List<string>() { }));
			Assert.IsFalse(FilterHelper.IsFilterUsed<string>(null));
		}
	}
}
