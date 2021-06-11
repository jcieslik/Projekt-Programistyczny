using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
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
    public class BidService : BaseDataService
    {
        public BidService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<BidDTO> GetBidByIdAsync(Guid id)
            => _mapper.Map<BidDTO>(await _context.Bids.FindAsync(id));

        public async Task<IEnumerable<BidDTO>> GetBidsFromOfferAsync(Guid offerId)
            => await _context.Bids.Include(x => x.Offer).Include(x => x.Bidder)
            .AsNoTracking()
            .Where(x => x.Offer.Id == offerId)
            .ProjectTo<BidDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        public async Task<IEnumerable<BidDTO>> GetBidsFromUserAsync(Guid userId)
            => await _context.Bids.Include(x => x.Offer).Include(x => x.Bidder)
            .AsNoTracking()
            .Where(x => x.Bidder.Id == userId)
            .ProjectTo<BidDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        public async Task<BidDTO> CreateBidAsync(CreateBidDTO dto)
        {
            var user = await _context.Users.FindAsync(dto.BidderId);
            var offer = await _context.Offers.FindAsync(dto.OfferId);
            if(user == null)
            {
                throw new NotFoundException(nameof(User), dto.BidderId);
            }
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), dto.OfferId);
            }

            var entity = new Bid
            {
                Bidder = user,
                Offer = offer,
                Value = dto.Value
            };

            _context.Bids.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<BidDTO>(entity);
        }
    }
}
