using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineAPI.Models
{
    public class ComboOffer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OfferType { get; set; }
        public decimal DiscountValue { get; set; }
        public bool IsActive { get; set; }
    }
}
