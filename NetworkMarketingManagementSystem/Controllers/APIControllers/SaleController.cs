using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using NetworkMarketingManagementSystem.Application.Abstraction;
using NetworkMarketingManagementSystem.Application.Implementation;
using NetworkMarketingManagementSystem.Application.Models;
using NetworkMarketingManagementSystem.Models.DTOs;
using NetworkMarketingManagementSystem.Models.Requests.ForSale;

namespace NetworkMarketingManagementSystem.Controllers.APIControllers
{
    [Route("Sales", Name = "Sale")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        /// <summary>
        /// Sale Create Action Method
        /// </summary>
        /// <param name="sale"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Create
        ///     {
        ///     "distributorId": 1,
        ///     "saleDate": "2022-12-24T22:06:27.435Z",
        ///     "soldProductsInfo": [
        ///         {
        ///         "productId": 1,
        ///         "quantity": 4
        ///         },
        ///         {
        ///         "productId": 3,
        ///         "quantity": 2
        ///         }
        ///       ]
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created a new sale data entry</response>
        /// <response code="400">Sale passed is null or Distributor or Product doesn't exists</response>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] SaleCreateRequest sale)
        {
            var (status, id) = await _saleService.CreateSaleAsync(sale.Adapt<SaleServiceModel>());

            return StatusCode((int)status, id);
        }

        [HttpGet("Read/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Read(int Id)
        {
            var (status, sale) = await _saleService.ReadSaleAsync(Id);

            return StatusCode((int)status, sale?.Adapt<SaleDTO>());
        }

        [EnableQuery]
        [HttpGet("Read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadAllFiltered(string select, string filter, string orderby)
        {
            var (status, sales) = await _saleService.ReadAllSaleAsync();

            return StatusCode((int)status, sales?.ToList().Adapt<List<SaleDTO>>());
        }
    }
}
