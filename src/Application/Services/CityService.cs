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
    public class CityService : BaseDataService, ICityService
    {
        public CityService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IEnumerable<CityDTO>> GetCitiesAsync()
            => await _context.Cities.AsNoTracking()
                .ProjectTo<CityDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<CityDTO> CreateCityAsync(string name)
        {
            var checkName = await _context.Cities.Where(x => x.Name == name).CountAsync();
            if (checkName > 0)
            {
                throw new NameAlreadyInUseException(name);
            }
            var entity = new City
            {
                Name = name
            };
            _context.Cities.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CityDTO>(entity);
        }
    }
}
