using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Wrappers
{
	[TestClass]
	[Ignore] //pseudotestes para tentar entender como biblioteca serializa data, mas não entendi ainda, depois revisito
	public class GeneralTests
	{
		private const string MASK = "yyyy-MM-ddTHH:mm:sszzz";

		[TestMethod]
		public void TestConvertDateStringToDateTime_DateTimeZoneHandling_Local()
		{
			TestConvertDateStringToDateTime("2019-01-01T23:59:59-02:00", DateTimeZoneHandling.Local, "2019-01-01T23:59:59-02:00");
		}

		[TestMethod]
		public void TestConvertDateStringToDateTime_DateTimeZoneHandling_RoundtripKind()
		{
			TestConvertDateStringToDateTime("2019-01-01T23:59:59-02:00", DateTimeZoneHandling.RoundtripKind, "2019-01-01T23:59:59-02:00");
		}

		[TestMethod]
		public void TestConvertDateStringToDateTime_DateTimeZoneHandling_Unspecified()
		{
			TestConvertDateStringToDateTime("2019-01-01T23:59:59-02:00", DateTimeZoneHandling.Unspecified, "2019-01-01T23:59:59-02:00");
		}

		[TestMethod]
		public void TestConvertDateStringToDateTime_DateTimeZoneHandling_Utc()
		{
			TestConvertDateStringToDateTime("2019-01-01T23:59:59-03:00", DateTimeZoneHandling.Utc, "2019-01-02T02:59:59+00:00");
		}

		private void TestConvertDateStringToDateTime(string inputDateString, DateTimeZoneHandling dateTimeZoneHandling, string expectedResult)
		{
			string json = "{\"date\": \"" + inputDateString + "\"}";

			JsonSerializerSettings settings = new JsonSerializerSettings() { DateTimeZoneHandling = dateTimeZoneHandling };
			JObject obj = JsonConvert.DeserializeObject<JObject>(json, settings);
			DateTime result = obj.Value<DateTime>("date");

			Assert.AreEqual(expectedResult, result.ToString("yyyy-MM-ddTHH:mm:sszzz"));
		}
	}
}
