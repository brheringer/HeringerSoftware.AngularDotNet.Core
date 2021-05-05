using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence.Mapping;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	public class MockCompositionMapping : EntityTypeConfiguration<MockComposition>
	{
		protected override void ConfigureSpecializedFields(EntityTypeBuilder<MockComposition> builder)
		{
			builder.Property(e => e.TheString);
			builder.HasOne(e => e.TheReference);
			//builder.HasOne(e => e.TheEntity).WithMany();
		}
	}
}
