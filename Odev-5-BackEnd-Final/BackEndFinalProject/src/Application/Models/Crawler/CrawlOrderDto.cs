using Domain.Enums;

namespace Application.Models.Crawler
{
    public class CrawlOrderDto
    {
        public CrawlType CrawlType { get; set; }
        public int RequestedAmount { get; set; }
        public bool IsChecked { get; set; }


        public CrawlOrderDto()
        {
            CrawlType = 0;
            RequestedAmount = 15;
            IsChecked = true;

        }
    }
}
