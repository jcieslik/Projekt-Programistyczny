﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Bid> Bids { get; set; }
        DbSet<Brand> Brands { get; set; }
        DbSet<ProductCategory> Categories { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<ProductImage> Images { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Offer> Offers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Province> Provinces { get; set; }
        DbSet<ProductRate> Rates { get; set; }
        DbSet<User> Users { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}