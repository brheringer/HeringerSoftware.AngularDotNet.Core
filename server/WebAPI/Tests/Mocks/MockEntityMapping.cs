using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence.Mapping;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	public class MockEntityMapping : EntityTypeConfiguration<MockEntity>
	{
		protected override void ConfigureSpecializedFields(EntityTypeBuilder<MockEntity> builder)
		{
			builder.Ignore(e => e.TheObject);
			builder.Ignore(e => e.TheList);
			builder.HasOne(e => e.TheReference);
			builder.HasOne(e => e.TheNullReference); //.WithMany().IsRequired(false).OnDelete(DeleteBehavior.Restrict);
			builder.HasMany(e => e.TheComposition);
		}
	}
}
