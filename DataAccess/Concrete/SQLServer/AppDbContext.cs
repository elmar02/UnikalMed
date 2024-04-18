using Core.Entities.Concrete;
using Entities.Common;
using Entities.Concrete;
using Entities.Concrete.AdvertEntities;
using Entities.Concrete.BlogEntities;
using Entities.Concrete.CategoryEntities;
using Entities.Concrete.HeaderEntities;
using Entities.Concrete.PartnerEntities;
using Entities.Concrete.ProductEntities;
using Entities.Concrete.ReferenceEntities;
using Entities.Concrete.ServiceEntities;
using Entities.Concrete.StaffEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.PostgresSQL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=UnikalMedDB; Trusted_Connection=True; MultipleActiveResultSets=True; TrustServerCertificate=True;");
        }

        #region Category Sets
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryLanguage> CategoryLanguages { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<SubCategoryLanguage> SubCategoryLanguages { get; set; }
        #endregion
        #region Header Sets
        public DbSet<Header> Headers { get; set; }
        public DbSet<HeaderSpecification> HeaderSpecifications { get; set; }
        public DbSet<HeaderLanguage> HeaderLanguages { get; set; }
        #endregion
        #region Product Sets
        public DbSet<PictureProduct> PictureProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLanguage> ProductLanguages { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        #endregion
        #region Brand Sets
        public DbSet<Brand> Brands { get; set; }
        #endregion
        #region Partner Sets
        public DbSet<Partner> Partners { get; set; }
        #endregion
        #region Advert Sets
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<AdvertLanguage> AdvertLanguages { get; set; }
        #endregion
        #region Service Sets
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceLanguage> ServiceLanguages { get; set; }
        #endregion
        #region Staff Sets
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffLanguage> StaffLanguages { get; set; }
        #endregion
        #region Reference Sets
        public DbSet<Reference> References { get; set; }
        public DbSet<ReferenceLanguage> ReferenceLanguages { get; set; }
        #endregion
        #region Blog Sets
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogLanguage> BlogLanguages { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().ToTable("Users");
            builder.Entity<AppRole>().ToTable("Roles");

            #region Category Deletetions
            builder.Entity<Category>()
            .HasMany(c => c.Languages)
            .WithOne(cl => cl.Category)
            .HasForeignKey(cl => cl.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(sc => sc.Category)
                .HasForeignKey(sc => sc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region SubCategory Deletetions
            builder.Entity<SubCategory>()
            .HasMany(sc => sc.Languages)
            .WithOne(scl => scl.SubCategory)
            .HasForeignKey(scl => scl.SubCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SubCategory>()
                .HasMany(sc => sc.Products)
                .WithOne(p => p.SubCategory)
                .HasForeignKey(p => p.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region Product Deletetions
            builder.Entity<Product>()
            .HasMany(sc => sc.Languages)
            .WithOne(scl => scl.Product)
            .HasForeignKey(scl => scl.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>()
            .HasMany(sc => sc.PictureProducts)
            .WithOne(scl => scl.Product)
            .HasForeignKey(scl => scl.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>()
            .HasMany(sc => sc.Headers)
            .WithOne(scl => scl.Product)
            .HasForeignKey(scl => scl.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProductLanguage>()
            .HasMany(sc => sc.Specifications)
            .WithOne(scl => scl.Language)
            .HasForeignKey(scl => scl.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region Header Deletetions
            builder.Entity<Header>()
            .HasMany(sc => sc.Languages)
            .WithOne(scl => scl.Header)
            .HasForeignKey(scl => scl.HeaderId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<HeaderLanguage>()
            .HasMany(sc => sc.Specifications)
            .WithOne(scl => scl.Language)
            .HasForeignKey(scl => scl.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region Advert Deletetions
            builder.Entity<Advert>()
            .HasMany(sc => sc.Languages)
            .WithOne(scl => scl.Advert)
            .HasForeignKey(scl => scl.AdvertId)
            .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region Service Deletetions
            builder.Entity<Service>()
            .HasMany(sc => sc.Languages)
            .WithOne(scl => scl.Service)
            .HasForeignKey(scl => scl.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region Staff Deletetions
            builder.Entity<Staff>()
            .HasMany(sc => sc.Languages)
            .WithOne(scl => scl.Staff)
            .HasForeignKey(scl => scl.StaffId)
            .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region Reference Deletetions
            builder.Entity<Reference>()
            .HasMany(sc => sc.Languages)
            .WithOne(scl => scl.Reference)
            .HasForeignKey(scl => scl.ReferenceId)
            .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region Blog Deletetions
            builder.Entity<Blog>()
            .HasMany(sc => sc.Languages)
            .WithOne(scl => scl.Blog)
            .HasForeignKey(scl => scl.BlogId)
            .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity)
                .OfType<BaseEntity>();

            foreach (var entity in entities)
            {
                if (entity.CreatedDate == default)
                {
                    entity.CreatedDate = DateTime.UtcNow;
                }

                entity.UpdatedDate = DateTime.UtcNow;
            }
        }
    }
}
