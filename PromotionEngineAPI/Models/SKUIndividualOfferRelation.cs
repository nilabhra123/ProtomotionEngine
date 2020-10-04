using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineAPI.Models
{
    public class SKUIndividualOfferRelation
    {
        public int SKUId { get; set; }
        public int OfferId { get; set; }
        public bool IsActive { get; set; }
    }
}
