﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.ViewModel.DeliveryService
{
    public  class DeliveryServiceFilterViewModel
    {
        
        public string SearchText { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
