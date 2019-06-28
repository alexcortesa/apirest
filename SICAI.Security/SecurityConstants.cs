using System.Security.Cryptography;
using System.Configuration;

namespace SICAI.Security
{
    public class SecurityConstants
    {
        public static readonly byte[] KeyForHmacSha256 = new byte[64];

        public static readonly string TokenIssuer = string.Empty;

        public static readonly string TokenAudience = string.Empty;

        public static readonly double TokenLifetimeMinutes = 300;

        static SecurityConstants()
        {
            RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider();
            cryptoProvider.GetNonZeroBytes(KeyForHmacSha256);

            TokenIssuer = "issuer";

            TokenAudience = ConfigurationSettings.AppSettings["JWT_AUDIENCE_TOKEN"].ToString(); 
        }
    }
}
