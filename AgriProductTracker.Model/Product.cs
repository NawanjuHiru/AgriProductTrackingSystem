﻿using AgriProductTracker.Model.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Model
{
    public  class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public Availability Availability { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedById { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual OrderItem OrderItem { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }


        public virtual ICollection<ProductImage> ProductImages { get; set; }





    }
}
