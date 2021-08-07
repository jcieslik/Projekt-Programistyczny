using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : BaseDataService, IOrderService
    {
        public OrderService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OrderDTO> GetOrderByIdAsync(long id)
        {
            return _mapper.Map<OrderDTO>(
                await _context.Orders
                .Include(x => x.OfferWithDelivery)
                .Include(x => x.Customer)
                .SingleOrDefaultAsync(x => x.Id == id)
                );
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersFromUser(long userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            return await _context.Orders
                .Include(x => x.Customer).Include(x => x.OfferWithDelivery)
                .AsNoTracking()
                .Where(x => x.Customer.Id == userId)
                .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersFromOffer(long offerId)
        {
            var offer = await _context.Offers.FindAsync(offerId);
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), offerId);
            }
            return await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.OfferWithDelivery).ThenInclude(x => x.Offer)
                .AsNoTracking()
                .Where(x => x.OfferWithDelivery.Offer.Id == offerId)
                .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<OrderDTO> CreateOrderAsync(CreateOrderDTO dto)
        {
            var user = await _context.Users.FindAsync(dto.CustomerId);
            var offerWithDelivery = await _context.OffersAndDeliveryMethods
                .Include(x => x.Offer)
                .SingleOrDefaultAsync(x => x.Id == dto.OfferWithDeliveryId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), dto.CustomerId);
            }
            if (offerWithDelivery == null)
            {
                throw new NotFoundException(nameof(OfferAndDeliveryMethod), dto.OfferWithDeliveryId);
            }
            if (offerWithDelivery.Offer.State != OfferState.Awaiting)
            {
                throw new AuctionIsNotAwailableException();
            }

            var entity = new Order
            {
                OrderStatus = dto.OrderStatus,
                Customer = user,
                OfferWithDelivery = offerWithDelivery,
                PaymentDate = dto.PaymentDate,
                ProductCount = dto.ProductCount,
                FullPrice = dto.FullPrice,
                DestinationStreet = dto.DestinationStreet,
                DestinationCity = dto.DestinationCity,
                DestinationPostCode = dto.DestinationPostCode
            };

            _context.Orders.Add(entity);

            offerWithDelivery.Offer.ProductCount -= dto.ProductCount;

            if(offerWithDelivery.Offer.ProductCount <= 0)
            {
                offerWithDelivery.Offer.State = OfferState.Finished;
            }

            if(dto.CartOfferId.HasValue)
            {
                var cartOffer = await _context.CartOffer.FindAsync(dto.CartOfferId);
                if (offerWithDelivery == null)
                {
                    throw new NotFoundException(nameof(OfferAndDeliveryMethod), dto.OfferWithDeliveryId);
                }

                _context.CartOffer.Remove(cartOffer);
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(entity);
        }

        public async Task<OrderDTO> UpdateOrder(UpdateOrderDTO dto)
        {
            var order = await _context.Orders
                .Include(x => x.OfferWithDelivery).ThenInclude(x => x.Offer)
                .SingleOrDefaultAsync(x => x.Id == dto.Id);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), dto.Id);
            }

            if (dto.OrderStatus.HasValue)
            {
                order.OrderStatus = dto.OrderStatus.Value;
                if (order.OrderStatus == OrderStatus.Canceled)
                {
                    order.OfferWithDelivery.Offer.ProductCount += order.ProductCount;
                }
            }
            if (!string.IsNullOrEmpty(dto.DestinationCity))
            {
                order.DestinationCity = dto.DestinationCity;
            }
            if (!string.IsNullOrEmpty(dto.DestinationStreet))
            {
                order.DestinationStreet = dto.DestinationStreet;
            }
            if (!string.IsNullOrEmpty(dto.DestinationPostCode))
            {
                order.DestinationPostCode = dto.DestinationPostCode;
            }

            if (dto.PaymentDate.HasValue)
            {
                order.PaymentDate = dto.PaymentDate;
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<PaginatedList<OrderDTO>> GetPaginatedOrdersByCustomer(long userId, PaginationProperties pagination)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            var orders = _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.OfferWithDelivery)
                .AsNoTracking()
                .Where(x => x.Customer.Id == userId);

            return await orders.ProjectTo<OrderDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(pagination.PageIndex, pagination.PageSize);
        }

        public async Task<PaginatedList<OrderDTO>> GetPaginatedOrdersBySeller(long userId, PaginationProperties pagination)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            var orders = _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.OfferWithDelivery)
                .ThenInclude(x => x.Offer)
                .ThenInclude(x => x.Seller)
                .AsNoTracking()
                .Where(x => x.OfferWithDelivery.Offer.Seller.Id == userId);

            return await orders.ProjectTo<OrderDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(pagination.PageIndex, pagination.PageSize);
        }
    }
}
