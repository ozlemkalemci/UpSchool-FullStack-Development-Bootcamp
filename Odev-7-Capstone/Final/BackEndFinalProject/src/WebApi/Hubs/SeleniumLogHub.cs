using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    public class SeleniumLogHub : Hub
    {
        public async Task SendLogNotificationAsync(string log)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("NewSeleniumLogAdded", log);
        }
    }
}
