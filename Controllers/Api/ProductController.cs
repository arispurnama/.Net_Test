using Application.Contract.Pagination;
using Application.Contract.Request;
using Application.Contract.Responses;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.Api
{
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Post([FromBody] ProductRequest request)
        {
            try
            {
                var result = await _productService.AddAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var response = new PaginationResponse<ProductResponse>();
            try
            {
                var result = await _productService.GetAllAsync();
                response.Data = result;
                response.Total = result.Count;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            return Ok(response);
        }
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> Delete(int? Id)
        {
            try
            {
                var result = await _productService.DeleteAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPatch]
        [Route("")]
        public async Task<IActionResult> Update(int? Id, [FromBody] ProductRequest request)
        {
            try
            {
                var result = await _productService.UpdateAsync(Id, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var response = new SingleResponse<ProductResponse>();
            try
            {
                var result = await _productService.GetByIdAsync(Id);
                response.Data = result;
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            return Ok(response);
        }
    }
}
