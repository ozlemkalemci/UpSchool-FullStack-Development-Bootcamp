namespace Domain.Common
{
    public abstract class EntityBase<TKey> : IEntityBase<TKey>, ICreatedByEntity
    {
        public TKey Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
        public string? CreatedByUserId { get; set; }
    }
}
