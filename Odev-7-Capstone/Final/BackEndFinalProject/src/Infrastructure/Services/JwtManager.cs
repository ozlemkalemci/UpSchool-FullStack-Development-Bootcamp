// JWT, kullanıcıları kimlik doğrulamak ve yetkilendirmek için kullanılan bir yöntemdir.

// İhtiyaç duyduğumuz bazı araçları ve ayarları içe aktarıyoruz.
using Application.Common.Interfaces; // Uygulama tarafından tanımlanmış arayüzleri içe aktarıyoruz.
using Application.Common.Models.Auth; // Uygulama tarafından tanımlanmış kimlik doğrulama modellerini içe aktarıyoruz.
using Domain.Settings; // Uygulama tarafından tanımlanmış yapılandırma ayarlarını içe aktarıyoruz.
using Microsoft.Extensions.Options; // Microsoft.Extensions.Options kütüphanesini içe aktarıyoruz.
using Microsoft.IdentityModel.Tokens; // Microsoft.IdentityModel.Tokens kütüphanesini içe aktarıyoruz.
using System.IdentityModel.Tokens.Jwt; // System.IdentityModel.Tokens.Jwt kütüphanesini içe aktarıyoruz.
using System.Security.Claims; // System.Security.Claims kütüphanesini içe aktarıyoruz.
using System.Text; // System.Text kütüphanesini içe aktarıyoruz.

// Bu kod, JwtManager adlı bir sınıf oluşturur.
// Bu sınıf, IJwtService arayüzünü uygular.
namespace Infrastructure.Services
{
    public class JwtManager : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        // JwtManager sınıfının yapıcı yöntemi, JwtSettings nesnesini alır.
        // JwtSettings nesnesi, JWT ayarlarını içeren yapılandırma değerlerini temsil eder.
        public JwtManager(IOptions<JwtSettings> jwtSettingsOption)
        {
            _jwtSettings = jwtSettingsOption.Value;
        }

        // Generate yöntemi, JWT oluşturmak için kullanılır.
        // Bu yöntem, kullanıcı kimlik bilgilerini alır ve onları JWT içindeki taleplere dönüştürür.
        // Ayrıca JWT'nin geçerlilik süresini ayarlar ve imzalar.
        public JwtDto Generate(string userId, string email, string firstName, string lastName, List<string>? roles = null)
        {
            // Talepleri temsil eden bir liste oluşturuyoruz.
            var claims = new List<Claim>()
            {
                new Claim("uid", userId), // Kullanıcı kimliği (uid) talebi
                new Claim(JwtRegisteredClaimNames.Email,email), // E-posta talebi
                new Claim(JwtRegisteredClaimNames.Sub, userId), // Alt kimlik (sub) talebi
                new Claim(JwtRegisteredClaimNames.GivenName,firstName), // İsim talebi
                new Claim(JwtRegisteredClaimNames.FamilyName,lastName), // Soyisim talebi
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()) // JWT kimlik doğrulama numarası talebi
            };

            // Simetrik bir güvenlik anahtarı oluşturuyoruz.
            // Bu anahtar, JWT'nin imzalanmasında kullanılır.
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            // İmza doğrulaması için SigningCredentials nesnesi oluşturuyoruz.
            var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);

            // JWT'nin son kullanma tarihini belirliyoruz.
            var expiry = DateTime.Now.AddMinutes(_jwtSettings.ExpiryInMinutes);

            // JwtSecurityToken sınıfını kullanarak JWT'yi oluşturuyoruz.
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer, // JWT'nin verenini belirtir
                audience: _jwtSettings.Audience, // JWT'nin alıcısını belirtir
                claims: claims, // JWT içindeki talepleri belirtir
                expires: expiry, // JWT'nin geçerlilik süresini belirtir
                signingCredentials: signingCredentials // İmza doğrulamasını belirtir
            );

            // JwtSecurityTokenHandler sınıfını kullanarak JWT'yi metne dönüştürüyoruz.
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            // JwtDto nesnesini oluşturarak oluşturulan JWT'yi ve geçerlilik süresini döndürüyoruz.
            return new JwtDto(accessToken, expiry);
        }
    }
}
