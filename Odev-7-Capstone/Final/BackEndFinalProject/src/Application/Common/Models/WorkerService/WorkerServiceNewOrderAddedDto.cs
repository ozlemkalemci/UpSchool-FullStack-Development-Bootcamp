using Application.Common.Models.Crawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models.WorkerService
{
    public class WorkerServiceNewOrderAddedDto
    {
        public CrawlOrderDto Order { get; set; }
        public string AccessToken { get; set; }
        public WorkerServiceNewOrderAddedDto(CrawlOrderDto order, string accessToken)
        {
            Order = order;
            AccessToken = accessToken;
            
        }
    }
}
