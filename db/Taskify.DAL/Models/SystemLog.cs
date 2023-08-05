using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskify.DAL.Models
{
    public class SystemLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public LogLevel Level { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; set; }
        public string Message { get; set; }
        public string? Caller { get; set; }
        public string? UserName { get; set; }
    }
}
