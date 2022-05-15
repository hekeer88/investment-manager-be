
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace Base.Contracts.BLL;

public interface IEntityService<TPublicEntity, TBllEntity> : IEntityRepository<TBllEntity>, IEntityService<TPublicEntity, 
    TBllEntity, Guid>
    where TBllEntity: class, IDomainEntityId
{
    
}

public interface IEntityService<TPublicEntity, TEntity, TKey> : IEntityRepository<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    TPublicEntity Add(TPublicEntity entity);
    TPublicEntity Update(TPublicEntity entity);

}
