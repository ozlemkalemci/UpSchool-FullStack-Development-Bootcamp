namespace Domain.Settings
{
    public class JwtSettings
    {
        // JWT imzalama ve doğrulama için kullanılacak gizli anahtar
        public string SecretKey { get; set; }

        // Token oluşturucusunun adı
        public string Issuer { get; set; }

        // Token'i kullanacak hedef alıcı
        public string Audience { get; set; }

        // Token'in geçerlilik süresi (dakika cinsinden)
        public int ExpiryInMinutes { get; set; }
    }
}
