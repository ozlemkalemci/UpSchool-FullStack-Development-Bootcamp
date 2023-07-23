using Application.Common.Interfaces;
using Application.Common.Models.Crawler;
using Application.Common.Models.Email;
using ClosedXML.Excel;
using Infrastructure.Persistence.Contexts;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services
{
    public class EmailManager : IEmailService
    {
        private readonly IExcelService _excelService;
        private readonly ApplicationDbContext _dbContext;
        public EmailManager(IExcelService excelService, ApplicationDbContext dbContext)
        {
            _excelService = excelService;
            _dbContext = dbContext;
        }

        public async Task SendEmailWithAttachmentAsync(CrawlOrderDto crawlOrderDto, byte[] attachmentData, string attachmentFileName)
        {
            var htmlContent = $"<h4> Hello! Your crawl transaction is finished. {DateTime.Now} </h4>";
            var subject = $"Information about to crawl process";

            var sendEmailDto = new SendEmailDto(crawlOrderDto.Email, htmlContent, subject)
            {
                AttachmentData = attachmentData,
                AttachmentFileName = attachmentFileName
            };

            await Send(sendEmailDto);
        }

        private async Task Send(SendEmailDto sendEmailDto)
        {
            MailMessage message = new MailMessage();

            sendEmailDto.EmailAddresses.ForEach(emailAddress => message.To.Add(emailAddress));

            message.From = new MailAddress("");

            message.Subject = sendEmailDto.Subject;

            message.IsBodyHtml = true;

            message.Body = sendEmailDto.Content;

            if (sendEmailDto.AttachmentData != null)
            {
                // Excel dosyasını geçici bir dosyaya kaydet
                string tempFilePath = Path.GetTempFileName();
                string excelFilePath = Path.ChangeExtension(tempFilePath, "xlsx");
                File.WriteAllBytes(excelFilePath, sendEmailDto.AttachmentData);

                // Geçici dosyadan Excel paketini yükle
                using (var workbook = new XLWorkbook(excelFilePath))
                {
                    // Excel dosyasını e-postaya ekle
                    using (var fileStream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read))
                    {
                        var attachment = new Attachment(fileStream, sendEmailDto.AttachmentFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        message.Attachments.Add(attachment);

                        // E-postayı gönder
                        using (var smtpclient = new SmtpClient())
                        {
                            smtpclient.Port = 587;
                            smtpclient.Host = "smtp.gmail.com";
                            smtpclient.EnableSsl = true;
                            smtpclient.UseDefaultCredentials = false;
                            smtpclient.Credentials = new NetworkCredential("", "");
                            smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;

                            await smtpclient.SendMailAsync(message);
                        }
                    }
                }

                // Geçici dosyayı sil
                File.Delete(excelFilePath);
            }
            else
            {
                SmtpClient client = new SmtpClient();

                client.Port = 587; 
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("", "");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                await client.SendMailAsync(message);
            }
        }
    }
}
