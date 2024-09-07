using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceMini.Application.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
