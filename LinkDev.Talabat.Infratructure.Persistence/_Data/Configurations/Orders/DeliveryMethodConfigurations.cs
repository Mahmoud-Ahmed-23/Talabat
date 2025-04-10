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
	internal class DeliveryMethodConfigurations : BaseAuditableEntityConfigurations<DeliveryMethod, int>
	{
		public override void Configure(EntityTypeBuilder<DeliveryMethod> builder)
		{
			base.Configure(builder);
			builder.Property(method => method.Cost)
				.HasColumnType("decimal(8,2)");
		}
	}
}
