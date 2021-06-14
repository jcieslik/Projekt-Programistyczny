using Application.Common.Interfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : BaseDataService
    {
        public OrderService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OrderDTO> GetOrderByIdAsync(Guid id)
        {
            return _mapper.Map<OrderDTO>(
                await _context.Orders
                .Include(x => x.Offer)
                .Include(x => x.Customer)
                .SingleOrDefaultAsync(x => x.Id == id)
                );
        }
    }
}
