using Application.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRateService
    {
        Task<ProductRateDTO> GetRateByIdAsync(Guid id);
    }
}
