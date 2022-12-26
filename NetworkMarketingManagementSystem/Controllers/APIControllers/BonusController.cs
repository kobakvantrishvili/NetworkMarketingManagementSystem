using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using NetworkMarketingManagementSystem.Application.Abstraction;
using NetworkMarketingManagementSystem.Application.Implementation;
using NetworkMarketingManagementSystem.Application.Models;
using NetworkMarketingManagementSystem.Models.DTOs;
using NetworkMarketingManagementSystem.Models.Requests.ForProduct;
using NetworkMarketingManagementSystem.Persistence.MongoDb.Models;
using NetworkMarketingManagementSystem.Persistence.MongoDb.Repositories.Abstraction;

namespace NetworkMarketingManagementSystem.Controllers.APIControllers
{
    [Route("Bonuses", Name = "Bonus")]
    public class BonusController : ControllerBase
    {

        private readonly IBonusService _bonusService;

        public BonusController(IBonusService bonusService)
        {
            _bonusService = bonusService;
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(DateTime startDate, DateTime endDate)
        {
            var status  = await _bonusService.CreateBonusesAsync(startDate, endDate);

            return StatusCode((int)status);
        }

        [HttpGet("Read/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Read(string Id)
        {
            var (status, bonus) = await _bonusService.ReadBonusAsync(Id);

            return StatusCode((int)status, bonus?.Adapt<BonusDTO>());
        }

        [EnableQuery]
        [HttpGet("Read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadAllFiltered(string select, string filter, string orderby)
        {
            var (status, bonuses) = await _bonusService.ReadAllBonusesAsync();

            return StatusCode((int)status, bonuses?.ToList().Adapt<List<BonusDTO>>());
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(DateTime startDate, DateTime endDate)
        {
            var status = await _bonusService.DeleteBonusesAsync(startDate, endDate);

            return StatusCode((int)status);
        }
    }
}
