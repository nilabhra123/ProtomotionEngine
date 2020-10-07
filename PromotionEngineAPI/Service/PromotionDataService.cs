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
                    var finalPrice = ((group.FirstOrDefault()?.combo.OfferType == OfferType.AMOUNT_DISCOUNT) ? group.Sum(a => a.item.Price) - group.FirstOrDefault()?.combo.DiscountValue : group.Sum(a => a.item.Price) - (group.Sum(a => a.item.Price) * (group.FirstOrDefault()?.combo.DiscountValue / 100)));
                    currentComboOffers.Add(new SKUOfferDetails
                    {

                        SKUName = string.Join(" & ", group.Select(a => a.item.Name)),
                        PriceAfterDiscount = finalPrice,
                        OfferEligibilityQuantity = 1,
                        OfferDescription = $"purchase at {finalPrice}"
                    });
                });

                var currentIndividualOffers = (from item in this._promotionRepo.GetAllSKUData()
                                               join offerRel in this._promotionRepo.GetAllIndividualSKUOfferRelationData()
                                               on item.Id equals offerRel.SKUId
                                               where offerRel.IsActive == true
                                               join offer in this._promotionRepo.GetAllIndividualSKUOfferData()
                                               on offerRel.OfferId equals offer.Id
                                               let finalPrice = (offer.OfferType == OfferType.AMOUNT_DISCOUNT ? item.Price * offer.PurchaseQuantity - offer.DiscountValue : item.Price * offer.PurchaseQuantity - ((item.Price * offer.PurchaseQuantity * offer.DiscountValue) / 100))
                                               select new SKUOfferDetails()
                                               {
                                                   SKUName = item.Name,
                                                   PriceAfterDiscount = finalPrice,
                                                   OfferEligibilityQuantity = offer.PurchaseQuantity,
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

        public FinalCart AddToCart(List<PurchaseDetail> purchaseDetails)
        {
            var currentOffers = this.GetActiveCurrentOffers();
            var skuDetails = new List<SKUCartDetail>();
            purchaseDetails.ForEach(a =>
            {
                skuDetails.Add(this.CalculateCartAmountPerSKU(a, currentOffers));
            });
            return new FinalCart
            {
                SKUCartDetails = skuDetails,
                TotalAmount = Decimal.Floor((decimal)skuDetails.Sum(a => a.AmountToPay))
            };
        }

        public SKUCartDetail CalculateCartAmountPerSKU(PurchaseDetail purchase, List<SKUOfferDetails> currentOffers)
        {
            var skuPurchased = this._promotionRepo.GetAllSKUData().FirstOrDefault(a => a.Id == purchase.SKUId);
            var skuCart = new SKUCartDetail();
            if(currentOffers.Any(a=>a.SKUName == skuPurchased.Name))
            {
                currentOffers = currentOffers.Where(a => a.SKUName == skuPurchased.Name).ToList();
                while(purchase.Quantity > 0)
                {
                    if(currentOffers.Any(a=>a.OfferEligibilityQuantity <= purchase.Quantity))
                    {
                        var bestOffer = currentOffers.Where(a => a.OfferEligibilityQuantity <= purchase.Quantity).OrderBy(a => a.PriceAfterDiscount).FirstOrDefault();
                        skuCart.SKUName = bestOffer.SKUName;
                        skuCart.Quantity += bestOffer.OfferEligibilityQuantity;
                        skuCart.AmountToPay += bestOffer.PriceAfterDiscount;
                    }
                    else
                    {
                        skuCart.SKUName = skuPurchased.Name;
                        skuCart.AmountToPay += skuPurchased.Price * purchase.Quantity;
                        skuCart.Quantity += purchase.Quantity;
                        skuCart.PurchaseNote = $"Puchased {purchase.Quantity} at {skuCart.AmountToPay}";
                        purchase.Quantity = 0;
                    }
                    purchase.Quantity -= skuCart.Quantity;

                }
            }
            else
            {
                skuCart.SKUName = skuPurchased.Name;
                skuCart.AmountToPay = skuPurchased.Price * purchase.Quantity;
                skuCart.Quantity = purchase.Quantity;
                skuCart.PurchaseNote = $"Puchased {purchase.Quantity} at {skuCart.AmountToPay}";
            }

            return skuCart;
        }

        public void AddIndividualOffer(IndividualSKUOffer offer)
        {
            this._promotionRepo.AddIndividualOffer(offer);
        }

        public void AddComboOffer(ComboOffer combo)
        {
            this._promotionRepo.AddComboOffer(combo);
        }

        public void ApplyIndividualOffer(SKUIndividualOfferRelation offerRelation)
        {
            this._promotionRepo.AddOrUpdateInvidialOfferRelation(offerRelation);
        }

        public void ApplyComboOffer(SKUComboRelation comboRelation)
        {
            this._promotionRepo.AddOrUpdateComboOfferRelation(comboRelation);
        }
    }
}
