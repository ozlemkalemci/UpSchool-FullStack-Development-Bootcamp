using Application.Common.Interfaces;
using Application.Models.Crawler;
using Application.Models.Order;
using Application.Models.OrderEvent;
using Application.Models.Product;
using Domain.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Application.Utilities
{
    public class Crawler
    {
        private readonly ISignalRClient _signalRClient;
        public Crawler(ISignalRClient signalRClient)
        {
            this._signalRClient = signalRClient;
            Task.Run(() => _signalRClient.Connect()).Wait();
        }
        public async Task<OrderDto> OrderResults(CrawlOrderDto _crawlOrderDto)
        {
            var crawlOrderDto = new CrawlOrderDto 
            {
                CrawlType = _crawlOrderDto.CrawlType,
                RequestedAmount = _crawlOrderDto.RequestedAmount,
                IsChecked = _crawlOrderDto.IsChecked,

            };

            var orderResults = new List<OrderDto>();
            var crawledProducts = new List<ProductDto>();
            var eventList = new List<OrderEventDto>();
            int requestedAmount;

            // Ürün sayısı girilsin mi?

            if (crawlOrderDto.RequestedAmount > 0)
            {
                requestedAmount = crawlOrderDto.RequestedAmount;
            }
            else
            {
                requestedAmount = int.MaxValue;
            }

            new DriverManager().SetUpDriver(new ChromeConfig());
            IWebDriver driver = new ChromeDriver();

            var startEvent = new OrderEventDto
            {
                Status = OrderStatus.BotStarted,
                CreatedOn = DateTimeOffset.Now,
            };

            eventList.Add(startEvent);

            await _signalRClient.SendLogNotification($"✔ Bot Started.................{DateTimeOffset.Now}");

            // Siteye giriş
            driver.Navigate().GoToUrl("https://finalproject.dotnet.gg/");
            Thread.Sleep(1000);

            var crawlingStartedEvent = new OrderEventDto
            {
                Status = OrderStatus.CrawlingStarted,
                CreatedOn = DateTimeOffset.Now,
            };

            eventList.Add(crawlingStartedEvent);


            // Sayfa itemlerini bulma
            IReadOnlyCollection<IWebElement> pages = driver.FindElements(By.CssSelector(".page-item"));

            // Ürünü tutan liste ve sayaçlar

            int pageCounter = 1;
            int foundProductCount = 0;

            while (foundProductCount < requestedAmount)
            {
                IReadOnlyCollection<IWebElement> products = driver.FindElements(By.CssSelector(".card.h-100"));

                foreach (IWebElement p in products)
                {
                    if (foundProductCount >= requestedAmount)
                        break;

                    string productInfo = p.Text;

                    productInfo = productInfo.Replace("Add to cart", "");
                    productInfo = productInfo.Replace("Sale", "");
                    productInfo = productInfo.Replace("$", "; $");

                    if (productInfo.IndexOf("$") != -1 && productInfo.IndexOf("$") == productInfo.LastIndexOf("$"))
                    {
                        productInfo += "; null";
                    }

                    productInfo = productInfo.Replace("\r\n", "");

                    IWebElement productImage = p.FindElement(By.TagName("img"));
                    string imageUrl = productImage.GetAttribute("src");
                    imageUrl = imageUrl.Replace("https://finalproject.dotnet.gg/productPics/", "");
                    productInfo = productInfo + "; " + imageUrl;

                    var productParts = productInfo.Replace("$", "").Split(";");
                    var productName = productParts[0];
                    var price = decimal.Parse(productParts[1]);
                    var sale = productParts[2];
                    var picture = productParts[3];

                    var newProduct = new ProductDto()
                    {
                        Id = Guid.NewGuid(),
                        Name = productName,
                        Price = price,
                        SalePrice = sale,
                        Picture = picture,
                        IsOnSale = sale != " null",
                        CreatedOn = DateTimeOffset.Now,
                    };

                    switch (crawlOrderDto.CrawlType)
                    {
                        case CrawlType.All:
                            crawledProducts.Add(newProduct);
                            Thread.Sleep(500);
                            foundProductCount++;
                            await _signalRClient.SendLogNotification($"{foundProductCount}. Product Added...............{DateTimeOffset.Now}");
                            
                            break;
                        case CrawlType.OnSale:
                            if (newProduct.IsOnSale)
                            {
                                crawledProducts.Add(newProduct);
                                Thread.Sleep(500);
                                foundProductCount++;
                                await _signalRClient.SendLogNotification($"{foundProductCount}. Product Added...............{DateTimeOffset.Now}");
                            
                            }
                            break;
                        case CrawlType.NormalPrice:
                            if (!newProduct.IsOnSale)
                            {
                                crawledProducts.Add(newProduct);
                                Thread.Sleep(500);
                                foundProductCount++;
                                await _signalRClient.SendLogNotification($"{foundProductCount}. Product Added...............{DateTimeOffset.Now}");

                            }
                            break;
                    }


                    
                    if (foundProductCount >= requestedAmount)
                        break;

                    

                }

                pageCounter++;

                if (pageCounter >= pages.Count)
                    break;

                await _signalRClient.SendLogNotification($"➤ Moved To The {pageCounter}. Page........{DateTimeOffset.Now}");
                driver.Navigate().GoToUrl($"https://finalproject.dotnet.gg/?currentPage={pageCounter}");

                Thread.Sleep(1000);
            }
            var crawlingCompleted = new OrderEventDto
            {
                Status = OrderStatus.CrawlingCompleted,
                CreatedOn = DateTimeOffset.Now,
            };

            eventList.Add(crawlingCompleted);

            var orderDto = new OrderDto
            {
                RequestedAmount = requestedAmount,
                TotalFoundAmount = foundProductCount,
                CrawlType = crawlOrderDto.CrawlType,
                OrderEvents = eventList,
                Products = crawledProducts,
                CreatedOn = DateTimeOffset.Now,

            };

            var orderCompleted = new OrderEventDto
            {
                Status = OrderStatus.OrderCompleted,
                CreatedOn = DateTimeOffset.Now,
            };

            eventList.Add(orderCompleted);
            await _signalRClient.SendLogNotification("_________________________________________________________");
            await _signalRClient.SendLogNotification($"✔ The Crawling Process Was Completed. ");
            await _signalRClient.SendLogNotification($"   {DateTimeOffset.Now}");
            await _signalRClient.SendLogNotification("_________________________________________________________");
            await _signalRClient.SendLogNotification(" ");
            await _signalRClient.SendLogNotification("★ Total Amount Of Products Found: " + orderDto.TotalFoundAmount);
            await _signalRClient.SendLogNotification(" ");
            await _signalRClient.SendLogNotification("Driver Closed.");
            driver.Close();
            return orderDto;

        }

    }

}
