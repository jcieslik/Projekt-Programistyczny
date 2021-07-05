using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Add;
using Application.DAL.DTO.CommandDTOs.AddOrRemove;
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

        public async Task<IEnumerable<CartOfferDTO>> GetOffersFromCartAsync(long cartId)
        {
            return await _context.CartOffer
                .Include(x => x.Offer).ThenInclude(x => x.Images)
                .Include(x => x.Cart)
                .Where(x => x.Cart.Id == cartId)
                .ProjectTo<CartOfferDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task AddOfferToCartAsync(AddOfferToCartDTO dto)
        {
            var offer = await _context.Offers.FindAsync(dto.OfferId);
            var cart = await _context.Carts.FindAsync(dto.CartId);
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), dto.OfferId);
            }
            if (cart == null)
            {
                throw new NotFoundException(nameof(Cart), dto.CartId);
            }

            var entity = new CartOffer
            {
                Cart = cart,
                Offer = offer,
                ProductsCount = dto.ProductsCount
            };

            _context.CartOffer.Add(entity);
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
    }
}
