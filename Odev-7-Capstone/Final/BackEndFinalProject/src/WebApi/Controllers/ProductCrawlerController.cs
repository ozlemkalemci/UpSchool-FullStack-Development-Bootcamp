using Application.Common.Interfaces;
using Application.Common.Models.Crawler;
using Application.Common.Models.Order;
using Application.Common.Models.OrderEvent;
using Application.Common.Models.Product;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using Domain.Utilities;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApi.Hubs;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCrawlerController : ControllerBase
    {
        private readonly Crawler _productCrawler;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly CrawlOrderDto _crawlOrderDto;
        private readonly IExcelService _excelService;
        private readonly IEmailService _emailService;
        private readonly IHubContext<SeleniumLogHub> _seleniumLogHubContext;
        private readonly IHubContext<OrdersHub> _ordersHubContext;
        private readonly ICurrentUserService _currentUserService;


        public ProductCrawlerController(IMapper mapper, ApplicationDbContext dbContext, Crawler productCrawler, IExcelService excelService, IEmailService emailService, IHubContext<SeleniumLogHub> seleniumLogHubContext, ICurrentUserService currentUserService, IHubContext<OrdersHub> ordersHubContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _productCrawler = productCrawler;
            _excelService = excelService;
            _emailService = emailService;
            _seleniumLogHubContext = seleniumLogHubContext;
            _currentUserService = currentUserService;
            _ordersHubContext = ordersHubContext;
        }



        [HttpGet]
        [Route("GetAllOrdersAsync")]
        public async Task<IActionResult> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            // Kullanıcının UserId'sini alıyoruz
            var userId = _currentUserService.UserId;

            // Kullanıcının UserId'sine göre siparişleri çekiyoruz
            var orders = await _dbContext.Orders
                .Include(o => o.OrderEvents)
                .Include(o => o.Products)
                .Where(o => o.UserId == userId)
                .ToListAsync(cancellationToken);

            var orderDtos = orders.Select(OrderDto.MapFromOrder).ToList();

            return Ok(orderDtos);
        }

        [HttpGet("GetTablesByIdAsync")]
        public IActionResult GetById(Guid id)
        {
            var order = _dbContext.Orders
                .Include(o => o.Products)
                .Include(o => o.OrderEvents)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<OrderEvent, OrderEventDto>();
                cfg.CreateMap<Product, ProductDto>();
            });

            var mapper = new Mapper(config);

            var orderDto = mapper.Map<OrderDto>(order);

            return Ok(orderDto);
        }




        [HttpPost]
        [Route("PostOrderAsync")]
        public async Task<IActionResult> AddRangeAsync(CrawlOrderDto crawlOrderDto, CancellationToken cancellationToken)
        {
            var crawledProducts = await _productCrawler.OrderResults(crawlOrderDto);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = _currentUserService.UserId,
                CrawlType = crawledProducts.CrawlType,
                TotalFoundAmount = crawledProducts.TotalFoundAmount,
                RequestedAmount = crawledProducts.RequestedAmount,
                CreatedOn = DateTimeOffset.Now,
                OrderEvents = crawledProducts.OrderEvents.Select(p => new OrderEvent
                {
                    Id = Guid.NewGuid(),
                    Status = p.Status,
                    CreatedOn = DateTimeOffset.Now,
                }).ToList(),
                Products = crawledProducts.Products.Select(p => new Product
                {
                    Id = Guid.NewGuid(),
                    Name = p.Name,
                    Price = p.Price,
                    SalePrice = p.SalePrice,
                    Picture = p.Picture,
                    IsOnSale = p.IsOnSale,
                    CreatedOn = DateTimeOffset.Now,
                }).ToList()
            };

            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var orderDto = OrderDto.MapFromOrder(order);

            await _ordersHubContext.Clients.All.SendAsync(SignalRMethodKeys.Orders.Added, orderDto,cancellationToken);

            
            var products = orderDto.Products; // excel işlemleri için

            if (crawlOrderDto.IsEmailChecked == false && crawlOrderDto.IsDownloadChecked == false)
            {
                return Ok(orderDto);
            }
            else if (crawlOrderDto.IsEmailChecked == true && crawlOrderDto.IsDownloadChecked == true)
            {
                byte[] excelData = await _excelService.GenerateExcelFileAsync(products);
                var excelFileName = $"{DateTimeOffset.Now:yyyyMMdd_HHmmss}_products.xlsx";

                await _emailService.SendEmailWithAttachmentAsync(crawlOrderDto, excelData, excelFileName);

                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelFileName);
            }
            else if (crawlOrderDto.IsEmailChecked == true && crawlOrderDto.IsDownloadChecked == false)
            {
                byte[] excelData = await _excelService.GenerateExcelFileAsync(products);
                var excelFileName = $"{DateTimeOffset.Now:yyyyMMdd_HHmmss}_products.xlsx";

                await _emailService.SendEmailWithAttachmentAsync(crawlOrderDto, excelData, excelFileName);
                return Ok(orderDto);
            }
            else
            {
                byte[] excelData = await _excelService.GenerateExcelFileAsync(products);

                var excelFileName = $"{DateTimeOffset.Now:yyyyMMdd_HHmmss}_products.xlsx";
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelFileName);
            }
        }


        [HttpDelete("DeleteOrderAsync/{id:guid}")]
        public async Task<IActionResult> DeleteOrderAsync(Guid id, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var order = await _dbContext.Orders
                .Include(o => o.Products)
                .Include(o => o.OrderEvents)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId, cancellationToken);

            if (order == null)
            {
                return NotFound();
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _ordersHubContext.Clients.All.SendAsync(SignalRMethodKeys.Orders.Deleted, id, cancellationToken);

            return Ok();
        }
    }
}


