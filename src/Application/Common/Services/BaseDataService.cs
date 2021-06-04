using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Common.Services
{
    public abstract class BaseDataService
    {
        protected readonly IApplicationDbContext _context;
        protected readonly IMapper _mapper;

        public BaseDataService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
