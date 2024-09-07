using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceMini.Application.Dto
{
    public class LoginDto
    {
        [MaxLength(100)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [MaxLength(20)]
        [Required]
        public string Password { get; set; }
    }
}
