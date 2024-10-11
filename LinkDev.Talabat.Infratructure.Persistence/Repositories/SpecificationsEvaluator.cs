using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure.Persistence.Repositories
{
	internal static class SpecificationsEvaluator<TEntity, TKey>
		where TEntity : BaseAuditableEntity<TKey>
		where TKey : IEquatable<TKey>
	{

		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
		{
			var query = inputQuery; //dbcontext.set<TEntity>

			if (spec.Criteria is not null)
				query = query.Where(spec.Criteria); //dbcontext.set<TEntity>.Where(E => E.Id == id)

			if(spec.OrderByDesc is not null)
				query = query.OrderByDescending(spec.OrderByDesc);
			else if(spec.OrderBy is not null)
				query = query.OrderBy(spec.OrderBy);


			//dbcontext.set<TEntity>.Where(E => E.Id == id).Include(P => P.Brand)....

			query = spec.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

			return query;
		}
	}
}