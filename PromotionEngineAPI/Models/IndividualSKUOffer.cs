using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineAPI.Models
{
    public class IndividualSKUOffer
    {
        public int Id { get; set; }
        public string OfferType { get; set; }
        public int PurchaseQuantity { get; set; }
        public decimal DiscountValue { get; set; }
    }   
}
