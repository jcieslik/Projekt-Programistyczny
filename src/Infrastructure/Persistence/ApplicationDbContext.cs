using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly ICurrentUserService _currentUserService;

        public DbSet<Bid> Bids { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<ProductImage> Images { get; set; }
        public DbSet<ProductRate> Rates { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wish> Wishes { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTimeService,
            ICurrentUserService currentUserService
            ) : base(options)
        {
            _dateTimeService = dateTimeService;
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.Id;
                        entry.Entity.Created = _dateTimeService.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = _currentUserService.Id;
                        entry.Entity.LastModified = _dateTimeService.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
