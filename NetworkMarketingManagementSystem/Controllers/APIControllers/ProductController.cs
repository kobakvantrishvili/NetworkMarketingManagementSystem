using Mapster;
using Microsoft.AspNetCore.Mvc;
using NetworkMarketingManagementSystem.Application.Abstraction;
using NetworkMarketingManagementSystem.Application.Implementation;
using NetworkMarketingManagementSystem.Application.Models;
using NetworkMarketingManagementSystem.Models.DTOs;
using NetworkMarketingManagementSystem.Models.Requests.ForProduct;

namespace NetworkMarketingManagementSystem.Controllers.APIControllers
{
    [Route("Products", Name = "Product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Product Create Action Method
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Create
        ///     {
        ///     "code": "11112222",
        ///     "name": "Chips",
        ///     "price": 2.50
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created a new product data entry</response>
        /// <response code="400">Product passed is null</response>
        /// <response code="409">Product already exists</response>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest product)
        {
            var (status, id) = await _productService.CreateProductAsync(product.Adapt<ProductServiceModel>());

            return StatusCode((int)status, id);
        }

        [HttpGet("Read/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Read(int Id)
        {
            var (status, product) = await _productService.ReadProductAsync(Id);

            return StatusCode((int)status, product?.Adapt<ProductDTO>());
        }

        [HttpGet("Read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadAll()
        {
            var (status, products) = await _productService.ReadAllProductAsync();

            return StatusCode((int)status, products?.Adapt<List<ProductDTO>>());
        }

        /// <summary>
        /// Product Update Action Method
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Update
        ///     {
        ///     "id": 1,
        ///     "code": "11113333",
        ///     "name": "Chips",
        ///     "price": 2.80
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Succesfully updated Product</response>
        /// <response code="400">Product passed is null</response>
        /// <response code="404">Product not found</response>
        /// <response code="409">Product code can't match another product's code</response>
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest product)
        {
            var status = await _productService.UpdateProductAsync(product.Adapt<ProductServiceModel>());

            return StatusCode((int)status);
        }

        [HttpDelete("Delete/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int Id)
        {
            var status = await _productService.DeleteProductAsync(Id);

            return StatusCode((int)status);
        }
    }
}
