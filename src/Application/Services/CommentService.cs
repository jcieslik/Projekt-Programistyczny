using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using Application.DAL.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommentService : BaseDataService, ICommentService
    {
        public CommentService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<CommentDTO> GetCommentByIdAsync(long id)
            => _mapper.Map<CommentDTO>(
                await _context.Comments
                .Include(x => x.Order)
                .Include(x => x.Customer)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id)
                );

        public async Task<IEnumerable<CommentDTO>> GetCommentsFromUserAsync(long userId, bool onlyNotHidden = true)
        {
            var comments = _context.Comments
                .Include(c => c.Customer).Include(c => c.Order)
                .AsNoTracking()
                .Where(c => c.Seller.Id == userId);

            comments = onlyNotHidden ? comments.Where(x => !x.IsHidden) : comments;

            return await comments
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PaginatedList<CommentDTO>> GetPaginatedCommentsFromUserAsync(SearchCommentsVM vm)
        {
            var comments = _context.Comments
                .Include(c => c.Customer)
                .Include(c => c.Seller)
                .Include(c => c.Order)
                .ThenInclude(o => o.OfferWithDelivery)
                .ThenInclude(o => o.Offer)
                .AsNoTracking()
                .Where(c => c.Seller.Id == vm.SubjectId);

            comments = vm.OnlyNotHidden ? comments.Where(x => !x.IsHidden) : comments;

            comments = vm.Properties.OrderBy switch
            {
                "rate_asc" => comments.OrderBy(x => x.RateValue),
                "rate_desc" => comments.OrderByDescending(x => x.RateValue),
                "creation" => comments.OrderBy(x => x.Created),
                _ => comments.OrderBy(x => x.Created)
            };

            return await comments.ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(vm.Properties.PageIndex, vm.Properties.PageSize);
        }

        public async Task<CommentDTO> CreateCommentAsync(CreateCommentDTO dto)
        {
            var customer = await _context.Users.FindAsync(dto.CustomerId);
            if (customer == null)
            {
                throw new NotFoundException(nameof(User), dto.CustomerId);
            }
            var order = await _context.Orders
                .Include(o => o.OfferWithDelivery)
                .ThenInclude(o => o.Offer)
                .ThenInclude(o => o.Seller)
                .FirstOrDefaultAsync(o => o.Id == dto.OrderId);

            if (order == null)
            {
                throw new NotFoundException(nameof(order), dto.OrderId);
            }

            var entity = new Comment
            {
                Order = order,
                Customer = customer,
                Content = dto.Content,
                RateValue = dto.RateValue,
                Seller = order.OfferWithDelivery.Offer.Seller,
                IsHidden = false
            };

            _context.Comments.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<CommentDTO>(entity);
        }

        public async Task<CommentDTO> UpdateCommentAsync(UpdateCommentDTO dto)
        {
            var comment = await _context.Comments.FindAsync(dto.Id);
            if (comment == null)
            {
                throw new NotFoundException(nameof(Comment), dto.Id);
            }

            if (!string.IsNullOrEmpty(dto.Content))
            {
                comment.Content = dto.Content;
            }

            if (dto.RateValue.HasValue)
            {
                comment.RateValue = dto.RateValue.Value;
            }

            if (dto.IsHidden.HasValue)
            {
                comment.IsHidden = dto.IsHidden.Value;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<CommentDTO>(comment);
        }
    }
}
