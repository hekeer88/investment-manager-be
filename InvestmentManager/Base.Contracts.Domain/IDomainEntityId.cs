namespace Base.Contracts.Domain;

// default Guid based domain entity interface
public interface IDomainEntityId : IDomainEntityId<Guid>
{
}


// universal domain entity interface
public interface IDomainEntityId<TKey>
where TKey: IEquatable<TKey>
{
    public TKey Id { get; set; }
}