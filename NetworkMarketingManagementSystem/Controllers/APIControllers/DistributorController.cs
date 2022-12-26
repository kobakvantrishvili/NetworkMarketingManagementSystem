using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetworkMarketingManagementSystem.Application.Abstraction;
using NetworkMarketingManagementSystem.Application.Models;
using NetworkMarketingManagementSystem.Models.DTOs;
using Mapster;
using NetworkMarketingManagementSystem.Application.Models.Enums;
using NetworkMarketingManagementSystem.Models.Requests.ForDistributor;

namespace NetworkMarketingManagementSystem.Controllers.APIControllers
{
    [Route("Distributors", Name = "Distributor")]
    public class DistributorController : ControllerBase
    {
        private readonly IDistributorService _distributorService;

        public DistributorController(IDistributorService distributorService)
        {
            _distributorService = distributorService;
        }

        /// <summary>
        /// Distributor Create Action Method
        /// </summary>
        /// <param name="distributor"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Create
        ///     {
        ///     "firstName": "Koba",
        ///     "lastName": "Kvantrishvili",
        ///     "birthday": "2001-11-30T10:24:37.425Z",
        ///     "sex": true,
        ///     "image": "",
        ///     "referredBy": 0,
        ///     "identityDocument": {
        ///         "type": 1,
        ///         "series": "1234567890",
        ///         "number": "0123456789",
        ///         "releaseDate": "2022-08-15T10:24:37.425Z",
        ///         "validUntil": "2026-08-15T10:24:37.425Z",
        ///         "personalNumber": "01001070707",
        ///         "issuingAgency": "Justice Hall"
        ///         },
        ///     "contactInfo": {
        ///         "type": 1,
        ///         "contact": "568130272"
        ///         },
        ///     "addressInfo": {
        ///         "type": 1,
        ///         "address": "abcd street"
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created a new distributor data entry</response>
        /// <response code="400">Distributor passed is null</response>
        /// <response code="403">Distributor does not fit the hierarchy</response>
        /// <response code="409">Distributor with same PersonalNumber already exists</response>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] DistributorCreateRequest distributor)
        {
            var (status, id) = await _distributorService.CreateDistributorAsync(distributor.Adapt<DistributorServiceModel>());

            return StatusCode((int)status, id);
        }

        [HttpGet("Read/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Read(int Id)
        {
            var (status, distributor) = await _distributorService.ReadDistributorAsync(Id);

            return StatusCode((int)status, distributor?.Adapt<DistributorDTO>());
        }

        [HttpGet("Read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadAll()
        {
            var (status, distributors) = await _distributorService.ReadAllDistributorAsync();

            return StatusCode((int)status, distributors?.Adapt<List<DistributorDTO>>());
        }

        /// <summary>
        /// Distributor Update Action Method
        /// </summary>
        /// <param name="distributor"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Update
        ///     {
        ///     "Id": 1,
        ///     "firstName": "Boba",
        ///     "lastName": "Kvantrishvili",
        ///     "birthday": "2001-11-30T10:24:37.425Z",
        ///     "sex": true,
        ///     "image": "",
        ///     "referredBy": 2,
        ///     "identityDocument": {
        ///         "type": 1,
        ///         "series": "1234567890",
        ///         "number": "9123456789",
        ///         "releaseDate": "2022-08-15T10:24:37.425Z",
        ///         "validUntil": "2026-08-15T10:24:37.425Z",
        ///         "personalNumber": "01001077777",
        ///         "issuingAgency": "Justice Hall"
        ///         },
        ///     "contactInfo": {
        ///         "type": 1,
        ///         "contact": "568135275"
        ///         },
        ///     "addressInfo": {
        ///         "type": 1,
        ///         "address": "abc street"
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Succesfully updated Distributor</response>
        /// <response code="400">Distributor passed is null, Distributor's recommender does not exist or there is a circular reference</response>
        /// <response code="403">Distributor does not fit the hierarchy</response>
        /// <response code="404">Distributor not found</response>
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] DistributorUpdateRequest distributor)
        {
            var status = await _distributorService.UpdateDistributorAsync(distributor.Adapt<DistributorServiceModel>());

            return StatusCode((int)status);
        }

        [HttpDelete("Delete/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int Id)
        {
            var status = await _distributorService.DeleteDistributorAsync(Id);

            return StatusCode((int)status);
        }
    }
}
