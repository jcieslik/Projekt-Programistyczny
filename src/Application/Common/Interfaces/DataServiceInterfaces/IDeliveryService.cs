using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.AddOrRemove;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IDeliveryService
    {
        Task AddOfferAndDeliveryMethodRelation(AddDeliveryMethodWihOfferRelationDTO dto);
        Task<DeliveryDTO> CreateDeliveryMethod(CreateDeliveryMethodDTO dto);
        Task DeleteDeliveryMethodAsync(long id);
        Task<DeliveryDTO> GetDeliveryMethodByIdAsync(long id);
        Task<IEnumerable<DeliveryDTO>> GetDeliveryMethodsAsync();
        Task<IEnumerable<OfferDeliveryDTO>> GetDeliveryMethodsFromOfferAsync(long offerId);
        Task RemoveOfferAndDeliveryMethodRelation(long offerId, long deliveryMethodId);
        Task<DeliveryDTO> UpdateDeliveryMethod(UpdateDeliveryMethodDTO dto);
        Task UpdateOfferAndDeliveryMethodRelation(UpdateDeliveryMethodWihOfferRelationDTO dto);
    }
}