using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using AutoMapper;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Services
{
    public class WishService : BaseDataService, IWishService
    {
        public WishService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<WishDTO> CreateWishAsync(CreateWishDto dto)
        {
            var offer = await _context.Offers.FindAsync(dto.OfferId);
            var customer = await _context.Users.FindAsync(dto.CustomerId);
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), dto.OfferId);
            }
            if (customer == null)
            {
                throw new NotFoundException(nameof(User), dto.CustomerId);
            }

            var wish = new Wish
            {
                Customer = customer,
                Offer = offer
            };

            _context.Wishes.Add(wish);

            await _context.SaveChangesAsync();

            return _mapper.Map<WishDTO>(wish);
        }

        public async Task HideWish(long id)
        {
            var wish = await _context.Wishes.FindAsync(id);
            if (wish == null)
            {
                throw new NotFoundException(nameof(Wish), id);
            }
            wish.IsHidden = true;
        }

    }
}
