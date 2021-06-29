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
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BidService : BaseDataService, IBidService
    {
        public BidService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<BidDTO> GetBidByIdAsync(long id)
            => _mapper.Map<BidDTO>(
                await _context.Bids
                .Include(x => x.Offer)
                .Include(x => x.Bidder)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id)
                );

        public async Task<IEnumerable<BidDTO>> GetBidsFromOfferAsync(long offerId, bool onlyNotHidden)
        {
            var bids = _context.Bids.Include(x => x.Offer).Include(x => x.Bidder)
            .AsNoTracking()
            .Where(x => x.Offer.Id == offerId);

            bids = onlyNotHidden ? bids.Where(x => !x.IsHidden) : bids;

            return await bids.ProjectTo<BidDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<IEnumerable<BidDTO>> GetBidsFromUserAsync(long userId, bool onlyNotHidden)
        {
            var bids = _context.Bids.Include(x => x.Offer).Include(x => x.Bidder)
            .AsNoTracking()
            .Where(x => x.Offer.Id == userId);

            bids = onlyNotHidden ? bids.Where(x => !x.IsHidden) : bids;

            return await bids.ProjectTo<BidDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<BidDTO> CreateBidAsync(CreateBidDTO dto)
        {
            var user = await _context.Users.FindAsync(dto.BidderId);
            var offer = await _context.Offers.FindAsync(dto.OfferId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), dto.BidderId);
            }
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), dto.OfferId);
            }

            if(offer.OfferType != OfferType.Auction)
            {
                throw new OfferIsNotAnAuctionException();
            }


            if(offer.State != OfferState.Awaiting)
            {
                throw new AuctionIsNotAwailableException();
            }

            var entity = new Bid
            {
                Bidder = user,
                Offer = offer,
                Value = dto.Value,
                IsHidden = false
            };

            _context.Bids.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<BidDTO>(entity);
        }

        public async Task<BidDTO> UpdateBidAsync(UpdateBidDTO dto)
        {
            var bid = await _context.Bids.FindAsync(dto.Id);
            if (bid == null)
            {
                throw new NotFoundException(nameof(Bid), dto.Id);
            }

            if (dto.Value.HasValue)
            {
                bid.Value = dto.Value.Value;
            }

            if (dto.IsHidden.HasValue)
            {
                bid.IsHidden = dto.IsHidden.Value;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<BidDTO>(bid);
        }
    }
}
