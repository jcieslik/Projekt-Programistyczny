using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.AddOrRemove;
using Application.DAL.DTO.CommandDTOs.Create;
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
    public class DeliveryService : BaseDataService, IDeliveryService
    {
        public DeliveryService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<DeliveryDTO> GetDeliveryMethodByIdAsync(long id)
        {
            return _mapper.Map<DeliveryDTO>(await _context.DeliveryMethods.FindAsync(id));
        }

        public async Task<IEnumerable<DeliveryDTO>> GetDeliveryMethodsAsync()
        {
            return await _context.DeliveryMethods
                .AsNoTracking()
                .ProjectTo<DeliveryDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<DeliveryDTO> CreateDeliveryMethod(CreateDeliveryMethodDTO dto)
        {
            var entity = new DeliveryMethod
            {
                BasePrice = dto.BasePrice,
                Name = dto.Name
            };

            _context.DeliveryMethods.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<DeliveryDTO>(entity);
        }

        public async Task<DeliveryDTO> UpdateDeliveryMethod(UpdateDeliveryMethodDTO dto)
        {
            var method = await _context.DeliveryMethods.FindAsync(dto.Id);
            if (method == null)
            {
                throw new NotFoundException(nameof(DeliveryMethod), dto.Id);
            }

            if (dto.BasePrice.HasValue)
            {
                method.BasePrice = dto.BasePrice.Value;
            }

            if (!string.IsNullOrEmpty(dto.Name))
            {
                method.Name = dto.Name;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<DeliveryDTO>(method);
        }

        public async Task DeleteDeliveryMethodAsync(long id)
        {
            var method = await _context.DeliveryMethods.FindAsync(id);
            if (method == null)
            {
                throw new NotFoundException(nameof(DeliveryMethod), id);
            }

            _context.DeliveryMethods.Remove(method);

            await _context.SaveChangesAsync();
        }

        public async Task AddOfferAndDeliveryMethodRelation(AddDeliveryMethodWihOfferRelationDTO dto)
        {
            var offer = await _context.Offers.FindAsync(dto.OfferId);
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), dto.OfferId);
            }
            var method = await _context.DeliveryMethods.FindAsync(dto.DeliveryMethodId);
            if (method == null)
            {
                throw new NotFoundException(nameof(DeliveryMethod), dto.DeliveryMethodId);
            }

            var relationCheck = await _context.OffersAndDeliveryMethods
                .Include(x => x.Offer)
                .Include(x => x.DeliveryMethod)
                .Where(x => x.Offer.Id == dto.OfferId && x.DeliveryMethod.Id == dto.DeliveryMethodId)
                .SingleOrDefaultAsync();

            if (relationCheck != null)
            {
                throw new RelationAlreadyExistException();
            }

            var entity = new OfferAndDeliveryMethod
            {
                DeliveryMethod = method,
                Offer = offer,
                DeliveryFullPrice = dto.FullPrice
            };

            _context.OffersAndDeliveryMethods.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveOfferAndDeliveryMethodRelation(long offerId, long deliveryMethodId)
        {
            var relation = await _context.OffersAndDeliveryMethods
                .Include(x => x.Offer)
                .Include(x => x.DeliveryMethod)
                .Where(x => x.Offer.Id == offerId && x.DeliveryMethod.Id == deliveryMethodId)
                .SingleOrDefaultAsync();

            if (relation == null)
            {
                throw new NotFoundException(nameof(OfferAndDeliveryMethod));
            }

            _context.OffersAndDeliveryMethods.Remove(relation);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DeliveryDTO>> GetDeliveryMethodsFromOfferAsync(long offerId)
        {
            var offer = await _context.Offers.FindAsync(offerId);
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), offerId);
            }

            return await _context.OffersAndDeliveryMethods
                .Include(x => x.Offer)
                .Include(x => x.DeliveryMethod)
                .Where(x => x.Offer.Id == offerId)
                .Select(x => new DeliveryDTO { Id = x.DeliveryMethod.Id, Name = x.DeliveryMethod.Name, Price = x.DeliveryFullPrice})
                .ToListAsync();
        }
    }
}
