using Application.Common.Interfaces;
using Application.Common.Models;
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
    public class OfferService : BaseDataService
    {
        public OfferService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OfferDTO> GetOfferByIdAsync(Guid id)
        {
            var offer = await _context.Offers
                .Include(x => x.Bids).ThenInclude(b => b.Bidder)
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Include(x => x.City)
                .Include(x => x.Province)
                .Include(x => x.Seller)
                .Include(x => x.Comments).ThenInclude(c => c.Customer)
                .Include(x => x.Rates).ThenInclude(r => r.Customer)
                .Include(x => x.Images)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<OfferDTO>(offer);
        }

        public async Task<PaginatedList<OfferWithBaseDataDTO>> GetPaginatedOffersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
