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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CityService : BaseDataService, ICityService
    {
        public CityService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<CityDTO> GetCityByIdAsync(long id)
            => _mapper.Map<CityDTO>(await _context.Cities.FindAsync(id));

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

        public async Task<CityDTO> UpdateCityAsync(UpdateCityDTO dto)
        {
            var city = await _context.Cities.FindAsync(dto.Id);
            if(city == null)
            {
                throw new NotFoundException(nameof(City), dto.Id);
            }
            var checkName = await _context.Cities.Where(x => x.Name == dto.Name).CountAsync();
            if (checkName > 0)
            {
                throw new NameAlreadyInUseException(dto.Name);
            }
            city.Name = dto.Name;

            await _context.SaveChangesAsync();

            return _mapper.Map<CityDTO>(city);
        }
    }
}
