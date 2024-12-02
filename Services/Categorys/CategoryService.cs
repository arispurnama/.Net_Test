using Application.Context;
using Application.Contract.Request;
using Application.Contract.Responses;
using Application.Controllers;
using Application.Models;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Categorys
{
    
    public class CategoryService : ICategorieService
    {
        public readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryResponse> AddAsync(CategoryRequest request)
        {
            var req = new Category()
            {
                Name = request.Name,
            };
            _context.UpdateRange(req);
            await _context.SaveChangesAsync();
            return new CategoryResponse
            {
                Name = req.Name,
            };
        }

        public async Task<CategoryResponse> DeleteAsync(int Id)
        {
            var res = await _context.Set<Category>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == int.Parse(Id.ToString()));
            await _context.Set<Category>()
                .AsNoTracking()
                .Where(x => x.Id == Id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return new CategoryResponse
            {
                Id = res.Id,
                Name = res.Name
            };
        }

        public async Task<List<CategoryResponse>> GetAllAsync()
        {
            var result = _context.Set<Category>().AsNoTracking()
                .Select(x => new CategoryResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                });
            return await result.ToListAsync();
        }

        public async Task<CategoryResponse> GetByIdAsync(int Id)
        {
            var res = await _context.Set<Category>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == int.Parse(Id.ToString()));

            return new CategoryResponse
            {
                Id = res.Id,
                Name = res.Name
            };
        }

        public async Task<CategoryResponse> UpdateAsync(int Id, CategoryRequest request)
        {
            await _context.Set<Category>()
                .AsNoTracking()
                .Where(x => x.Id == Id)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(y => y.Name, request.Name));
            await _context.SaveChangesAsync();
            var res = await _context.Set<Category>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Id);
            return new CategoryResponse
            {
                Id = res.Id,
                Name = res.Name
            };
        }
    }
}
