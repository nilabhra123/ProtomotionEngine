using PromotionEngineAPI.Models;
using PromotionEngineAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineAPI.Service
{
    public class PromotionDataService : IPromotionDataService
    {
        public readonly IPromotionEngineRepository _promotionRepo;

        public PromotionDataService(IPromotionEngineRepository promotionRepo)
        {
            _promotionRepo = promotionRepo;
        }

        public List<SKUOfferDetails> GetActiveCurrentOffers()
        {
            try
            {
                var currentComboOffers = new List<SKUOfferDetails>();
                var allOffers = new List<SKUOfferDetails>();
                var comboOfferGroups = (from item in this._promotionRepo.GetAllSKUData()
                                        join skuCombo in this._promotionRepo.GetAllSKUComboOfferRelationData()
                                        on item.Id equals skuCombo.SKUId
                                        join combo in this._promotionRepo.GetComboOffers()
                                        on skuCombo.ComboOfferId equals combo.Id
                                        where combo.IsActive == true
                                        select new { item, combo });

                comboOfferGroups.GroupBy(a=>a.combo.Id).ToList().ForEach(group =>
                {
                    var finalPrice = ((group.FirstOrDefault()?.combo.OfferType == OfferType.AMOUNT_DISCOUNT) ? group.Sum(a => a.item.price) - group.FirstOrDefault()?.combo.DiscountValue : group.Sum(a => a.item.price) - (group.Sum(a => a.item.price) * (group.FirstOrDefault()?.combo.DiscountValue / 100)));
                    currentComboOffers.Add(new SKUOfferDetails
                    {

                        SKUName = string.Join(" & ", group.Select(a => a.item.Name)),
                        PriceAfterDiscount = finalPrice,
                        OfferDescription = $"purchase at {finalPrice}"
                    });
                });

                var currentIndividualOffers = (from item in this._promotionRepo.GetAllSKUData()
                                               join offerRel in this._promotionRepo.GetAllIndividualSKUOfferRelationData()
                                               on item.Id equals offerRel.SKUId
                                               where offerRel.IsActive == true
                                               join offer in this._promotionRepo.GetAllIndividualSKUOfferData()
                                               on offerRel.OfferId equals offer.Id
                                               let finalPrice = (offer.OfferType == OfferType.AMOUNT_DISCOUNT ? item.price * offer.PurchaseQuantity - offer.DiscountValue : item.price * offer.PurchaseQuantity - ((item.price * offer.PurchaseQuantity * offer.DiscountValue) / 100))
                                               select new SKUOfferDetails()
                                               {
                                                   SKUName = item.Name,
                                                   PriceAfterDiscount = finalPrice,
                                                   OfferDescription = $"Purchase {offer.PurchaseQuantity} {item.Name} at " +
                                                   $"{finalPrice}"
                                               }).ToList();

                allOffers.AddRange(currentIndividualOffers);
                allOffers.AddRange(currentComboOffers);

                return allOffers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
