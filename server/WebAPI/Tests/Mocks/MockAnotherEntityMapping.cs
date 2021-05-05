using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence.Mapping;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	public class MockAnotherEntityMapping : EntityTypeConfiguration<MockAnotherEntity>
	{
		protected override void ConfigureSpecializedFields(EntityTypeBuilder<MockAnotherEntity> builder)
		{
		}
	}
}
