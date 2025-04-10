using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infratructure.Persistence.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure.Persistence.Data.Configurations.Products
{
	internal class CategoryConfigurations : BaseAuditableEntityConfigurations<ProductCategory, int>
	{
		public override void Configure(EntityTypeBuilder<ProductCategory> builder)
		{
			base.Configure(builder);

			builder.Property(b => b.Name)
				.IsRequired();

		}
	}
}
