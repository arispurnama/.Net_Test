using Application.Contract.Request;
using Application.Contract.Responses;
using Application.Controllers;
using System.IO.Pipelines;

namespace Application.Services
{
    public interface ICategorieService
    {
        Task<CategoryResponse> AddAsync(CategoryRequest request);
        Task<List<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse> DeleteAsync(int Id);
        Task<CategoryResponse> UpdateAsync(int Id, CategoryRequest request);
        Task<CategoryResponse> GetByIdAsync(int Id);
    }
}
