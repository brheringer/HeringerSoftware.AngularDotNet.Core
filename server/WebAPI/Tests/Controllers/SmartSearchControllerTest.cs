using HeringerSoftware.AngularDotNet.Core.DataTransferObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeringerSoftware.AngularDotNet.Core.WebAPI.Controllers;
using HeringerSoftware.AngularDotNet.Core.Persistence;
using HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks;
using System;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Controllers
{
    [TestClass]
    public class SmartSearchControllerTest : BaseControllerTest
	{
		[TestMethod]
		public void TestSmartSearch()
		{
			SetBearerTokenForDefaultTestUser();
			Create("abcde", "ACME");
			Create("bcde", "ACME");
			Create("cde", "ACME");
			Create("abcde", "TABAJARA");
			var dto = Get<EntitiesReferencesDto>("/api/MockSmartSearch/MockEntity/10/b/ACME");
			Assert.IsNotNull(dto);
			Assert.IsNotNull(dto.Response);
			Assert.IsFalse(dto.Response.HasException, dto.Response.Exception);
			Assert.IsNotNull(dto.References);
			Assert.AreEqual(2, dto.References.Count);
			Assert.AreEqual("abcde", dto.References[0].Presentation);
			Assert.AreEqual("bcde", dto.References[1].Presentation);
		}

		[TestMethod]
		public void TestSmartSearchMax()
		{
			SetBearerTokenForDefaultTestUser();
			Create("abc");
			Create("abcd");
			Create("abcde");
			var dto = Get<EntitiesReferencesDto>("/api/MockSmartSearch/MockEntity/1/abc/ACME");
			Assert.IsFalse(dto.Response.HasException, dto.Response.Exception);
			Assert.AreEqual(1, dto.References.Count);
			Assert.AreEqual("abc", dto.References[0].Presentation);
		}

		[TestMethod]
		public void TestSmartSearchSort()
		{
			SetBearerTokenForDefaultTestUser();
			Create("az");
			Create("ax");
			Create("aa");
			var dto = Get<EntitiesReferencesDto>("/api/MockSmartSearch/MockEntity/10/a/ACME");
			Assert.IsFalse(dto.Response.HasException, dto.Response.Exception);
			Assert.AreEqual(3, dto.References.Count);
			Assert.AreEqual("aa", dto.References[0].Presentation);
			Assert.AreEqual("ax", dto.References[1].Presentation);
			Assert.AreEqual("az", dto.References[2].Presentation);
		}

		private MockDto Create(string theString, string company = "ACME")
		{
			MockDto eDto = new MockDto();
			eDto.TheString = theString;
			eDto.Company = company;

			var afterInsert = Post<MockDto>("/api/mock", eDto);

			Assert.IsFalse(afterInsert.Response.HasException, afterInsert.Response.Exception);

			return afterInsert;
		}
	}

}

//TODO search by: entity reference, enumeration, date, number
