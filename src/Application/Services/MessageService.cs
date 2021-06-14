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
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MessageService : BaseDataService, IMessageService
    {
        public MessageService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<MessageDTO> GetMessageByIdAsync(Guid id)
            => _mapper.Map<MessageDTO>(
                await _context.Messages
                .Include(x => x.Recipient)
                .Include(x => x.Sender)
                .SingleOrDefaultAsync(x => x.Id == id)
                );

        public async Task<PaginatedList<MessageDTO>> GetPaginatedMessagesFromUserAsync(Guid userId, PaginationProperties properties)
        {
            var messages = _context.Messages
            .Include(m => m.Recipient).Include(m => m.Sender)
            .AsNoTracking()
            .Where(x => x.Sender.Id == userId || x.Recipient.Id == userId);

            messages = properties.OrderBy switch
            {
                "delivery_time_asc" => messages.OrderBy(x => x.Created),
                "delivery_time_desc" => messages.OrderByDescending(x => x.Created),
                _ => messages.OrderBy(x => x.Created)
            };

            return await messages.ProjectTo<MessageDTO>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(properties.PageIndex, properties.PageSize);
        }

        public async Task<Guid> CreateMessageAsync(CreateMessageDTO dto)
        {
            var sender = await _context.Users.FindAsync(dto.SenderId);
            if (sender == null)
            {
                throw new NotFoundException(nameof(User), dto.SenderId);
            }

            var recipient = await _context.Users.FindAsync(dto.RecipientId);
            if (recipient == null)
            {
                throw new NotFoundException(nameof(User), dto.RecipientId);
            }

            var entity = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SendDate = dto.SendDate,
                Topic = dto.Topic,
                Content = dto.Content,
                IsHidden = false,
                MailboxType = (MailboxType)dto.MailboxType
            };

            _context.Messages.Add(entity);

            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> UpdateMessageAsync(UpdateMessageDTO dto)
        {
            var message = await _context.Messages.FindAsync(dto.Id);
            if (message == null)
            {
                throw new NotFoundException(nameof(User), dto.Id);
            }

            message.IsHidden = dto.IsHidden;

            await _context.SaveChangesAsync();

            return message.Id;
        }
    }
}
