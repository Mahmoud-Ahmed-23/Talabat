using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure.Persistence.Data.Configurations.Base
{
	internal class BaseAuditableEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
		where TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
	{
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.Property(p => p.Id)
				.ValueGeneratedOnAdd();

			builder.Property(p => p.CreatedBy)
				.IsRequired();

			builder.Property(p => p.CreatedOn)
				.IsRequired();

			builder.Property(p => p.LastModifiedBy)
				.IsRequired();

			builder.Property(p => p.LastModifiedOn)
				.IsRequired();

		}
	}
}
