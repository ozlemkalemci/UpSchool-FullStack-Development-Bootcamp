using Application.Common.Models.Crawler;
using Application.Common.Models.Order;
using Application.Utilities;
using Domain.Enums;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Headers;
using System.Net.Http;
using Domain.Utilities;
using System.Net.Http.Json;
using Application.Common.Models.WorkerService;

namespace CrawlerWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Crawler _crawler;
        private readonly HubConnection _connection;
        private readonly HttpClient _httpClient;

        public Worker(ILogger<Worker> logger, Crawler crawler, HttpClient httpClient)
        {
            _logger = logger;
            _crawler = crawler;
            _httpClient = httpClient;

            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7016/Hubs/OrdersHub")
            .WithAutomaticReconnect()
            .Build();
            _httpClient = httpClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            //_connection.On<WorkerServiceNewOrderAddedDto>(SignalRMethodKeys.Orders.Added, async (newOrderAddedDto) =>
            //{
            //    Console.WriteLine($"Our access token is {newOrderAddedDto.AccessToken}");

            //    // Crawler.StartAsync(order)

            //    await Task.Delay(10000, stoppingToken);

            //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newOrderAddedDto.AccessToken);

            //    var result = await _httpClient.PostAsJsonAsync("api/ProductCrawler/PostOrderAsync", newOrderAddedDto, stoppingToken);

            //});

            //await _connection.StartAsync(stoppingToken);

            //Console.WriteLine(_connection.State.ToString());
            //Console.WriteLine(_connection.ConnectionId);

            ////_logger.LogInformation("Crawler Worker Service is starting.");

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    //try
            //    //{
                    
            //    //    CrawlOrderDto crawlOrderDto = new CrawlOrderDto
            //    //    {
            //    //        CrawlType = CrawlType.All, 
            //    //        IsAmountEntered = true, 
            //    //        RequestedAmount = 10, 
            //    //        IsDownloadChecked = true, 
            //    //        IsEmailChecked = false, 
            //    //        Email = "destek@example.com" 
            //    //    };

            //    //    OrderDto orderResults = await _crawler.OrderResults(crawlOrderDto);

                    
            //    //    _logger.LogInformation($"Crawler iþlemi tamamlandý. Bulunan ürün sayýsý: {orderResults.TotalFoundAmount}");

                    
            //    //    int intervalInSeconds = 3600; 
            //    //    await Task.Delay(TimeSpan.FromSeconds(intervalInSeconds), stoppingToken);
            //    //}
            //    //catch (Exception ex)
            //    //{
            //    //    _logger.LogError(ex, "Crawler Worker Service hatasý oluþtu.");
            //    //}
            //}

            //_logger.LogInformation("Crawler Worker Service is stopping.");
        }
    }
}