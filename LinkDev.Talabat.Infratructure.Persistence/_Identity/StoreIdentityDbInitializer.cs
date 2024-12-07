using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infratructure.Persistence._Common;
using LinkDev.Talabat.Infratructure.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure.Persistence._Identity
{
	public class StoreIdentityDbInitializer(StoreIdentityDbContext dbContext, UserManager<ApplicationUser> userManager) : DbInitializer(dbContext), IStoreIdentityDbInitializer
	{

		public override async Task SeedAsync()
		{
			var user = new ApplicationUser
			{
				DisplayName = "Mahmoud Ahmed",
				UserName = "Mahmoud.Ahmed",
				Email = "mahmoud.ahmed@gmail.com",
				PhoneNumber = "01324124112",
			};

			await userManager.CreateAsync(user, "P@assw0rd");

		}
	}
}
