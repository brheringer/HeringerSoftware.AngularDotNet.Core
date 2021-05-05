using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeringerSoftware.AngularDotNet.Core.Model.Tests
{
	[TestClass]
	public class EntityFactoryTest
	{
		[TestMethod]
		public void TestCreation()
		{
			var factory = new EntityFactory<MockedEntity>(() => new MockedEntity() { Name = "test" });
			var entityWithNoModification = factory.Create();
			var entityWithModification = factory.Create(e => e.RationalNumber = 99);
			Assert.AreEqual(0, entityWithNoModification.RationalNumber);
			Assert.AreEqual(99, entityWithModification.RationalNumber);
		}
	}
}
