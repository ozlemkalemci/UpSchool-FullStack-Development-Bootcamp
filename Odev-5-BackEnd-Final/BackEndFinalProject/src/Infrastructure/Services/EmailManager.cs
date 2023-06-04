using Application.Common.Interfaces;
using Application.Models.Product;
using Application.Models.Email;
using ClosedXML.Excel;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
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

		public async Task SendEmailWithAttachmentAsync(SendEmailConfirmationDto sendEmailConfirmationDto)
		{
			var htmlContent = $"<h4> Hello! Your crawl transaction is finished.</h4>";
			var subject = $"Information about to crawl process";


			var productResult = await _dbContext.Products.ToListAsync();

			var productDto = productResult.Select(product => new ProductDto
			{
				Id = Guid.NewGuid(),
				Name = product.Name,
				Price = product.Price,
				SalePrice = product.SalePrice,
				Picture = product.Picture,
				IsOnSale = product.IsOnSale,
				CreatedOn = DateTimeOffset.Now,
			}).ToList();

			byte[] attachment = await _excelService.GenerateExcelFileAsync(productDto);


			Send(new SendEmailDto(sendEmailConfirmationDto.Email, htmlContent, subject, attachment));
		}

		private void Send(SendEmailDto sendEmailDto)
		{
			MailMessage message = new MailMessage();

			sendEmailDto.EmailAddresses.ForEach(emailAddress => message.To.Add(emailAddress));

			message.From = new MailAddress("noreply@entegraturk.com");

			message.Subject = sendEmailDto.Subject;

			message.IsBodyHtml = true;

			message.Body = sendEmailDto.Content;

			if (sendEmailDto.Attachment != null)
			{
				// Excel dosyasını geçici bir dosyaya kaydet
				string tempFilePath = Path.GetTempFileName();
				string excelFilePath = Path.ChangeExtension(tempFilePath, "xlsx");
				File.WriteAllBytes(excelFilePath, sendEmailDto.Attachment);

				// Geçici dosyadan Excel paketini yükle
				using (var workbook = new XLWorkbook(excelFilePath))
				{
					// Excel dosyasını e-postaya ekle
					using (var fileStream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read))
					{
						var attachment = new Attachment(fileStream, "ExcelFileName.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
						message.Attachments.Add(attachment);

						// E-postayı gönder
						using (var smtpclient = new SmtpClient())
						{
							smtpclient.Port = 587;
							smtpclient.Host = "mail.entegraturk.com";
							smtpclient.EnableSsl = false;
							smtpclient.UseDefaultCredentials = false;
							smtpclient.Credentials = new NetworkCredential("noreply@entegraturk.com", "xzx2xg4Jttrbzm5nIJ2kj1pE4l");
							smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;

							smtpclient.Send(message);
						}
					}
				}

				// Geçici dosyayı sil
				File.Delete(excelFilePath);
			}

			else {
				SmtpClient client = new SmtpClient();


				client.Port = 587;

				client.Host = "mail.entegraturk.com";

				client.EnableSsl = false;

				client.UseDefaultCredentials = false;

				client.Credentials = new NetworkCredential("noreply@entegraturk.com", "xzx2xg4Jttrbzm5nIJ2kj1pE4l");

				client.DeliveryMethod = SmtpDeliveryMethod.Network;

				client.Send(message);
			}

			
		}

	}
}
