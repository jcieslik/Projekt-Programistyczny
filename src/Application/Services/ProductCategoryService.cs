using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductCategoryService : BaseDataService, IProductCategoryService
    {
        public ProductCategoryService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ProductCategoryDTO> GetProductCategoryByIdAsync(long id)
        {
            return _mapper.Map<ProductCategoryDTO>(
                await _context.Categories
                .Include(i => i.Offers)
                .Include(i => i.ParentCategory)
                .Include(i => i.ChildrenCategories)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id)
                );
        }
        public async Task<IEnumerable<ProductCategoryDTO>> GetProductCategoriesAsync()
        {
            var categories = _context.Categories
                .Include(i => i.Offers)
                .Include(i => i.ParentCategory)
                .Include(i => i.ChildrenCategories)
                .AsNoTracking();

            return await categories.ProjectTo<ProductCategoryDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ProductCategoryDTO> CreateProductCategoryAsync(CreateProductCategoryDTO dto)
        {
            var checkName = await _context.Categories.Where(x => x.Name == dto.Name).CountAsync();
            if (checkName > 0)
            {
                throw new NameAlreadyInUseException(dto.Name);
            }
            ProductCategory parent = null;
            if (dto.ParentCategoryId.HasValue)
            {
                parent = await _context.Categories.FindAsync(dto.ParentCategoryId.Value);
                if(parent == null)
                {
                    throw new NotFoundException(nameof(ProductCategory), dto.ParentCategoryId.Value);
                }
            }
            var entity = new ProductCategory
            {
                Name = dto.Name,
                ParentCategory = parent
            };
            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductCategoryDTO>(entity);
        }

        public async Task<ProductCategoryDTO> UpdateProductCategoryAsync(UpdateProductCategoryDTO dto)
        {
            var category = await _context.Categories.FindAsync(dto.Id);
            if(category == null)
            {
                throw new NotFoundException(nameof(ProductCategory), dto.Id);
            }
            if (!string.IsNullOrEmpty(dto.Name)) {
                var checkName = await _context.Categories.Where(x => x.Name == dto.Name).CountAsync();
                if (checkName > 0)
                {
                    throw new NameAlreadyInUseException(dto.Name);
                }
                category.Name = dto.Name;
            }
            if (dto.ParentId.HasValue)
            {
                var parent = await _context.Categories.FindAsync(dto.ParentId.Value);
                category.ParentCategory = parent ?? throw new NotFoundException(nameof(ProductCategory), dto.ParentId.Value);
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductCategoryDTO>(category);
        }
    }
}
