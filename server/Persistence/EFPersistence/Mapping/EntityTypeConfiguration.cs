using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence.Mapping
{
	public abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : Model.Entity
	{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
			builder.ToTable(typeof(T).Name);

			builder.HasKey(entity => entity.Id);

			builder.Property(entity => entity.Id).IsRequired().ValueGeneratedOnAdd();
			builder.Property(entity => entity.CreationDateTime).IsRequired();
			builder.Property(entity => entity.CreationUser).IsRequired();
			builder.Property(entity => entity.LastUpdateDateTime).IsRequired();
			builder.Property(entity => entity.LastUpdateUser).IsRequired();
			builder.Property(entity => entity.Version).IsRequired().IsConcurrencyToken();

			ConfigureSpecializedFields(builder);
		}

		protected abstract void ConfigureSpecializedFields(EntityTypeBuilder<T> builder);

		protected string GetPropertyForeignKeyName(string propertyName)
		{
			return $"{propertyName}Id";
		}
	}
}
