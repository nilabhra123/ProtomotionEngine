using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineAPI.Models
{
    public class FinalCart
    {
        public List<SKUCartDetail> SKUCartDetails { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
