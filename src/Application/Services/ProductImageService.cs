using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductImageService : BaseDataService, IProductImageService
    {
        public ProductImageService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ProductImageDTO> GetProductImageByIdAsync(Guid id)
            => _mapper.Map<ProductImageDTO>(
                await _context.Images
                .Include(x => x.Offer)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id));

        public async Task<IEnumerable<ProductImageDTO>> GetProductImagesFromOfferdAsync(Guid offerId, bool onlyNotHidden)
        {
            var images = _context.Images.Include(i => i.Offer)
            .AsNoTracking()
            .Where(i => i.Offer.Id == offerId);

            images = onlyNotHidden ? images.Where(x => !x.IsHidden) : images;

            return await images.ProjectTo<ProductImageDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<ProductImageDTO> CreateProductImageAsync(CreateProductImageDTO dto)
        {
            var offer = await _context.Offers.FindAsync(dto.OfferId);
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), dto.OfferId);
            }

            var entity = new ProductImage
            {
                Offer = offer,
                ImageTitle = dto.ImageTitle,
                ImageData = dto.ImageData,
                IsMainProductImage = dto.IsMainProductImage,
                IsHidden = false
            };

            _context.Images.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductImageDTO>(entity);
        }


        public async Task<ProductImageDTO> UpdateProductImageAsync(UpdateProductImageDTO dto)
        {
            var image = await _context.Images.FindAsync(dto.Id);
            if (image == null)
            {
                throw new NotFoundException(nameof(ProductImage), dto.Id);
            }

            if (dto.ImageData != null && dto.ImageData.Length > 0 )
            {
                image.ImageData = dto.ImageData;
            }

            if (!string.IsNullOrEmpty(dto.ImageTitle))
            {
                image.ImageTitle = dto.ImageTitle;
            }

            if (dto.IsMainProductImage.HasValue)
            {
                image.IsMainProductImage = dto.IsMainProductImage.Value;
            }

            if (dto.IsHidden.HasValue)
            {
                image.IsHidden = dto.IsHidden.Value;
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<ProductImageDTO>(image);
        }
    }
}
