using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OfferService : BaseDataService, IOfferService
    {
        public OfferService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OfferDTO> GetOfferByIdAsync(long id)
        {
            var offer = await _context.Offers
                .Include(x => x.Bids).ThenInclude(b => b.Bidder)
                .Include(x => x.Category)
                .Include(x => x.Province)
                .Include(x => x.Seller)
                .Include(x => x.Images)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<OfferDTO>(offer);
        }

        public async Task<IEnumerable<OfferWithBaseDataDTO>> GetOffersFromUserAsync(long userId)
        {
            var offers = _context.Offers
                .Include(x => x.Bids)
                .Include(x => x.Images)
                .AsNoTracking()
                .Where(x => x.Seller.Id == userId);

            return await offers.ProjectTo<OfferWithBaseDataDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<PaginatedList<OfferWithBaseDataDTO>> GetPaginatedOffersFromUserActiveWishesAsync(long userId, FilterModel filterModel, PaginationProperties paginationProperties)
        {
            var offers = _context.Wishes
                .Include(x => x.Customer)
                .Include(x => x.Offer).ThenInclude(x => x.Seller)
                .Include(x => x.Offer).ThenInclude(x => x.Images)
                .Include(x => x.Offer).ThenInclude(x => x.Bids)
                .AsNoTracking()
                .Where(x => x.Customer.Id == userId && !x.IsHidden)
                .Select(x => x.Offer);

            if (filterModel.CategoryId.HasValue)
            {
                offers = offers.Where(x => x.Category.Id == filterModel.CategoryId);
            }
            if (filterModel.SearchText != null && filterModel.SearchText.Length > 0)
            {
                List<string> words = filterModel.SearchText.Split(' ').ToList();
                foreach (string word in words)
                {
                    offers = offers.Where(x => x.Description.Contains(word) || x.Title.Contains(word));
                }
            }

            offers = paginationProperties.OrderBy switch
            {
                "price_asc" => offers.OrderBy(x => x.PriceForOneProduct),
                "price_desc" => offers.OrderByDescending(x => x.PriceForOneProduct),
                "end_date_asc" => offers.OrderBy(x => x.EndDate),
                "end_date_desc" => offers.OrderByDescending(x => x.EndDate),
                "creation_asc" => offers.OrderBy(x => x.Created),
                "creation_desc" => offers.OrderByDescending(x => x.Created),
                _ => offers.OrderBy(x => x.Created)
            };

            return await offers.ProjectTo<OfferWithBaseDataDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync<OfferWithBaseDataDTO>(paginationProperties.PageIndex, paginationProperties.PageSize);
        }

        public async Task<PaginatedList<OfferWithBaseDataDTO>> GetPaginatedOffersAsync(FilterModel filterModel, PaginationProperties paginationProperties, OfferState state = OfferState.All)
        {
            var offers = _context.Offers
                .Include(x => x.Bids)
                .Include(x => x.Images)
                .Include(x => x.Category)
                .Include(x => x.Province)
                .AsNoTracking()
                .Where(
                x => 
                state == OfferState.All ? !x.IsHidden && 
                (x.State == OfferState.Awaiting || x.State == OfferState.Finished || x.State == OfferState.Outdated) && 
                DateTime.Compare(x.StartDate, DateTime.Now) <= 0 &&
                DateTime.Compare(x.EndDate, DateTime.Now) >= 0 :
                !x.IsHidden && x.State == state &&
                DateTime.Compare(x.StartDate, DateTime.Now) <= 0 &&
                DateTime.Compare(x.EndDate, DateTime.Now) >= 0

                );

            if (filterModel.CategoryId.HasValue)
            {
                List<long> ids = GetChildrenCategoriesIds(filterModel.CategoryId.Value);
                ids.Add(filterModel.CategoryId.Value);

                offers = offers.Where(x => ids.Contains(x.Category.Id));
            }
            
            if (filterModel.OfferType.HasValue)
            {
                offers = offers.Where(x => x.OfferType == filterModel.OfferType);
            }
            if (filterModel.ProductState.HasValue)
            {
                offers = offers.Where(x => x.ProductState == filterModel.ProductState);
            }
            if (filterModel.OfferState.HasValue)
            {
                offers = offers.Where(x => x.State == filterModel.OfferState);
            }
            if (filterModel.Brands.Count > 0)
            {
                offers = offers.Where(x => filterModel.Brands.Contains(x.Brand));
            }
            if (filterModel.Cities.Count > 0)
            {
                offers = offers.Where(x => filterModel.Cities.Contains(x.City));
            }
            if (filterModel.ProvincesIds.Count > 0)
            {
                offers = offers.Where(x => filterModel.ProvincesIds.Contains(x.Province.Id));
            }
            if (filterModel.MaxPrice.HasValue)
            {
                offers = offers.Where(x => x.PriceForOneProduct <= filterModel.MaxPrice.Value);
            }
            if (filterModel.MinPrice.HasValue)
            {
                offers = offers.Where(x => x.PriceForOneProduct >= filterModel.MinPrice.Value);
            }
            if (filterModel.SellerId.HasValue)
            {
                offers = offers.Where(x => x.Seller.Id == filterModel.SellerId);
            }
            if (filterModel.SearchText != null && filterModel.SearchText.Length > 0)
            {
                List<string> words = filterModel.SearchText.Split(' ').ToList();
                foreach (string word in words)
                {
                    offers = offers.Where(x => x.Description.Contains(word) || x.Title.Contains(word));
                }
            }
            offers = paginationProperties.OrderBy switch
            {
                "price_asc" => offers.OrderBy(x => x.PriceForOneProduct),
                "price_desc" => offers.OrderByDescending(x => x.PriceForOneProduct),
                "end_date_asc" => offers.OrderBy(x => x.EndDate),
                "end_date_desc" => offers.OrderByDescending(x => x.EndDate),
                "creation_asc" => offers.OrderBy(x => x.Created),
                "creation_desc" => offers.OrderByDescending(x => x.Created),
                _ => offers.OrderBy(x => x.Created)
            };

            return await offers.ProjectTo<OfferWithBaseDataDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync<OfferWithBaseDataDTO>(paginationProperties.PageIndex, paginationProperties.PageSize);
        }

        public async Task<OfferDTO> CreateOfferAsync(CreateOfferDTO dto)
        {
            var seller = await _context.Users.FindAsync(dto.SellerId);
            var province = await _context.Provinces.FindAsync(dto.ProvinceId);
            var category = await _context.Categories.FindAsync(dto.CategoryId);
            if (seller == null)
            {
                throw new NotFoundException(nameof(User), dto.SellerId);
            }
            if (province == null)
            {
                throw new NotFoundException(nameof(Province), dto.ProvinceId);
            }
            if (category == null)
            {
                throw new NotFoundException(nameof(ProductCategory), dto.CategoryId);
            }

            var entity = new Offer
            {
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                ProductCount = dto.ProductCount,
                State = dto.State,
                OfferType = dto.OfferType,
                ProductState = dto.ProductState,
                PriceForOneProduct = dto.PriceForOneProduct,
                EndDate = dto.EndDate,
                IsHidden = false,
                Seller = seller,
                City = dto.City,
                Brand = dto.Brand,
                Category = category,
                Province = province,
                MinimalBid = dto.MinimalBid
            };

            _context.Offers.Add(entity);

            foreach(var item in dto.Images)
            {
                var image = new ProductImage
                {
                    Offer = entity,
                    ImageData = item.ImageData,
                    IsHidden = false,
                    IsMainProductImage = item.IsMainProductImage
                };
                _context.Images.Add(image);
            }

            foreach(var item in dto.DeliveryMethods)
            {
                var deliveryMethod = new OfferAndDeliveryMethod
                {
                    Offer = entity,
                    DeliveryMethod = await _context.DeliveryMethods.FindAsync(item.DeliveryMethodId),
                    DeliveryFullPrice = item.DeliveryFullPrice,
                };
                _context.OffersAndDeliveryMethods.Add(deliveryMethod);
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<OfferDTO>(entity);
        }

        public async Task<OfferDTO> UpdateOfferAsync(UpdateOfferDTO dto)
        {

            var offer = await _context.Offers.FindAsync(dto.Id);
            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), dto.Id);
            }

            if (!string.IsNullOrEmpty(dto.City))
            {
                offer.City = dto.City;
            }
            if (dto.ProvinceId.HasValue)
            {
                var province = await _context.Provinces.FindAsync(dto.ProvinceId.Value);
                offer.Province = province ?? throw new NotFoundException(nameof(Province), dto.ProvinceId.Value);
            }
            if (!string.IsNullOrEmpty(dto.Brand))
            {
                offer.Brand = dto.Brand;
            }
            if (dto.CategoryId.HasValue)
            {
                var category = await _context.Categories.FindAsync(dto.CategoryId.Value);
                offer.Category = category ?? throw new NotFoundException(nameof(ProductCategory), dto.CategoryId.Value);
            }
            if (dto.PriceForOneProduct.HasValue)
            {
                offer.PriceForOneProduct = dto.PriceForOneProduct.Value;
            }
            if (dto.ProductCount.HasValue)
            {
                offer.ProductCount = dto.ProductCount.Value;
            }
            if (dto.OfferType.HasValue)
            {
                offer.OfferType = (OfferType)dto.PriceForOneProduct.Value;
            }
            if (dto.State.HasValue)
            {
                offer.State = (OfferState)dto.State.Value;
            }
            if (dto.ProductState.HasValue)
            {
                offer.ProductState = (ProductState)dto.ProductState.Value;
            }
            if (dto.StartDate.HasValue)
            {
                offer.StartDate = dto.StartDate.Value;
            }
            if (dto.EndDate.HasValue)
            {
                offer.EndDate = dto.EndDate.Value;
            }
            if (!string.IsNullOrEmpty(dto.Title))
            {
                offer.Title = dto.Title;
            }
            if (!string.IsNullOrEmpty(dto.Description))
            {
                offer.Description = dto.Description;
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<OfferDTO>(offer);
        }

        private List<long> GetChildrenCategoriesIds(long parentId)
        {
            var childrenIds = _context.Categories
                .AsNoTracking()
                .Include(x => x.ParentCategory)
                .Where(x => x.ParentCategory.Id == parentId).Select(x => x.Id).ToList();
            List<long> result = new();
            foreach (long id in childrenIds)
            {
                var ids = GetChildrenCategoriesIds(id);
                result.AddRange(ids);
            }
            return result;
        }

        public async Task<IEnumerable<CartOfferDTO>> GetOffersFromCartAsync(long cartId)
        {
            return await _context.CartOffer
                .Include(x => x.Offer).ThenInclude(x => x.Images)
                .Include(x => x.Cart)
                .Where(x => x.Cart.Id == cartId)
                .ProjectTo<CartOfferDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task ChangeStatusOfOffersAfterEndDate()
        {
            var offers = _context.Offers
                .Include(x => x.Bids)
                .ThenInclude(x => x.Bidder)
                .Where(x => x.State == OfferState.Awaiting && x.EndDate < DateTime.Now);

            foreach(var offer in offers)
            {
                if(offer.OfferType == OfferType.Auction && offer.Bids.Count > 0)
                {
                    offer.State = OfferState.Finished;
                    var bid = offer.Bids.OrderByDescending(x => x.Value).FirstOrDefault();

                    var offerWithNullDelivery = new OfferAndDeliveryMethod
                    {
                        Offer = offer
                    };

                    _context.OffersAndDeliveryMethods.Add(offerWithNullDelivery);

                    var order = new Order
                    {
                        OrderStatus = OrderStatus.AwaitingForPayment,
                        Customer = bid.Bidder,
                        OfferWithDelivery = offerWithNullDelivery,
                        FullPrice = bid.Value
                    };

                    _context.Orders.Add(order);
                }
                else
                {
                    offer.State = OfferState.Outdated;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<OfferWithBaseDataDTO>> GetUserAciveBidOffers(long userId, PaginationProperties paginationProperties)
        {
            var offers = _context.Bids
                .Include(x => x.Bidder)
                .Include(x => x.Offer).ThenInclude(x => x.Images)
                .Where(x => x.Bidder.Id == userId && x.Offer.State == OfferState.Awaiting && x.Offer.IsHidden == false)
                .Select(x => x.Offer);
            offers = paginationProperties.OrderBy switch
            {
                "price_asc" => offers.OrderBy(x => x.PriceForOneProduct),
                "price_desc" => offers.OrderByDescending(x => x.PriceForOneProduct),
                "end_date_asc" => offers.OrderBy(x => x.EndDate),
                "end_date_desc" => offers.OrderByDescending(x => x.EndDate),
                "creation_asc" => offers.OrderBy(x => x.Created),
                "creation_desc" => offers.OrderByDescending(x => x.Created),
                _ => offers.OrderBy(x => x.Created)
            };

            return await offers.ProjectTo<OfferWithBaseDataDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync<OfferWithBaseDataDTO>(paginationProperties.PageIndex, paginationProperties.PageSize);
        }
    }
}
