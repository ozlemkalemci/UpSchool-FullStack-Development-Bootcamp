namespace Domain.Common
{
    public interface IEntityBase<TKey>
    {
        TKey Id { get; set; }
    }
}
