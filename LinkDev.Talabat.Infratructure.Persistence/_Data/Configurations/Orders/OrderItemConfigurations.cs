using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Infratructure.Persistence.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure.Persistence._Data.Configurations.Orders
{
	internal class OrderItemConfigurations : BaseAuditableEntityConfigurations<OrderItem, int>
	{
		public override void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			base.Configure(builder);
			builder.OwnsOne(item => item.Product, product => product.WithOwner());
			builder.Property(item => item.Price)
				.HasColumnType("decimal(8,2)");
		}
	}
}