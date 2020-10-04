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
    }
}
