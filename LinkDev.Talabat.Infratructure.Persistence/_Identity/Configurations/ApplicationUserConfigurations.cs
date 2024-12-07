using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infratructure.Persistence._Identity.Configurations
{
	internal class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.Property(u => u.DisplayName)
				.IsRequired()
				.HasMaxLength(100)
				.HasColumnType("varchar");

			builder.HasOne(u => u.Address)
				.WithOne(a => a.User)
				.HasForeignKey<Address>(a => a.UserId)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
