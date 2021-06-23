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
    public class OrderService : BaseDataService, IOrderService
    {
        public OrderService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OrderDTO> GetOrderByIdAsync(long id)
        {
            return _mapper.Map<OrderDTO>(
                await _context.Orders
                .Include(x => x.Offer)
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
                .Include(x => x.Customer).Include(x => x.Offer)
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
                .Include(x => x.Customer).Include(x => x.Offer)
                .AsNoTracking()
                .Where(x => x.Offer.Id == offerId)
                .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<OrderDTO> CreateOrderAsync(CreateOrderDTO dto)
        {
            var user = await _context.Users.FindAsync(dto.CustomerId);
            var offer = await _context.Offers.FindAsync(dto.OfferId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), dto.CustomerId);
            }
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), dto.OfferId);
            }

            var entity = new Order
            {
                OrderStatus = (OrderStatus)dto.OrderStatus,
                Customer = user,
                Offer = offer
            };

            _context.Orders.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(entity);
        }

        public async Task<OrderDTO> ChangeOrderStatus(UpdateOrderDTO dto)
        {
            var order = await _context.Orders.FindAsync(dto.Id);
            if (order == null)
            {
                throw new NotFoundException(nameof(Order), dto.Id);
            }
            order.OrderStatus = (OrderStatus)dto.OrderStatus;

            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDTO>(order);
        }
    }
}
