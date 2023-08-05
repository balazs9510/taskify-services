using System.ComponentModel.DataAnnotations;

namespace TaskifyAuthService.Web.Models
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
