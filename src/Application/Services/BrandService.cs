using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Update;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BrandService : BaseDataService, IBrandService
    {
        public BrandService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IEnumerable<BrandDTO>> GetBrandsAsync()
            => await _context.Brands.AsNoTracking()
                    .ProjectTo<BrandDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();

        public async Task<IEnumerable<BrandDTO>> GetBrandsFromCategoryAsync(Guid categoryId)
            => await _context.Brands
                    .Include(x => x.Offers).ThenInclude(x => x.Category)
                    .AsNoTracking()
                    .Where(x => x.Offers.Select(x => x.Category).Select(x => x.Id).Contains(categoryId))
                    .ProjectTo<BrandDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();

        public async Task<BrandDTO> CreateBrandAsync(string name)
        {
            var checkName = await _context.Brands.Where(x => x.Name == name).CountAsync();
            if (checkName > 0)
            {
                throw new NameAlreadyInUseException(name);
            }
            var entity = new Brand
            {
                Name = name
            };
            _context.Brands.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<BrandDTO>(entity);
        }

        public async Task<BrandDTO> UpdateBrandAsync(UpdateBrandDTO dto)
        {
            var brand = await _context.Brands.FindAsync(dto.Id);
            var checkName = await _context.Brands.Where(x => x.Name == dto.Name).CountAsync();
            if (brand == null)
            {
                throw new NotFoundException(nameof(Brand), dto.Id);
            }
            if (checkName > 0)
            {
                throw new NameAlreadyInUseException(dto.Name);
            }

            brand.Name = dto.Name;

            await _context.SaveChangesAsync();

            return _mapper.Map<BrandDTO>(brand);
        }
    }
}
