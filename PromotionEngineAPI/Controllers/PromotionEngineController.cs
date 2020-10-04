using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        [ProducesResponseType(typeof(BadRequestResult), 500)]
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

    }
}
