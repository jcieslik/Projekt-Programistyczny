using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDTO> GetCommentByIdAsync(Guid id);
        Task<IEnumerable<CommentDTO>> GetCommentsFromUserAsync(Guid userId);
        Task<IEnumerable<CommentDTO>> GetCommentsFromOfferAsync(Guid offerId);
        Task<Guid> CreateCommentAsync(CreateCommentDTO dto);
        Task<Guid> UpdateCommentAsync(UpdateCommentDTO dto);
    }
}
