using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductCategoryService : BaseDataService, IProductCategoryService
    {
        public ProductCategoryService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
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

        public async Task<ProductCategoryDTO> CreateProductCategoryAsync(ProductCategoryDTO dto)
        {
            var checkName = await _context.Categories.Where(x => x.Name == dto.Name).CountAsync();
            if (checkName > 0)
            {
                throw new NameAlreadyInUseException(dto.Name);
            }
            var entity = new ProductCategory
            {
                Name = dto.Name
            };
            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductCategoryDTO>(entity);
        }

    }
}
