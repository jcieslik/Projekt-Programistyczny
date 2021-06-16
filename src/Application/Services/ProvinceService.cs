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
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProvinceService : BaseDataService, IProvinceService
    {
        public ProvinceService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ProvinceDTO> GetProvinceByIdAsync(long id)
            => _mapper.Map<ProvinceDTO>(await _context.Provinces.FindAsync(id));

        public async Task<IEnumerable<ProvinceDTO>> GetProvincesAsync()
            => await _context.Provinces.AsNoTracking()
                .ProjectTo<ProvinceDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<ProvinceDTO> CreateProvinceAsync(string name)
        {
            var checkName = await _context.Provinces.Where(x => x.Name == name).CountAsync();
            if (checkName > 0)
            {
                throw new NameAlreadyInUseException(name);
            }
            var entity = new Province
            {
                Name = name
            };
            _context.Provinces.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProvinceDTO>(entity);
        }

        public async Task<ProvinceDTO> UpdateProvinceAsync(UpdateProvinceDTO dto)
        {
            var province = await _context.Provinces.FindAsync(dto.Id);
            if(province == null)
            {
                throw new NotFoundException(nameof(Province), dto.Id);
            }
            var checkName = await _context.Provinces.Where(x => x.Name == dto.Name).CountAsync();
            if (checkName > 0)
            {
                throw new NameAlreadyInUseException(dto.Name);
            }
            province.Name = dto.Name;
            await _context.SaveChangesAsync();

            return _mapper.Map<ProvinceDTO>(province);
        }
    }
}
