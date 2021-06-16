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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RateService : BaseDataService, IRateService
    {
        public RateService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

<<<<<<< HEAD
        public async Task<ProductRateDTO> GetRateByIdAsync(Guid id)
            => _mapper.Map<ProductRateDTO>(
                await _context.Rates
                .Include(x => x.Customer)
                .Include(x => x.Offer)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id));
=======
        public async Task<ProductRateDTO> GetRateByIdAsync(long id)
            => _mapper.Map<ProductRateDTO>(await _context.Rates.FindAsync(id));
>>>>>>> 498944bc2f210c91fb13939836a43f93ac551954

        public async Task<IEnumerable<ProductRateDTO>> GetRatesFromUserAsync(long userId, bool onlyNotHidden = true)
        {
            var rates = _context.Rates
                       .Include(c => c.Customer).Include(c => c.Offer)
                       .AsNoTracking()
                       .Where(c => c.Customer.Id == userId);

            rates = onlyNotHidden ? rates.Where(x => !x.IsHidden) : rates;

            return await rates.ProjectTo<ProductRateDTO>(_mapper.ConfigurationProvider)
                       .ToListAsync();
        }

        public async Task<IEnumerable<ProductRateDTO>> GetRatesFromOfferAsync(long offerId, bool onlyNotHidden = true)
        {
            var rates = _context.Rates
                       .Include(c => c.Customer).Include(c => c.Offer)
                       .AsNoTracking()
                       .Where(c => c.Offer.Id == offerId);

            rates = onlyNotHidden ? rates.Where(x => !x.IsHidden) : rates;

            return await rates.ProjectTo<ProductRateDTO>(_mapper.ConfigurationProvider)
                       .ToListAsync();
        }

        public async Task<ProductRateDTO> CreateRateAsync(CreateProductRateDTO dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), dto.UserId);
            }

            var offer = await _context.Offers.FindAsync(dto.OfferId);
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), dto.OfferId);
            }

            var entity = new ProductRate
            {
                Offer = offer,
                Customer = user,
                Value = dto.Value,
                IsHidden = false
            };

            _context.Rates.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductRateDTO>(entity);
        }

        public async Task<ProductRateDTO> UpdateRateAsync(UpdateProductRateDTO dto)
        {
            var rate = await _context.Rates.FindAsync(dto.Id);
            if (rate == null)
            {
                throw new NotFoundException(nameof(ProductRate), dto.Id);
            }

            if (dto.Value.HasValue)
            {
                rate.Value = dto.Value.Value;
            }
            if (dto.IsHidden.HasValue)
            {
                rate.IsHidden = dto.IsHidden.Value;
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<ProductRateDTO>(rate);
        }
    }
}
