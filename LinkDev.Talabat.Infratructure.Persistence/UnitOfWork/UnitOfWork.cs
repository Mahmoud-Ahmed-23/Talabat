﻿using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infratructure.Persistence.Data;
using LinkDev.Talabat.Infratructure.Persistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure.Persistence.UnitOfWork
{
	internal class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _dbContext;
		private readonly ConcurrentDictionary<string, object> _repositories;

		public UnitOfWork(StoreContext dbContext)
		{
			_dbContext = dbContext;
			_repositories = new();
		}

		public Task<int> CompleteAsync()
		=> _dbContext.SaveChangesAsync();

		public ValueTask DisposeAsync()
		=> _dbContext.DisposeAsync();

		public IGenericRepsitory<TEntity, TKey> GetRepsitory<TEntity, TKey>()
			where TEntity : BaseAuditableEntity<TKey>
			where TKey : IEquatable<TKey>
		{
			///var typeName = typeof(TEntity).Name;
			///if (_repositories.ContainsKey(typeName))
			///	return (IGenericRepsitory<TEntity, TKey>)_repositories[typeName];
			///var repositry = new GenericRepository<TEntity, TKey>(_dbContext);
			///_repositories.Add(typeName, repositry);
			///return repositry;
			///


			return (IGenericRepsitory<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_dbContext));

		}
	}
}
