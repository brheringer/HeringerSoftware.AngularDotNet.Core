using HeringerSoftware.AngularDotNet.Core.DataTransferObjects;
using HeringerSoftware.AngularDotNet.Core.Model;
using HeringerSoftware.AngularDotNet.Core.WebAPI.Wrappers;
using HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Wrappers
{
    [TestClass]
    public class EntityWrapperTest
    {
		private EntityWrapper EntityWrapperInstance { get; set; }

		[TestInitialize]
		public void SetUp()
		{
			var builder = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<MockDbContext>();
			builder.UseInMemoryDatabase("memdb");
			var dbContext = new MockDbContext(builder.Options, new MockUserResolver());
			this.EntityWrapperInstance = new EntityWrapper(dbContext);
		}

        [TestMethod]
        public void TestWrapIntoEntity()
        {
			EntityDto dto = new EntityDto();
			dto.Id = 1;
			dto.CreationDateTime = new System.DateTime(2017, 11, 3, 12, 01, 02);
			dto.CreationUser = "john";
			dto.LastUpdateDateTime = new System.DateTime(2017, 11, 4, 3, 11, 12);
			dto.LastUpdateUser = "jorge";
			dto.Version = 2;

			Entity entity = new MockEntity();
			EntityWrapperInstance.WrapCommonFieldsIntoEntity(dto, entity);

			Assert.AreEqual(dto.Id, entity.Id);
			Assert.AreEqual(dto.CreationDateTime, entity.CreationDateTime);
			Assert.AreEqual(dto.CreationUser, entity.CreationUser);
			Assert.AreEqual(dto.LastUpdateDateTime, entity.LastUpdateDateTime);
			Assert.AreEqual(dto.LastUpdateUser, entity.LastUpdateUser);
			Assert.AreEqual(dto.Version, entity.Version);
		}

		[TestMethod]
		public void TestWrapIntoDto()
		{
			Entity entity = new MockEntity();
			entity.Id = 1;
			entity.CreationDateTime = new System.DateTime(2017, 11, 3, 12, 01, 02);
			entity.CreationUser = "john";
			entity.LastUpdateDateTime = new System.DateTime(2017, 11, 4, 3, 11, 12);
			entity.LastUpdateUser = "jorge";
			entity.Version = 2;

			EntityDto dto = new EntityDto();
			EntityWrapperInstance.WrapCommonFieldsIntoDto(entity, dto);

			Assert.AreEqual(entity.Id, dto.Id);
			Assert.AreEqual(entity.CreationDateTime, dto.CreationDateTime);
			Assert.AreEqual(entity.CreationUser, dto.CreationUser);
			Assert.AreEqual(entity.LastUpdateDateTime, dto.LastUpdateDateTime);
			Assert.AreEqual(entity.LastUpdateUser, dto.LastUpdateUser);
			Assert.AreEqual(entity.Version, dto.Version);
		}

		[TestMethod]
		public void TestWrapToReferences()
		{
			var dto = EntityWrapperInstance.WrapToReferences(new List<Entity>());
			Assert.IsNotNull(dto);
			Assert.AreEqual(0, dto.References.Count);
		}

		[TestMethod]
		public void TestWrapToReference()
		{
			MockEntity e = new MockEntity();
			e.Id = 1;
			e.TheString = "test";

			EntityReferenceDto dto = EntityWrapperInstance.WrapToReference(e);
			Assert.AreEqual(e.Id, dto.Id);
			Assert.AreEqual(e.ToString(), dto.Presentation);
		}

		[TestMethod]
		public void TestCreateProxy()
		{
			EntityReferenceDto dto = new EntityReferenceDto();
			dto.Id = 1;

			MockEntity entity = EntityWrapperInstance.CreateProxy<MockEntity>(dto);
			Assert.IsNotNull(entity);
			Assert.AreEqual(dto.Id, entity.Id);
		}

		[TestMethod]
		public void TestCopyFromDtoToEntity()
		{
            MockDto dto = new MockDto();
			dto.Id = 1;
			dto.CreationDateTime = new System.DateTime(2017, 11, 3, 12, 01, 02);
			dto.CreationUser = "john";
			dto.LastUpdateDateTime = new System.DateTime(2017, 11, 4, 3, 11, 12);
			dto.LastUpdateUser = "jorge";
			dto.Version = 2;
			dto.TheBoolean = true;
			dto.TheChar = 'a';
			dto.TheDateTime = new DateTime(2017, 12, 21);
			dto.TheDecimal = 1.131M;
			dto.TheDouble = 1.132;
			dto.TheEnumeration = DayOfWeek.Tuesday.ToString();
			dto.TheFloat = 1.47f;
			dto.TheInteger = 1;
			dto.TheObject = new object();
			dto.TheShort = 2;
			dto.TheString = "asdf";
			dto.TheReference = new EntityReferenceDto();
			dto.TheReference.Id = 1;
			dto.TheList = new List<int>();
			dto.TheComposition.Add(new MockCompositionDto() { TheString = "x1" });
			dto.TheComposition.Add(new MockCompositionDto() { TheString = "y2" });

			MockEntity entity = new MockEntity();
			//entity.TheNullReference = new MockEntity(); //vai ter que ficar null - problema com ef core 3.1
			EntityWrapperInstance.CopyInto(dto, entity);

			Assert.AreEqual(dto.Id, entity.Id);
			Assert.AreEqual(dto.CreationDateTime, entity.CreationDateTime);
			Assert.AreEqual(dto.CreationUser, entity.CreationUser);
			Assert.AreEqual(dto.LastUpdateDateTime, entity.LastUpdateDateTime);
			Assert.AreEqual(dto.LastUpdateUser, entity.LastUpdateUser);
			Assert.AreEqual(dto.Version, entity.Version);
			Assert.AreEqual(dto.TheBoolean, entity.TheBoolean);
			Assert.AreEqual(dto.TheChar, entity.TheChar);
			Assert.AreEqual(dto.TheDateTime, entity.TheDateTime);
			Assert.AreEqual(dto.TheDecimal, entity.TheDecimal);
			Assert.AreEqual(dto.TheDouble, entity.TheDouble);
			Assert.AreEqual(dto.TheEnumeration, entity.TheEnumeration.ToString());
			Assert.AreEqual(dto.TheFloat, entity.TheFloat);
			Assert.AreEqual(dto.TheInteger, entity.TheInteger);
			Assert.IsNull(entity.TheObject, "The only references that are copied are of type EntityReferenceDto.");
			Assert.AreEqual(dto.TheShort, entity.TheShort);
			Assert.AreEqual(dto.TheString, entity.TheString);
			Assert.IsNotNull(entity.TheReference);
			Assert.AreEqual(dto.TheReference.Id, entity.TheReference.Id);
			Assert.IsNull(entity.TheList, "Collections are not copied.");
			Assert.IsNull(entity.TheNullReference);
			Assert.IsNotNull(entity.TheComposition);
			Assert.AreEqual(2, entity.TheComposition.Count);
			Assert.AreEqual("x1", entity.TheComposition[0].TheString);
			Assert.AreEqual("y2", entity.TheComposition[1].TheString);
		}

		[TestMethod]
		public void TestCopyFromDtoToEntity_UpdateComposition()
		{
			MockDto dto = new MockDto();
			dto.Id = 1;
			dto.TheList = new List<int>();
			dto.TheComposition.Add(new MockCompositionDto() { Id = 1, TheString = "update" });
			dto.TheComposition.Add(new MockCompositionDto() { Id = 2, TheString = "2", DeleteMe = true });
			dto.TheComposition.Add(new MockCompositionDto() { Id = 0, TheString = "3" }); //Id=0 significa nao persistente, vai ser adicionado

			MockEntity entity = new MockEntity();
			entity.TheComposition = new List<MockComposition>();
			entity.TheComposition.Add(new MockComposition() { Id = 1, TheString = "1" });
			entity.TheComposition.Add(new MockComposition() { Id = 2, TheString = "2" });
			EntityWrapperInstance.CopyInto(dto, entity);

			Assert.AreEqual(2, entity.TheComposition.Count);
			Assert.AreEqual("update", entity.TheComposition[0].TheString);
			Assert.AreEqual("3", entity.TheComposition[1].TheString);
		}

		[TestMethod]
		public void TestCopyFromEntityToDto()
		{
            MockEntity entity = new MockEntity();
			entity.Id = 1;
			entity.CreationDateTime = new System.DateTime(2017, 11, 3, 12, 01, 02);
			entity.CreationUser = "john";
			entity.LastUpdateDateTime = new System.DateTime(2017, 11, 4, 3, 11, 12);
			entity.LastUpdateUser = "jorge";
			entity.Version = 2;
			entity.TheBoolean = true;
			entity.TheChar = 'a';
			entity.TheDateTime = new DateTime(2017, 12, 21);
			entity.TheDecimal = 1.131M;
			entity.TheDouble = 1.132;
			entity.TheEnumeration = DayOfWeek.Tuesday;
			entity.TheFloat = 1.47f;
			entity.TheInteger = 1;
			entity.TheObject = new object();
			entity.TheShort = 2;
			entity.TheString = "asdf";
			entity.TheReference = new MockAnotherEntity();
			entity.TheReference.Id = 1;
			entity.TheList = new List<int>();
			entity.TheComposition = new List<MockComposition>()
			{
				new MockComposition() { TheString = "x1" },
				new MockComposition() { TheString = "y2" }
			};

			MockDto dto = new MockDto();
			dto.TheNullReference = new EntityReferenceDto(); //vai virar null
			EntityWrapperInstance.CopyInto(entity, dto);

			Assert.AreEqual(entity.Id, dto.Id);
			Assert.AreEqual(entity.CreationDateTime, dto.CreationDateTime);
			Assert.AreEqual(entity.CreationUser, dto.CreationUser);
			Assert.AreEqual(entity.LastUpdateDateTime, dto.LastUpdateDateTime);
			Assert.AreEqual(entity.LastUpdateUser, dto.LastUpdateUser);
			Assert.AreEqual(entity.Version, dto.Version);
			Assert.AreEqual(entity.TheBoolean, dto.TheBoolean);
			Assert.AreEqual(entity.TheChar, dto.TheChar);
			Assert.AreEqual(entity.TheDateTime, dto.TheDateTime);
			Assert.AreEqual(entity.TheDecimal, dto.TheDecimal);
			Assert.AreEqual(entity.TheDouble, dto.TheDouble);
			Assert.AreEqual(entity.TheEnumeration.ToString(), dto.TheEnumeration);
			Assert.AreEqual(entity.TheFloat, dto.TheFloat);
			Assert.AreEqual(entity.TheInteger, dto.TheInteger);
			Assert.IsNull(dto.TheObject, "The only references that are copied are of type EntityReferenceDto.");
			Assert.AreEqual(entity.TheShort, dto.TheShort);
			Assert.AreEqual(entity.TheString, dto.TheString);
			Assert.IsNotNull(dto.TheReference);
			Assert.IsInstanceOfType(entity.TheReference, typeof(MockAnotherEntity));
			Assert.AreEqual(entity.TheReference.Id, dto.TheReference.Id);
			Assert.IsNull(dto.TheList, "Collections are not copied.");
			Assert.IsNull(dto.TheNullReference);
			Assert.IsNotNull(dto.TheComposition);
			Assert.AreEqual(2, dto.TheComposition.Count);
			Assert.AreEqual("x1", dto.TheComposition[0].TheString);
			Assert.AreEqual("y2", dto.TheComposition[1].TheString);
		}

		[TestMethod]
		public void TestWrapToDto()
		{
            MockEntity e = new MockEntity();
			e.Id = 1;
			e.TheBoolean = true;
			e.TheDateTime = new DateTime(2017, 12, 29);
			e.TheReference = new MockAnotherEntity();
			e.TheReference.Id = 2;

			var dto = EntityWrapperInstance.Wrap<MockDto>(e);

			Assert.AreEqual(1, dto.Id);
			Assert.AreEqual(true, dto.TheBoolean);
			Assert.AreEqual(e.TheDateTime, dto.TheDateTime);
			Assert.IsNotNull(dto.TheReference);
			Assert.AreEqual(2, dto.TheReference.Id);
		}

		[TestMethod]
		public void TestWrapToDtoWithComposition()
		{
			MockEntity e = new MockEntity();
			e.Id = 1;
			e.TheString = "me";
			e.TheComposition = new List<MockComposition>();
			e.TheComposition.Add(new MockComposition() 
			{ 
				TheContainerEntity = e, 
				TheString = "i am a composition", 
				TheReference = new MockAnotherEntity() 
				{ 
					TheString = "i am another entity"
				} 
			});

			var dto = EntityWrapperInstance.Wrap<MockPartialDto>(e);

			Assert.AreEqual(1, dto.Id);
			Assert.AreEqual("me", dto.TheString);
		}

		[TestMethod]
		public void TestWrapToEntity()
		{
			MockDto dto = new MockDto();
			dto.Id = 1;
			dto.TheBoolean = true;
			dto.TheDateTime = new DateTime(2017, 12, 29);
			dto.TheReference = new EntityReferenceDto();
			dto.TheReference.Id = 2;
			dto.TheEnumeration = DayOfWeek.Wednesday.ToString();

			var e = EntityWrapperInstance.Wrap<MockEntity>(dto);

			Assert.AreEqual(1, e.Id);
			Assert.AreEqual(true, e.TheBoolean);
			Assert.AreEqual(dto.TheDateTime, e.TheDateTime);
			Assert.IsNotNull(e.TheReference);
			Assert.AreEqual(2, e.TheReference.Id);
			Assert.AreEqual(DayOfWeek.Wednesday, e.TheEnumeration);
		}

		[TestMethod]
		public void TestParseEnumNull()
		{
			MockDto dto = new MockDto();
			dto.TheEnumeration = null;

			var e = EntityWrapperInstance.Wrap<MockEntity>(dto);

			Assert.AreEqual(DayOfWeek.Sunday, e.TheEnumeration, "Non nullable enumeration should get default value for null value.");
		}

		[TestMethod]
		public void TestWrapCollection()
		{
			List<MockDto> dtoList = new List<MockDto>()
			{
				new MockDto()
				{
					Company = "matriz",
					TheReference = new EntityReferenceDto() { Id = 1 },
					TheComposition = new List<MockCompositionDto>() { new MockCompositionDto() { TheString = "x1" } }
				}
			};

			List<MockEntity> entities = EntityWrapperInstance.WrapCollection<MockEntity, MockDto>(dtoList);

			Assert.AreEqual(1, entities.Count);
			Assert.AreEqual("matriz", entities[0].Company);
			Assert.AreEqual(1, entities[0].TheReference.Id);
			Assert.AreEqual(1, entities[0].TheComposition.Count);
			Assert.AreEqual("x1", entities[0].TheComposition[0].TheString);
		}

		[TestMethod]
		public void TestWrapUpdateCollection()
		{
			List<MockDto> dtoList = new List<MockDto>()
			{
				new MockDto()
				{
					Id = 1,
					Company = "matriz",
					TheReference = new EntityReferenceDto() { Id = 1 },
					TheComposition = new List<MockCompositionDto>() { new MockCompositionDto() { Id = 1, TheString = "x1" } }
				}
			};

			List<MockEntity> entities = EntityWrapperInstance.WrapCollection<MockEntity, MockDto>(dtoList);

			Assert.AreEqual(1, entities.Count);
			Assert.AreEqual("matriz", entities[0].Company);
			Assert.AreEqual(1, entities[0].TheReference.Id);
			Assert.AreEqual(1, entities[0].TheComposition.Count);
			Assert.AreEqual("x1", entities[0].TheComposition[0].TheString);
		}

		//TODO nullables
	}
}
