using System.Text;
using TaskifyAuthService.Web;

namespace TaskifyAuthService.Web.Utils
{
    public static class ConfigurationExtensions
    {
        public static DateTime AuthTokenExpiration(this IConfiguration config)
        {
            return DateTime.Now.AddMinutes(config.AuthTokenExpirationMinutes());
        }

        public static int AuthTokenExpirationMinutes(this IConfiguration config)
        {
            return int.Parse(config.GetConfigWithKey(ConfigKeys.JwtExpiresMinute));
        }

        public static string GetJwtIssuer(this IConfiguration config)
        {
            return config.GetConfigWithKey(ConfigKeys.JwtIssuer);
        }

        public static string GetJwtAudience(this IConfiguration config)
        {
            return config.GetConfigWithKey(ConfigKeys.JwtAudience);
        }

        public static byte[] GetJwtKey(this IConfiguration config)
        {
            return Encoding.UTF8.GetBytes(config.GetConfigWithKey(ConfigKeys.JwtKey));
        }

        private static string GetConfigWithKey(this IConfiguration config, string key)
        {
            return config[key];
        }

    }
}
