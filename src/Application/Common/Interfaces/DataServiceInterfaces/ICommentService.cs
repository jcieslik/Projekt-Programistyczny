﻿using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface ICommentService
    {
        Task<CommentDTO> GetCommentByIdAsync(Guid id);
        Task<IEnumerable<CommentDTO>> GetCommentsFromUserAsync(Guid userId);
        Task<IEnumerable<CommentDTO>> GetCommentsFromOfferAsync(Guid offerId);
        Task<CommentDTO> CreateCommentAsync(CreateCommentDTO dto);
        Task<CommentDTO> UpdateCommentAsync(UpdateCommentDTO dto);
    }
}