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
	public class AuditInterceptor : SaveChangesInterceptor
	{
		private readonly ILoggedInUserService _loggedInUserService;

		public AuditInterceptor(ILoggedInUserService loggedInUserService)
		{
			_loggedInUserService = loggedInUserService;
		}

		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			UpdateData(eventData.Context);
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			UpdateData(eventData.Context);

			return base.SavingChanges(eventData, result);
		}

		private void UpdateData(DbContext? dbContext)
		{

			if (dbContext is null)
				return;

			foreach (var entry in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>()
				.Where(entity => entity.State is EntityState.Added or EntityState.Modified))
			{
				if (entry.State is EntityState.Added)
				{
					entry.Entity.CreatedBy = _loggedInUserService.UserId!;
					entry.Entity.CreatedOn = DateTime.UtcNow;
				}
				entry.Entity.LastModifiedBy = _loggedInUserService.UserId!;
				entry.Entity.LastModifiedOn = DateTime.UtcNow;
			}
		}
	}
}
