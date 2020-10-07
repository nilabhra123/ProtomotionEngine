using PromotionEngineAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineAPI.Service
{
    public interface IPromotionDataService
    {
        public List<SKUOfferDetails> GetActiveCurrentOffers();
        public FinalCart AddToCart(List<PurchaseDetail> purchaseDetails);
        public void AddIndividualOffer(IndividualSKUOffer offer);
        public void AddComboOffer(ComboOffer combo);
        public void ApplyIndividualOffer(SKUIndividualOfferRelation offerRelation);
        public void ApplyComboOffer(SKUComboRelation comboRelation);
    }
}
