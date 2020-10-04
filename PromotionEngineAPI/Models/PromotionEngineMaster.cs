using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineAPI.Models
{
    public class PromotionEngineMaster
    {
        public PromotionMaster PromotionMaster { get; set; }
        public Relation Relation { get; set; }
    }

    public class PromotionMaster
    {
        public List<SKU> SKUProducts { get; set; }
        public List<IndividualSKUOffer> IndividualOffers { get; set; }
        public List<ComboOffer> ComboOffers { get; set; }
    }

    public class Relation
    {
        public List<SKUIndividualOfferRelation> SKUIndividualOfferRelations { get; set; }
        public List<SKUComboRelation> SKUComboOfferRelations { get; set; }
    }
}
