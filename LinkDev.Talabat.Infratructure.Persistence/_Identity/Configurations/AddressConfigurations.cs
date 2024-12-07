using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure.Persistence._Identity.Configurations
{
	internal class AddressConfigurations : IEntityTypeConfiguration<Address>
	{

		public void Configure(EntityTypeBuilder<Address> builder)
		{
			builder.ToTable("Addresses");
		}
	}
}
