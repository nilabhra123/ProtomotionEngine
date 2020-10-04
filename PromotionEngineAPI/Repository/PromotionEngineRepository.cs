using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using PromotionEngineAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineAPI.Repository
{
    public class PromotionEngineRepository : IPromotionEngineRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string jsonFilePath;
        private PromotionEngineMaster _masterData;

        public PromotionEngineRepository(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
            this.jsonFilePath = Path.Combine(this._webHostEnvironment.ContentRootPath, "PromotionEngineData.json");
            this._masterData = this.GetPromotionEngineMasterData();
            
        }

        public PromotionEngineMaster GetPromotionEngineMasterData()
        {
            try
            {
                var json = File.ReadAllText(this.jsonFilePath);
                if (json != null)
                    return JsonConvert.DeserializeObject<PromotionEngineMaster>(json);
                else
                    return new PromotionEngineMaster();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<SKU> GetAllSKUData()
        {
            return this._masterData.PromotionMaster.SKUProducts;
        }

        public List<IndividualSKUOffer> GetAllIndividualSKUOfferData()
        {
            return this._masterData.PromotionMaster.IndividualOffers;
        }

        public List<ComboOffer> GetComboOffers()
        {
            return this._masterData.PromotionMaster.ComboOffers;
        }

        public List<SKUIndividualOfferRelation> GetAllIndividualSKUOfferRelationData()
        {
            return this._masterData.Relation.SKUIndividualOfferRelations;
        }

        public List<SKUComboRelation> GetAllSKUComboOfferRelationData()
        {
            return this._masterData.Relation.SKUComboOfferRelations;
        }

        public void AddSKUData(SKU sku)
        {
            try
            {
                var currentSKUData = this.GetAllSKUData().OrderBy(a => a.Id).ToList();
                var newId = currentSKUData.LastOrDefault().Id + 1;
                sku.Id = newId;
                currentSKUData.Add(sku);
                this._masterData.PromotionMaster.SKUProducts = currentSKUData;
                string newJsonResult = JsonConvert.SerializeObject(this._masterData, Formatting.Indented);
                File.WriteAllText(this.jsonFilePath, newJsonResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateSKUData(SKU sku)
        {
            try
            {
                var currentSKUData = this.GetAllSKUData().OrderBy(a => a.Id).ToList();
                var index = currentSKUData.FindIndex(a => a.Id == sku.Id);
                if (index > 0)
                    currentSKUData[index] = sku;
                else
                    throw new IndexOutOfRangeException("Invalid item: SKU is not found in the system");

                this._masterData.PromotionMaster.SKUProducts = currentSKUData;
                string newJsonResult = JsonConvert.SerializeObject(this._masterData, Formatting.Indented);
                File.WriteAllText(this.jsonFilePath, newJsonResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddIndividualOffer(IndividualSKUOffer offer)
        {
            try
            {
                var currentOffers = this.GetAllIndividualSKUOfferData().OrderBy(a => a.Id).ToList();
                offer.Id = currentOffers.LastOrDefault().Id + 1;
                currentOffers.Add(offer);
                this._masterData.PromotionMaster.IndividualOffers = currentOffers;
                string newJsonResult = JsonConvert.SerializeObject(this._masterData, Formatting.Indented);
                File.WriteAllText(this.jsonFilePath, newJsonResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateIndividualOffer(IndividualSKUOffer offer)
        {
            try
            {
                var currentOfferData = this.GetAllIndividualSKUOfferData().OrderBy(a => a.Id).ToList();
                var index = currentOfferData.FindIndex(a => a.Id == offer.Id);
                if (index > 0)
                    currentOfferData[index] = offer;
                else
                    throw new IndexOutOfRangeException("Invalid item: Individual Offer is not found in the system");

                this._masterData.PromotionMaster.IndividualOffers = currentOfferData;
                string newJsonResult = JsonConvert.SerializeObject(this._masterData, Formatting.Indented);
                File.WriteAllText(this.jsonFilePath, newJsonResult);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddComboOffer(ComboOffer offer)
        {
            try
            {
                var currentOffers = this.GetComboOffers().OrderBy(a => a.Id).ToList();
                offer.Id = currentOffers.LastOrDefault().Id + 1;
                currentOffers.Add(offer);
                this._masterData.PromotionMaster.ComboOffers = currentOffers;
                string newJsonResult = JsonConvert.SerializeObject(this._masterData, Formatting.Indented);
                File.WriteAllText(this.jsonFilePath, newJsonResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateComboOffer(ComboOffer offer)
        {
            try
            {
                var currentOfferData = this.GetComboOffers().OrderBy(a => a.Id).ToList();
                var index = currentOfferData.FindIndex(a => a.Id == offer.Id);
                if (index > 0)
                    currentOfferData[index] = offer;
                else
                    throw new IndexOutOfRangeException("Invalid item: Combo Offer is not found in the system");

                this._masterData.PromotionMaster.ComboOffers = currentOfferData;
                string newJsonResult = JsonConvert.SerializeObject(this._masterData, Formatting.Indented);
                File.WriteAllText(this.jsonFilePath, newJsonResult);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddOrUpdateInvidialOfferRelation(SKUIndividualOfferRelation offerRelation)
        {
            try
            {
                var allRelations = this.GetAllIndividualSKUOfferRelationData();
                var existingRelation = allRelations
                    .FirstOrDefault(a => a.SKUId == offerRelation.SKUId && a.OfferId == offerRelation.OfferId); 
                if(existingRelation != null)
                {
                    
                    var index = allRelations.IndexOf(existingRelation);
                    if (index > 0)
                        allRelations[index] = existingRelation;
                    else
                        throw new IndexOutOfRangeException("Invalid item: The SKU Offer relation is not found in the system");
                }
                else
                {
                    allRelations.Add(offerRelation);
                }
                this._masterData.Relation.SKUIndividualOfferRelations = allRelations;
                string newJsonResult = JsonConvert.SerializeObject(this._masterData, Formatting.Indented);
                File.WriteAllText(this.jsonFilePath, newJsonResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddOrUpdateComboOfferRelation(SKUComboRelation offerRelation)
        {
            try
            {
                var allRelations = this.GetAllSKUComboOfferRelationData();
                var existingRelation = allRelations
                    .FirstOrDefault(a => a.SKUId == offerRelation.SKUId && a.ComboOfferId == offerRelation.ComboOfferId);
                if (existingRelation != null)
                {

                    var index = allRelations.IndexOf(existingRelation);
                    if (index > 0)
                        allRelations[index] = existingRelation;
                    else
                        throw new IndexOutOfRangeException("Invalid item: The SKU Offer relation is not found in the system");
                }
                else
                {
                    allRelations.Add(offerRelation);
                }
                this._masterData.Relation.SKUComboOfferRelations = allRelations;
                string newJsonResult = JsonConvert.SerializeObject(this._masterData, Formatting.Indented);
                File.WriteAllText(this.jsonFilePath, newJsonResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
