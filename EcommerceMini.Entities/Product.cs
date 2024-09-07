using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceMini.Entities
{
    [Table("Products")]
    public class Product: BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string Sku { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(400)]
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsActive { get; set; }
    }
}
