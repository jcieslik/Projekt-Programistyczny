using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    class CartService : BaseDataService, ICartService
    {
        public CartService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IEnumerable<CartOfferDTO>> GetOffersFromCartAsync(long userId)
        {
            var user = await _context.Users
                .Include(e => e.Cart)
                .AsNoTracking()
                .Where(e => e.Id == userId).FirstOrDefaultAsync();



            var offers = _context.CartOffer
                .Include(x => x.Offer).ThenInclude(x => x.Images)
                .Include(x => x.Cart)
                .Where(x => x.Cart.Id == user.Cart.Id);

            var notActiveOffers = offers.Where(x => x.Offer.State != Domain.Enums.OfferState.Awaiting);
            var result = offers.Where(x => x.Offer.State == Domain.Enums.OfferState.Awaiting);
            _context.CartOffer.RemoveRange(notActiveOffers);
            await _context.SaveChangesAsync();

            return await result.ProjectTo<CartOfferDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<Cart> GetCartByUser(long userId)
        {
            var user = await _context.Users.Include(e => e.Cart).AsNoTracking().Where(e => e.Id == userId).FirstOrDefaultAsync();

            if (user.Cart == null)
            {
                await Create(user.Id);
            }

            return await _context.Carts.Include(e => e.Offers).Include(e => e.Customer).AsNoTracking().Where(e => e.CustomerId == user.Id).FirstOrDefaultAsync();
        }

        public async Task AddOfferToCartAsync(long offerId, int amount, long userId)
        {
            var offer = await _context.Offers.FindAsync(offerId);
            var user = await _context.Users.Include(e => e.Cart).AsNoTracking().Where(e => e.Id == userId).FirstOrDefaultAsync();
            var cart = await _context.Carts.FindAsync(user.Cart.Id);
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), offerId);
            }
            if (cart == null)
            {
                throw new NotFoundException(nameof(Cart), userId);
            }

            var cartOffer = _context.CartOffer.Where(e => e.Offer == offer && e.Cart == cart).FirstOrDefault();
            if (cartOffer != default)
            {
                if (offer.ProductCount - cartOffer.ProductsCount > 0)
                {
                    cartOffer.ProductsCount++;
                }
            }
            else
            {
                var entity = new CartOffer
                {
                    Cart = cart,
                    Offer = offer,
                    ProductsCount = amount
                };

                _context.CartOffer.Add(entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveOfferFromCartAsync(long relationId)
        {
            var relation = await _context.CartOffer.FindAsync(relationId);
            _context.CartOffer.Remove(relation);
            await _context.SaveChangesAsync();
        }

        public async Task<long> Create(long userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }

            var entity = new Cart
            {
                CustomerId = user.Id
            };

            _context.Carts.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateProductCountAsync(long userId, long offerId, int productCount)
        {
            var offer = await _context.Offers.FindAsync(offerId);
            var user = await _context.Users.Include(e => e.Cart).AsNoTracking().Where(e => e.Id == userId).FirstOrDefaultAsync();
            var cart = await _context.Carts.FindAsync(user.Cart.Id);
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), offerId);
            }
            if (cart == null)
            {
                throw new NotFoundException(nameof(Cart), userId);
            }

            var cartOffer = _context.CartOffer.Where(e => e.Offer == offer && e.Cart == cart).FirstOrDefault();
            if (cartOffer != default)
            {
                if(offer.ProductCount >= productCount)
                {
                    cartOffer.ProductsCount = productCount;
                }
            } 
            await _context.SaveChangesAsync();
        }
    }
}
