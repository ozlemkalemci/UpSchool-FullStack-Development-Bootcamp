namespace Application.Common.Models.Email;

public class SendEmailDto
{
    public List<string> EmailAddresses { get; set; }
    public string Content { get; set; }
    public string Subject { get; set; }
    public byte[] AttachmentData { get; set; }
    public string AttachmentFileName { get; set; }


    public SendEmailDto(List<string> emailAddresses, string content, string subject, byte[] attachment)
    {
        EmailAddresses = emailAddresses;

        Content = content;

        Subject = subject;

        AttachmentData = attachment;
    }

    public SendEmailDto(string emailAddress, string content, string subject, byte[] attachment, string attachmentFileName)
    {
        EmailAddresses = new List<string>() { emailAddress };

        Content = content;

        Subject = subject;

        AttachmentData = attachment;

        AttachmentFileName = attachmentFileName;
    }

    public SendEmailDto(string emailAddress, string content, string subject)
    {
        EmailAddresses = new List<string>() { emailAddress };

        Content = content;

        Subject = subject;

    }
}