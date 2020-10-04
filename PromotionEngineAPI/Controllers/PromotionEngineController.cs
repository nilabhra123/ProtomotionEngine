using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PromotionEngineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromotionEngineController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PromotionEngineController> _logger;

        public PromotionEngineController(ILogger<PromotionEngineController> logger)
        {
            _logger = logger;
        }
        
    }
}
