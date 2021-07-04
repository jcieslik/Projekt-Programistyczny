using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Add;
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
    class CartService : BaseDataService, ICartService
    {
        public CartService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IEnumerable<OfferWithBaseDataDTO>> GetOffersFromCartAsync(long userId)
        {
            var cartId = (await GetCartByUser(userId)).Id;
            var offers = _context.Carts
                .Include(x => x.Offers).ThenInclude(x => x.Seller)
                .Include(x => x.Offers).ThenInclude(x => x.Images)
                .Include(x => x.Offers).ThenInclude(x => x.Bids)
                .AsNoTracking()
                .First(x => x.Id == cartId)
                .Offers.ToList();
                //.Select(x => x.Offers.ToList());
                //.ProjectTo<OfferWithBaseDataDTO>(_mapper.ConfigurationProvider)
                
            return null; 
        }

        public async Task AddOfferToCartAsync(long offerId, long userId)
        {
            var offer = await _context.Offers.FindAsync(offerId);
            var cart = await GetCartByUser(userId);
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), offerId);
            }

            cart.Offers.Add(offer);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveOfferFromCartAsync(long offerId, long userId)
        {
            var offer = await _context.Offers.FindAsync(offerId);
            var cart = await GetCartByUser(userId);
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), offerId);
            }

            cart.Offers.Remove(offer);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCartByUser(long userId) 
        {
            var user = await _context.Users.Include(e => e.Cart).AsNoTracking().Where(e => e.Id == userId).FirstOrDefaultAsync(); // ZROBIC INCLUDE TEGO JEBANEGO CARTA I DO NIEGO INCLUDE OFERT

            if(user.Cart == null)
            {
                await Create(user.Id);
            }

            return await _context.Carts.Include(e => e.Offers).Include(e => e.Customer).AsNoTracking().Where(e => e.CustomerId == user.Id).FirstOrDefaultAsync();
        }

        public async Task<long> Create(long userId)
        {
            var entity = new Cart
            {
                CustomerId = userId
            };

            _context.Carts.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
