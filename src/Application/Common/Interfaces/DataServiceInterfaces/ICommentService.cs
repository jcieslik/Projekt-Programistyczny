using Application.Common.Models;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using Application.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface ICommentService
    {
        Task<CommentDTO> GetCommentByIdAsync(long id);
        Task<IEnumerable<CommentDTO>> GetCommentsFromUserAsync(long userId, bool onlyNotHidden = true);
        Task<IEnumerable<CommentDTO>> GetCommentsFromOfferAsync(long offerId, bool onlyNotHidden = true);
        Task<PaginatedList<CommentDTO>> GetPaginatedCommentsFromUserAsync(SearchCommentsVM vm);
        Task<PaginatedList<CommentDTO>> GetPaginatedCommentsFromOfferAsync(SearchCommentsVM vm);
        Task<CommentDTO> CreateCommentAsync(CreateCommentDTO dto);
        Task<CommentDTO> UpdateCommentAsync(UpdateCommentDTO dto);
    }
}
