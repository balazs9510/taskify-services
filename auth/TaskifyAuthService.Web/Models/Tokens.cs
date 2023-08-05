using System.ComponentModel.DataAnnotations;

namespace TaskifyAuthService.Web.Models
{
    public class Tokens
    {
        [Required]
        public string? AccessToken { get; set; }
        [Required]
        public string? RefreshToken { get; set; }
    }
}
