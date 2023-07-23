using Application.Common.Models.Crawler;

namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailWithAttachmentAsync(CrawlOrderDto crawlOrderDto, byte[] attachmentData, string attachmentFileName);
    }
}
