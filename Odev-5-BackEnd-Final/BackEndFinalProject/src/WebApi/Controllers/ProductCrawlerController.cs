using Application.Common.Interfaces;
using Application.Models.Crawler;
using Application.Models.Order;
using Application.Models.OrderEvent;
using Application.Models.Product;
using Application.Models.Email;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApi.Hubs;

namespace WebApi.Controllers
{
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

        public ProductCrawlerController(IMapper mapper, ApplicationDbContext dbContext, Crawler productCrawler, IExcelService excelService, IEmailService emailService, IHubContext<SeleniumLogHub> seleniumLogHubContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _productCrawler = productCrawler;
            _excelService = excelService;
            _emailService = emailService;
            _seleniumLogHubContext = seleniumLogHubContext;

        }

        [HttpGet]
        [Route("GetAllOrdersAsync")]
        public async Task<IActionResult> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            var orders = await _dbContext.Orders
                .Include(o => o.OrderEvents)
                .Include(o => o.Products)
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
                CrawlType = crawledProducts.CrawlType,
                TotalFoundAmount = crawledProducts.TotalFoundAmount,
                RequestedAmount = crawledProducts.RequestedAmount,
                CreatedOn = DateTimeOffset.Now,
                OrderEvents = crawledProducts.OrderEvents.Select(p => new OrderEvent
                {
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

            var products = orderDto.Products;

            if (crawlOrderDto.IsChecked == false)
            {
                return Ok(orderDto);
            }
            else
            {
                
                byte[] excelData = await _excelService.GenerateExcelFileAsync(products);

                var excelFileName = $"{DateTimeOffset.Now:yyyyMMdd_HHmmss}_products.xlsx";
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelFileName);
            }
            
        }

        
        [HttpPost]
        [Route("SendEmail")]
        public async Task<IActionResult> SendMailAsync(SendEmailConfirmationDto sendEmailConfirmationDto, CancellationToken cancellationToken)
        {
            await _emailService.SendEmailWithAttachmentAsync(new SendEmailConfirmationDto()
            {
                Email = sendEmailConfirmationDto.Email,
            });

            return Ok();

        }



        [HttpDelete]
        [Route("DeleteOrderAsync")]
        public async Task<IActionResult> DeleteRangeAsync(CancellationToken cancellationToken)
        {
            var products = await _dbContext.Products.ToListAsync(cancellationToken);
            _dbContext.Products.RemoveRange(products);
            await _dbContext.SaveChangesAsync(cancellationToken);


            return Ok();
        }
    }
}


