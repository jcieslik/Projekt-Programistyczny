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
        Task<OrderDTO> GetOrderByIdAsync(long id);
        Task<IEnumerable<OrderDTO>> GetOrdersFromOffer(long offerId);
        Task<IEnumerable<OrderDTO>> GetOrdersFromUser(long userId);
    }
}