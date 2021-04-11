using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace NewsApi.Models.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";
        private const string KEY = "mu_secret_key!123";
        public const int LIFETIME = 1; // 1 minute
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

    }
}
