using Application.Common.Interfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FilterService : BaseDataService
    {
        public FilterService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<FilterVm> GetFiltersData()
        {
            var result = new FilterVm
            {
                Brands = await _context.Brands.AsNoTracking()
                    .ProjectTo<BrandDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync(),
                Cities = await _context.Cities.AsNoTracking()
                    .ProjectTo<CityDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync(),
                Provinces = await _context.Provinces.AsNoTracking()
                    .ProjectTo<ProvinceDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync(),
                OfferTypes = await Task.FromResult(Enum.GetValues(typeof(OfferType))
                .Cast<OfferType>()
                .ToList()),

                ProductStates = await Task.FromResult(Enum.GetValues(typeof(ProductState))
                .Cast<ProductState>()
                .ToList())
            };

            return result;
        }
    }
}
