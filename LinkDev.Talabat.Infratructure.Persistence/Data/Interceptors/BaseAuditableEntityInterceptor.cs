using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure.Persistence.Data.Interceptors
{
	internal class BaseAuditableEntityInterceptor : SaveChangesInterceptor
	{
		private readonly ILoggedInUserService _loggedInUserService;

		public BaseAuditableEntityInterceptor(ILoggedInUserService loggedInUserService)
		{
			_loggedInUserService = loggedInUserService;
		}

		public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
		{
			UpdateEntity(eventData.Context);
			return base.SavedChangesAsync(eventData, result, cancellationToken);
		}
		private void UpdateEntity(DbContext? dbContext)
		{
			if (dbContext is null) return;

			var utcNow = DateTime.UtcNow;
			foreach (var entry in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>())
			{
				if (entry is { State: EntityState.Added or EntityState.Modified })
				{
					entry.Entity.CreatedBy = "";
					entry.Entity.CreatedOn = utcNow;
				}
				entry.Entity.LastModifiedBy = "";
				entry.Entity.LastModifiedOn = utcNow;
			}
		}
	}
}
