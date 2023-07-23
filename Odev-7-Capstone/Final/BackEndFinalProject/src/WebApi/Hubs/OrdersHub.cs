using Application.Common.Models.Crawler;
using Application.Common.Models.Order;
using Application.Common.Models.WorkerService;
using Domain.Utilities;
using Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Hubs
{
    public class OrdersHub:Hub
    {
        private readonly ApplicationDbContext _dbcontext;

        public OrdersHub(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<bool> DeleteAsync(Guid orderId)
        {
            var order = await _dbcontext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

            if (order is null) return false;

            _dbcontext.Orders.Remove(order);

            await _dbcontext.SaveChangesAsync();

            await Clients.AllExcept(Context.ConnectionId).SendAsync(SignalRMethodKeys.Orders.Deleted, orderId);

            return true;
        }

        //[Authorize]
        //public async Task Added(CrawlOrderDto crawlOrderDto, CancellationToken cancellationToken)
        //{
        //    var accessToken = Context.GetHttpContext().Request.Query["access_token"];
        //    await Clients.All.SendAsync(SignalRMethodKeys.Orders.Added, new WorkerServiceNewOrderAddedDto(crawlOrderDto, accessToken));

        //}

       
    }
}
