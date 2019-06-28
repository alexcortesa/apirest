using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Security.Tokens;
using System.Security.Claims;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Web.Script.Serialization;

namespace SICAI.Security
{
    public class TokenManager
    {
        public static string CreateJwtToken(string userName)
        {
            var claimList = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, userName)
                };

            var tokenHandler = new JwtSecurityTokenHandler() { RequireExpirationTime = true };
            var sSKey = new InMemorySymmetricSecurityKey(SecurityConstants.KeyForHmacSha256);

            var jwtToken = tokenHandler.CreateToken(makeSecurityTokenDescriptor(sSKey, claimList));
            return tokenHandler.WriteToken(jwtToken);
        }

        private static SecurityTokenDescriptor makeSecurityTokenDescriptor(InMemorySymmetricSecurityKey sSKey, List<Claim> claimList)
        {
            var now = DateTime.UtcNow;
            Claim[] claims = claimList.ToArray();
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                TokenIssuerName = SecurityConstants.TokenIssuer,
                AppliesToAddress = SecurityConstants.TokenAudience,
                Lifetime = new Lifetime(now, now.AddMinutes(SecurityConstants.TokenLifetimeMinutes)),
                SigningCredentials = new SigningCredentials(sSKey,
                    "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256",
                    "http://www.w3.org/2001/04/xmlenc#sha256"),
            };
        }

        public static ClaimsPrincipal ValidateJwtToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler() { RequireExpirationTime = true };

            // Parse JWT from the Base64UrlEncoded wire form (<Base64UrlEncoded header>.<Base64UrlEncoded body>.<signature>)
            JwtSecurityToken parsedJwt = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

            TokenValidationParameters validationParams =
                new TokenValidationParameters()
                {
                    AllowedAudience = SecurityConstants.TokenAudience,
                    ValidIssuer = SecurityConstants.TokenIssuer,
                    ValidateIssuer = true,
                    SigningToken = new BinarySecretSecurityToken(SecurityConstants.KeyForHmacSha256),
                };

            ClaimsPrincipal obj = tokenHandler.ValidateToken(parsedJwt, validationParams);
            return obj;
        }
    }
}
