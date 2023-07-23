namespace Application.Common.Interfaces
{
    public interface ISignalRClient
    {
        Task Connect();
        Task SendLogNotification(string logMessage);
    }
}
