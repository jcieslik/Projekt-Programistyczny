﻿using Application.Common.Exceptions;
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

        public async Task<BaseMessageDTO> GetMessageByIdAsync(long id)
            => _mapper.Map<BaseMessageDTO>(
                    await _context.Messages.FindAsync(id)
                );

        public async Task<MessageDTO> GetMessageTransmissionByIdAsync(long id)
            => _mapper.Map<MessageDTO>(
                await _context.MessageTransmissions
                .Include(x => x.Recipient)
                .Include(x => x.Sender)
                .Include(x => x.Message)
                .SingleOrDefaultAsync(x => x.Id == id)
                );
        public async Task<PaginatedList<MessageDTO>> GetPaginatedMessagesFromUserAsync(long userId, int mailboxType, PaginationProperties properties)
        {
            var messages = _context.MessageTransmissions
            .Include(m => m.Recipient)
            .Include(m => m.Sender)
            .Include(m => m.MailboxOwner)
            .Include(m => m.Message)
            .AsNoTracking()
            .Where(x => x.MailboxOwner.Id == userId && x.MailboxType == (MailboxType)mailboxType && !x.IsHidden);

            messages = properties.OrderBy switch
            {
                "delivery_time_asc" => messages.OrderBy(x => x.Created),
                "delivery_time_desc" => messages.OrderByDescending(x => x.Created),
                _ => messages.OrderBy(x => x.Created)
            };

            return await messages.ProjectTo<MessageDTO>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(properties.PageIndex, properties.PageSize);
        }

        public async Task<BaseMessageDTO> CreateMessageAsync(CreateMessageDTO dto)
        {

            var entity = new Message
            {
                SendDate = dto.SendDate,
                Topic = dto.Topic,
                Content = dto.Content,
                IsHidden = false,
            };

            _context.Messages.Add(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<BaseMessageDTO>(entity);
        }

        public async Task<MessageDTO> CreateTransmissionAsync(CreateTransmissionDTO dto)
        {
            var owner = await _context.Users.FindAsync(dto.MailboxOwnerId);
            if (owner == null)
            {
                throw new NotFoundException(nameof(User), dto.MailboxOwnerId);
            }

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

            var message = await _context.Messages.FindAsync(dto.MessageId);
            if (message == null)
            {
                throw new NotFoundException(nameof(Message), dto.MessageId);
            }

            var entity = new MessageTransmission
            {
                MailboxOwner = owner,
                Sender = sender,
                Recipient = recipient,
                Message = message,
                Status = (TransmissionStatus)dto.Status,
                MailboxType = (MailboxType)dto.MailboxType,
                IsHidden = false
            };

            _context.MessageTransmissions.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<MessageDTO>(entity);
        }

        public async Task<BaseMessageDTO> UpdateMessageAsync(UpdateMessageDTO dto)
        {
            var message = await _context.Messages.FindAsync(dto.Id);
            if (message == null)
            {
                throw new NotFoundException(nameof(User), dto.Id);
            }

            message.IsHidden = dto.IsHidden;

            await _context.SaveChangesAsync();

            return _mapper.Map<BaseMessageDTO>(message);
        }
        public async Task<MessageDTO> UpdateTransmissionAsync(UpdateTransmissionDTO dto)
        {
            var transmission = await _context.MessageTransmissions.FindAsync(dto.Id);
            if (transmission == null)
            {
                throw new NotFoundException(nameof(MessageTransmission), dto.Id);
            }

            if (dto.IsHidden.HasValue)
            {
                transmission.IsHidden = dto.IsHidden.Value;
            }

            if (dto.Status.HasValue)
            {
                transmission.Status = (TransmissionStatus)dto.Status.Value;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<MessageDTO>(transmission);
        }
    }
}
