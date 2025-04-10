using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure.Persistence._Common
{
	public abstract class DbInitializer(DbContext _dbContext) : IDbInitializer
	{
		
		public virtual async Task InitializeAsync()
		{
			var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

			if (pendingMigrations.Any())
				await _dbContext.Database.MigrateAsync(); // Update-Database
		}

		public abstract Task SeedAsync();
	}
}
