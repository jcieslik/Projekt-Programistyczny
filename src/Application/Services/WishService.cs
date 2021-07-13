using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

            var checkWish = _context.Wishes.Where(x => x.Offer == offer && x.Customer == customer && !x.IsHidden).Count();

            if(checkWish > 0)
            {
                throw new RelationAlreadyExistException();
            }

            _context.Wishes.Add(wish);

            await _context.SaveChangesAsync();

            return _mapper.Map<WishDTO>(wish);
        }

        public async Task HideWish(long offerId, long userId)
        {
            var user = await _context.Users.Include(u => u.Wishes).ThenInclude(u => u.Offer).Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new NotFoundException(nameof(user), userId);
            }

            var wish = user.Wishes.Where(o => o.Offer.Id == offerId).FirstOrDefault();
            if(wish == null)
            {
                throw new NotFoundException(nameof(wish), offerId);
            }
            wish.IsHidden = true;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckForUserWish(long offerId, long userId)
        {
            var user = await _context.Users.Include(u => u.Wishes).ThenInclude(u => u.Offer).Where(u => u.Id == userId).FirstOrDefaultAsync();
            if(user != null && user.Wishes.Where(w => w.Offer.Id == offerId && w.IsHidden == false).Any())
            {
                return true;
            }
            return false;
        }

        public async Task DeleteAsync(long offerId, long userId)
        {
            var wishes = await _context.Wishes
                .Include(x => x.Offer)
                .Include(x => x.Customer)
                .Where(x => x.Offer.Id == offerId && x.Customer.Id == userId).ToListAsync();

            if (wishes.Count == 0)
            {
                throw new NotFoundException(nameof(Wish));
            }

            _context.Wishes.RemoveRange(wishes);

            await _context.SaveChangesAsync();
        }

    }
}
