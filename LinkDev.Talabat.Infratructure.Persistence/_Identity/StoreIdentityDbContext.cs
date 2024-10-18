using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infratructure.Persistence._Identity.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LinkDev.Talabat.Infratructure.Persistence._Identity
{
	public class StoreIdentityDbContext : IdentityDbContext<ApplicationUser>
	{
		public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfiguration(new ApplicationUserConfigurations());
			builder.ApplyConfiguration(new AddressConfigurations());
		}
	}
}
