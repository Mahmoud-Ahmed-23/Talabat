using LinkDev.Talabat.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
	public interface IGenericRepsitory<TEntity, TKey> 
		where TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
	{

		Task<IEnumerable<TEntity>> GetAllAsync(bool withTraking);

		Task<TEntity> GetAsync(TKey id);

		Task AddAsync(TEntity entity);

		void Update(TEntity entity);

		void Delete(TEntity entity);
	}
}
