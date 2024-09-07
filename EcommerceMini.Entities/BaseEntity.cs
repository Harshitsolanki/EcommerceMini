﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceMini.Entities
{
    public class BaseEntity
    {
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? lastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set;}



    }
}
