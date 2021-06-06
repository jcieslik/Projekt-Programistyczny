﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommentService : BaseDataService
    {
        public CommentService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<CommentDTO> GetCommentByIdAsync(Guid id)
            => _mapper.Map<CommentDTO>(await _context.Comments.FindAsync(id));

        

        public async Task<Guid> CreateCommentAsync(CreateCommentDTO dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            if(user == null)
            {
                throw new NotFoundException(nameof(User), dto.UserId);
            }

            var offer = await _context.Offers.FindAsync(dto.OfferId);
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), dto.OfferId);
            }

            var entity = new Comment
            {
                Offer = offer,
                Customer = user,
                Content = dto.Content,
                IsHidden = false
            };

            _context.Comments.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> UpdateCommentAsync(UpdateCommentDTO dto)
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
            if (dto.IsHidden.HasValue)
            {
                comment.IsHidden = dto.IsHidden.Value;
            }

            await _context.SaveChangesAsync();
            return comment.Id;
        }
    }
}