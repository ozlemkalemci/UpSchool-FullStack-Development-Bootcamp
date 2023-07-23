using Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace Infrastructure.SignalR
{
    public class SignalRClient : ISignalRClient
    {
        private HubConnection hubConnection;

        public SignalRClient()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7016/Hubs/SeleniumLogHub") 
                .Build();
        }

        public async Task Connect()
        {
            await hubConnection.StartAsync();
        }

        public async Task SendLogNotification(string logMessage)
        {
            await hubConnection.InvokeAsync("SendLogNotificationAsync", logMessage);
        }
    }
}
