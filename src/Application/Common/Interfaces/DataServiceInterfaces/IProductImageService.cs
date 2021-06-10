using Application.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IProductImageService
    {
        Task<ProductImageDTO> GetProductImageByIdAsync(Guid id);
    }
}
