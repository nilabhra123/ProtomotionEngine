using PromotionEngineAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineAPI.Repository
{
    public interface IPromotionEngineRepository
    {
        public PromotionEngineMaster GetPromotionEngineMasterData();
        public List<SKU> GetAllSKUData();
        public List<IndividualSKUOffer> GetAllIndividualSKUOfferData();
        public List<ComboOffer> GetComboOffers();
        public List<SKUIndividualOfferRelation> GetAllIndividualSKUOfferRelationData();
        public List<SKUComboRelation> GetAllSKUComboOfferRelationData();
        public void AddSKUData(SKU sku);
        public void UpdateSKUData(SKU sku);
        public void AddIndividualOffer(IndividualSKUOffer offer);
        public void UpdateIndividualOffer(IndividualSKUOffer offer);
        public void AddComboOffer(ComboOffer offer);
        public void UpdateComboOffer(ComboOffer offer);
        public void AddOrUpdateInvidialOfferRelation(SKUIndividualOfferRelation offerRelation);
        public void AddOrUpdateComboOfferRelation(SKUComboRelation offerRelation);
    }
}
