using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MetalSoft.Core.Model;

namespace MetalSoft.Core.Persistence.NhPersistence.Tests
{
	[TestClass]
	public class FilterHelperTest
	{
		[TestMethod]
		public void IsFilterUsedNullableDateTime()
		{
			DateTime? date;

			date = null;
			Assert.IsFalse(FilterHelper.IsFilterUsed(date));

			date = DateTime.MinValue;
			Assert.IsFalse(FilterHelper.IsFilterUsed(date));

			date = DateTime.Today;
			Assert.IsTrue(FilterHelper.IsFilterUsed(date));
		}

		[TestMethod]
		public void IsFilterUsedDateTime()
		{
			Assert.IsFalse(FilterHelper.IsFilterUsed(DateTime.MinValue));
			Assert.IsTrue(FilterHelper.IsFilterUsed(DateTime.Today));
		}

		[TestMethod]
		public void IsFilterUsedDecimal()
		{
			Assert.IsFalse(FilterHelper.IsFilterUsed(0M));
			Assert.IsTrue(FilterHelper.IsFilterUsed(1M));
			Assert.IsTrue(FilterHelper.IsFilterUsed(-1M));
		}

		[TestMethod]
		public void IsFilterUsedInt()
		{
			Assert.IsFalse(FilterHelper.IsFilterUsed(0));
			Assert.IsTrue(FilterHelper.IsFilterUsed(1));
			Assert.IsTrue(FilterHelper.IsFilterUsed(-1));
		}

		[TestMethod]
		public void IsFilterUsedNullableInt()
		{
			int? value = null;
			Assert.IsFalse(FilterHelper.IsFilterUsed(value));

			value = 0;
			Assert.IsFalse(FilterHelper.IsFilterUsed(value));

			value = 1;
			Assert.IsTrue(FilterHelper.IsFilterUsed(value));

			value = -1;
			Assert.IsTrue(FilterHelper.IsFilterUsed(value));
		}

		[TestMethod]
		public void IsFilterUsedObject()
		{
			object obj;

			obj = null;
			Assert.IsFalse(FilterHelper.IsFilterUsed(obj));

			obj = new object();
			Assert.IsTrue(FilterHelper.IsFilterUsed(obj));

			obj = 1;
			Assert.IsTrue(FilterHelper.IsFilterUsed(obj));
		}

		[TestMethod]
		public void IsFilterUsedString()
		{
			Assert.IsFalse(FilterHelper.IsFilterUsed((string)null));
			Assert.IsFalse(FilterHelper.IsFilterUsed(string.Empty));
			Assert.IsTrue(FilterHelper.IsFilterUsed("asdf"));
		}

		[TestMethod]
		public void IsFilterUsedList()
		{
			List<string> nullList = null;
			Assert.IsFalse(FilterHelper.IsFilterUsed(nullList));
			Assert.IsFalse(FilterHelper.IsFilterUsed(new List<string>()));
			Assert.IsTrue(FilterHelper.IsFilterUsed(new List<string>() { "asdf" }));
			Assert.IsTrue(FilterHelper.IsFilterUsed(new List<string>() { string.Empty }));
		}
	}
}
