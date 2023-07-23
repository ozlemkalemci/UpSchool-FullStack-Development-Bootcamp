using Domain.Enums;

namespace Application.Common.Models.Crawler
{
    public class CrawlOrderDto
    {
        public CrawlType CrawlType { get; set; }
        public bool IsAmountEntered { get; set; }
        public int RequestedAmount { get; set; }
        public bool IsDownloadChecked { get; set; }
        public bool IsEmailChecked { get; set; }
        public string Email { get; set; }


        public CrawlOrderDto()
        {
            CrawlType = 0;
            IsAmountEntered = true;
            RequestedAmount = 0;
            IsDownloadChecked = false;
            IsEmailChecked = false;
            Email = string.Empty;
        }
    }
}
