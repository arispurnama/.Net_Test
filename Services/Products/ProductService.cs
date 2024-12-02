using Application.Context;
using Application.Contract.Request;
using Application.Contract.Responses;
using Application.Models;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Products
{
    public class ProductService : IProductService
    {
        public readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ProductResponse> AddAsync(ProductRequest request)
        {
            var req = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId,
            };
            _context.UpdateRange(req);
            await _context.SaveChangesAsync();
            return new ProductResponse
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId,
            };
        }

        public async Task<ProductResponse> DeleteAsync(int? Id)
        {
            var res = await _context.Set<Product>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == int.Parse(Id.ToString()));
            await _context.Set<Product>()
                .AsNoTracking()
                .Where(x => x.Id == Id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return new ProductResponse
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description,
                Price = res.Price,
                CategoryId = res.CategoryId,
            };
        }

        public async Task<List<ProductResponse>> GetAllAsync()
        {
            var result = _context.Set<Product>()
                .AsNoTracking()
                .Include(x => x.Category)
                .Select(x => new ProductResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                });
            return await result.ToListAsync();
        }

        public async Task<ProductResponse> GetByIdAsync(int Id)
        {
            var res = await _context.Set<Product>()
                .AsNoTracking()
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == int.Parse(Id.ToString()));
            
            return new ProductResponse
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description,
                Price = res.Price,
                CategoryId = res.CategoryId,
                CategoryName = res.Category.Name,
            };
        }

        public async Task<ProductResponse> UpdateAsync(int? Id, ProductRequest request)
        {
            await _context.Set<Product>()
                .AsNoTracking()
                .Where(x => x.Id == Id)
                .ExecuteUpdateAsync(x => 
                    x.SetProperty(y => y.Name, request.Name)
                    .SetProperty(y => y.Description, request.Description)
                    .SetProperty(y => y.Price, request.Price)
                    .SetProperty(y => y.CategoryId, request.CategoryId));
            await _context.SaveChangesAsync();
            var res = await _context.Set<Product>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Id);
            return new ProductResponse
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description,
                Price = res.Price,
                CategoryId = res.CategoryId,
            };
        }
    }
}
