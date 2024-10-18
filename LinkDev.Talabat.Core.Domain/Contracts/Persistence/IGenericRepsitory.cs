using LinkDev.Talabat.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts.Persistence
{
    public interface IGenericRepsitory<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
    {

        Task<IEnumerable<TEntity>> GetAllAsync(bool withTraking = true);
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec);

        Task<TEntity> GetAsync(TKey id);
        Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec);

        Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec);
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
