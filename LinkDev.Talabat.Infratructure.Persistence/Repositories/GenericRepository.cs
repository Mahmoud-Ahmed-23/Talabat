using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infratructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure.Persistence.Repositories
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
		=> (withTraking) ? await _dbContext.Set<TEntity>().ToListAsync()
			: await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

		public async Task<TEntity> GetAsync(TKey id)
			=> await _dbContext.Set<TEntity>().FindAsync(id);

		public async Task AddAsync(TEntity entity)
		=> await _dbContext.Set<TEntity>().AddAsync(entity);
		public void Update(TEntity entity)
		=> _dbContext.Set<TEntity>().Update(entity);

		public void Delete(TEntity entity)
		=> _dbContext.Set<TEntity>().Remove(entity);


	}
}
