using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infratructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure.Persistence.Repositories.GenericRepository
{
    internal class GenericRepository<TEntity, TKey> :
		IGenericRepsitory<TEntity, TKey> where TEntity :
		BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
	{

		private readonly StoreContext _dbContext;

		public GenericRepository(StoreContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTraking)
		{
			if (typeof(TEntity) == typeof(Product))
			{
				return (IEnumerable<TEntity>)(withTraking ? await _dbContext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync()
				: await _dbContext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).AsNoTracking().ToListAsync());

			}
			return withTraking ? await _dbContext.Set<TEntity>().ToListAsync()
				: await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

		}

		public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec)
		{
			return await ApplySpecifications(spec).ToListAsync();
		}
		
		public async Task<TEntity> GetAsync(TKey id)
		{
			if (typeof(TEntity) == typeof(Product))
			{
				return await _dbContext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id.Equals(id)) as TEntity;
			}
			return await _dbContext.Set<TEntity>().FindAsync(id);
		}

		public async Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec)
		{
			return await ApplySpecifications(spec).FirstOrDefaultAsync();
		}

		public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec)
		{
			return await ApplySpecifications(spec).CountAsync();
		}
		public async Task AddAsync(TEntity entity)
		=> await _dbContext.Set<TEntity>().AddAsync(entity);

		public void Update(TEntity entity)
		=> _dbContext.Set<TEntity>().Update(entity);

		public void Delete(TEntity entity)
		=> _dbContext.Set<TEntity>().Remove(entity);



		#region Helpers
		private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> spec)
		{
			return SpecificationsEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), spec);
		}





		#endregion


	}
}
