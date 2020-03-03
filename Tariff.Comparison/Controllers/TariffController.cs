using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tariff.Framework.Services.Interface;

namespace Tariff.Comparison.Controllers
{
    [Route("api/tariff")]
    [ApiController]
    public class TariffController : ControllerBase
    {
        private readonly ITariffService _tariffService;

        public TariffController(ITariffService tariffService)
        {
            _tariffService = tariffService;
        }

        [HttpGet("GetProducts/{usage}")]
        public async Task<IActionResult> GetProducts(string usage)
        {
            if (string.IsNullOrWhiteSpace(usage)) return BadRequest();
            var products = await _tariffService.GetProducts(usage);
            return Ok(products);
        }
    }
}
