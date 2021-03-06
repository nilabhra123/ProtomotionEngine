﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineAPI.Models
{
    public class SKUOfferDetails
    {
        public string SKUName { get; set; }
        public string OfferDescription { get; set; }
        public int OfferEligibilityQuantity { get; set; }
        public decimal? PriceAfterDiscount { get; set; }
    }
}
