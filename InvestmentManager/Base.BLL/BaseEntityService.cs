using Base.Contracts.Base;
using Base.Contracts.BLL;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace Base.BLL;

public class BaseEntityService<TPublicEntity, TBllEntity, TDalEntity, TRepository> :
        BaseEntityService<TPublicEntity, TBllEntity, TDalEntity, TRepository, Guid>, 
        IEntityService<TPublicEntity, TBllEntity>
    where TPublicEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
    where TBllEntity : class, IDomainEntityId
    where TRepository : IEntityRepository<TDalEntity>
{
    public BaseEntityService(TRepository repository, IMapper<TBllEntity, TDalEntity> bllMapper, 
        IMapper<TPublicEntity, TBllEntity> publicMapper) : base(repository, bllMapper, publicMapper)
    {
    }
}

public class BaseEntityService<TPublicEntity, TBllEntity, TDalEntity, TRepository, TKey> : IEntityService<TPublicEntity, TBllEntity, TKey>
    where TPublicEntity : class, IDomainEntityId<TKey>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TRepository : IEntityRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
{
    protected TRepository Repository;
    protected IMapper<TBllEntity, TDalEntity> BLLMapper;
    protected IMapper<TPublicEntity, TBllEntity> PublicMapper;

    public BaseEntityService(TRepository repository, IMapper<TBllEntity, TDalEntity> bllMapper, 
        IMapper<TPublicEntity, TBllEntity> publicMapper)
    {
        Repository = repository;
        BLLMapper = bllMapper;
        PublicMapper = publicMapper;
    }

    public TBllEntity Add(TBllEntity entity)
    {
        return BLLMapper.Map(Repository.Add(BLLMapper.Map(entity)!))!;
    }
    
    public TPublicEntity Add(TPublicEntity entity)
    {
        return PublicMapper.Map(Add(PublicMapper.Map(entity)!))!;
    }
    
    public TBllEntity Update(TBllEntity entity)
    {
        return BLLMapper.Map(Repository.Update(BLLMapper.Map(entity)!))!;
    }
    
    public TPublicEntity Update(TPublicEntity entity)
    {
        return PublicMapper.Map(Update(PublicMapper.Map(entity)!))!;
    }

    public TBllEntity Remove(TBllEntity entity)
    {
        return BLLMapper.Map(Repository.Remove(BLLMapper.Map(entity)!))!;
    }

    public TBllEntity Remove(TKey id)
    {
        return BLLMapper.Map(Repository.Remove(id))!;
    }

    public TBllEntity? FirstOrDefault(TKey id, bool noTracking = true)
    {
        return BLLMapper.Map(Repository.FirstOrDefault(id, noTracking))!;
    }

    public IEnumerable<TBllEntity> GetAll(bool noTracking = true)
    {
        return Repository.GetAll(noTracking).Select(x => BLLMapper.Map(x)!);
    }

    public bool Exists(TKey id)
    {
        return Repository.Exists(id);
    }
    
    public async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
    {
        return BLLMapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
    }
    

    public async Task<IEnumerable<TBllEntity>> GetAllAsync(bool noTracking = true)
    {
        return (await Repository.GetAllAsync(noTracking)).Select(x => BLLMapper.Map(x)!);
    }
    

    public Task<bool> ExistsAsync(TKey id)
    {
        return Repository.ExistsAsync(id);
    }

    public async Task<TBllEntity> RemoveAsync(TKey id)
    {
        return BLLMapper.Map(await Repository.RemoveAsync(id))!;
    }
    
    
    // TODO: GET all without user, DELETE later
    public async Task<IEnumerable<TPublicEntity>> GetAll()
    {
        return (await GetAllAsync()).Select(x => PublicMapper.Map(x)!);
    }
    



}