using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateOrderAsync(CreateOrderDTO dto);
        Task<OrderDTO> CreateOrderAsync(UpdateOrderDTO dto);
        Task<OrderDTO> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<OrderDTO>> GetOrdersFromOffer(Guid offerId);
        Task<IEnumerable<OrderDTO>> GetOrdersFromUser(Guid userId);
    }
}