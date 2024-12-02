using Application.Contract.Pagination;
using Application.Contract.Request;
using Application.Contract.Responses;
using Application.Services;
using Application.Services.Products;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.Api
{
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategorieService _categorieService;
        public CategoryController(ICategorieService categorieService)
        {
            _categorieService = categorieService;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Post([FromBody] CategoryRequest request)
        {
            try
            {
                var result = await _categorieService.AddAsync(request);
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
            var response = new PaginationResponse<CategoryResponse>();
            try
            {
                var result = await _categorieService.GetAllAsync();
                response.Total = result.Count;
                response.Data = result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            return Ok(response);
        }
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var result = await _categorieService.DeleteAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPatch]
        [Route("")]
        public async Task<IActionResult> Update(int Id, [FromBody] CategoryRequest request)
        {
            try
            {
                var result = await _categorieService.UpdateAsync(Id, request);
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
            var response = new SingleResponse<CategoryResponse>();
            try
            {
                var result = await _categorieService.GetByIdAsync(Id);
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
