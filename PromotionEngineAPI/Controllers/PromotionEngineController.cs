using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionEngineAPI.Models;
using PromotionEngineAPI.Service;

namespace PromotionEngineAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionEngineController : ControllerBase
    {
        private readonly IPromotionDataService _promotionDataService;

        public PromotionEngineController(IPromotionDataService promotionDataService)
        {
            _promotionDataService = promotionDataService;
        }

        [HttpGet]
        [Route("GetCurrentOffers")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public ActionResult GetCurrentOffers()
        {
            try
            {
                var data = this._promotionDataService.GetActiveCurrentOffers();
                return Ok(data);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddToCart")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public ActionResult AddToCart(List<PurchaseDetail> purchaseDetails)
        {
            try
            {
                var data = this._promotionDataService.AddToCart(purchaseDetails);
                return Ok(data);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddIndividualOffer")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public ActionResult AddIndividualOffer(IndividualSKUOffer offer)
        {
            try
            {
                this._promotionDataService.AddIndividualOffer(offer);
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddComboOffer")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public ActionResult AddComboOffer(ComboOffer offer)
        {
            try
            {
                this._promotionDataService.AddComboOffer(offer);
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ApplyIndividualOffer")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public ActionResult ApplyIndividualOffer(SKUIndividualOfferRelation offerRelation)
        {
            try
            {
                this._promotionDataService.ApplyIndividualOffer(offerRelation);
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ApplyComboOffer")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public ActionResult ApplyComboOffer(SKUComboRelation offerRelation)
        {
            try
            {
                this._promotionDataService.ApplyComboOffer(offerRelation);
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
