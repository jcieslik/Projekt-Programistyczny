using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FilterService : BaseDataService, IFilterService
    {
        public FilterService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IEnumerable<ProvinceDTO>> GetProvincessAsync()
            => await _context.Provinces.AsNoTracking()
                    .ProjectTo<ProvinceDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();

        public async Task<IEnumerable<OfferType>> GetOfferTypesAsync()
            => await Task.FromResult(Enum.GetValues(typeof(OfferType))
                .Cast<OfferType>()
                .ToList());

        public async Task<IEnumerable<ProductState>> GetProductStatesAsync()
            => await Task.FromResult(Enum.GetValues(typeof(ProductState))
                .Cast<ProductState>()
                .ToList());

        public async Task<FilterVm> GetFiltersData()
        {
            var result = new FilterVm
            {
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
