namespace Domain.Common
{
    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {
        public TKey Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }


    }
}
