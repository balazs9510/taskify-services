using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskify.DAL.Models
{
    public class UserRefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}
