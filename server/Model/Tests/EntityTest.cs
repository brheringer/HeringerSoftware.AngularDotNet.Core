using System;
using HeringerSoftware.AngularDotNet.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeringerSoftware.AngularDotNet.Core.Model.Tests
{
	[TestClass]
	public class EntityTest
	{
		[TestMethod]
		public void TestIsPersistent()
		{
			var e = new MockedEntity();
			e.Id = 0;
			Assert.IsFalse(e.IsPersistent);
			e.Id = 1;
			Assert.IsTrue(e.IsPersistent);
		}

		[TestMethod]
		public void TestValidateRequiredStringPropertyFilled()
		{
			var e = new MockedEntity();
			e.Name = "a name";
			e.ValidateRequiredProperty(() => e.Name);
		}

		[TestMethod]
		[ExpectedException(typeof(RequiredPropertyException))]
		public void TestValidateRequiredStringPropertyNotFilled()
		{
			var e = new MockedEntity();
			e.Name = string.Empty;
			e.ValidateRequiredProperty(() => e.Name);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidPropertyValueException))]
		public void TestValidateRequiredPropertyStringTooLong()
		{
			var e = new MockedEntity();
			e.Name = new string('x', 11);
			e.ValidateRequiredProperty(() => e.Name, 10);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidPropertyValueException))]
		public void TestValidatePropertyStringTooLong()
		{
			var e = new MockedEntity();
			e.Name = new string('x', 11);
			e.ValidatePropertyValue(() => e.Name, 10);
		}

		[TestMethod]
		public void TestValidateRequiredEntityPropertyFilled()
		{
			var e = new MockedEntity();
			e.OtherInstance = new MockedEntity();
			e.ValidateRequiredProperty(() => e.OtherInstance);
		}

		[TestMethod]
		[ExpectedException(typeof(RequiredPropertyException))]
		public void TestValidateRequiredEntityPropertyNotFilled()
		{
			var e = new MockedEntity();
			e.OtherInstance = null;
			e.ValidateRequiredProperty(() => e.OtherInstance);
		}

		[TestMethod]
		public void TestValidateRequiredDateTimePropertyFilled()
		{
			var e = new MockedEntity();
			e.Date = new DateTime(2017, 11, 3);
			e.ValidateRequiredProperty(() => e.Date);
		}

		[TestMethod]
		[ExpectedException(typeof(RequiredPropertyException))]
		public void TestValidateRequiredDateTimePropertyNotFilled()
		{
			var e = new MockedEntity();
			e.Date = DateTime.MinValue;
			e.ValidateRequiredProperty(() => e.Date);
		}

		[TestMethod]
		public void TestValidateDateTimePropertyValueEqualsToMinMax()
		{
			DateTime aDay = new DateTime(2017, 6, 1);
			var e = new MockedEntity();
			e.Date = aDay;
			e.ValidatePropertyValue(() => e.Date, aDay, aDay);
		}

		[TestMethod]
		public void TestValidateDateTimePropertyValueBetweenMinMax()
		{
			DateTime aDay = new DateTime(2017, 6, 1);
			var e = new MockedEntity();
			e.Date = aDay;
			e.ValidatePropertyValue(() => e.Date, aDay.AddDays(-1), aDay.AddDays(1));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidPropertyValueException))]
		public void TestValidateDateTimePropertyValueBeforeMin()
		{
			DateTime aDay = new DateTime(2017, 6, 1);
			var e = new MockedEntity();
			e.Date = aDay;
			e.ValidatePropertyValue(() => e.Date, aDay.AddDays(1), aDay.AddDays(1));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidPropertyValueException))]
		public void TestValidateDateTimePropertyValueAfterMax()
		{
			DateTime aDay = new DateTime(2017, 6, 1);
			var e = new MockedEntity();
			e.Date = aDay;
			e.ValidatePropertyValue(() => e.Date, aDay.AddDays(-1), aDay.AddDays(-1));
		}
	}
}
