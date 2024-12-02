using Application.Contract.Request;
using Application.Contract.Responses;

namespace Application.Services
{
    public interface IProductService
    {
        Task<ProductResponse> AddAsync(ProductRequest request);
        Task<List<ProductResponse>> GetAllAsync();
        Task<ProductResponse> GetByIdAsync(int Id);
        Task<ProductResponse> DeleteAsync(int? Id);
        Task<ProductResponse> UpdateAsync(int? Id, ProductRequest request);
    }
}
