using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineAPI.Models
{
    public class SKUCartDetail
    {
        public string SKUName { get; set; }
        public int Quantity { get; set; }
        public decimal? AmountToPay { get; set; }
        public string PurchaseNote { get; set; }
    }
}
