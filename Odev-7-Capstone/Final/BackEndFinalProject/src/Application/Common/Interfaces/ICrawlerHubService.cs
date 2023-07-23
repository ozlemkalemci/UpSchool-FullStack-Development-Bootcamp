namespace Application.Common.Interfaces
{
    public interface ICrawlerHubService
    {
        Task RemovedAsync(Guid id, CancellationToken cancellationToken);
    }
}
