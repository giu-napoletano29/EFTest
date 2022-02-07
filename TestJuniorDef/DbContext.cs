using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apitest.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(builder =>
            {
                builder.ToTable("Account");
            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("tUser");
                builder.Property(x => x.AccountId).IsRequired();
                builder.Property(x => x.Name).IsRequired();
                builder.Property(x => x.LastName).IsRequired();

                builder.HasOne<Account>(s => s.Account)
                    .WithOne(g => g.User)
                    .HasForeignKey<User>(x => x.AccountId);
            });


            modelBuilder.Entity<ProductCategory>(builder =>
            {
                builder.ToTable("Product_Category");
                builder.HasKey(o => new { o.ProductId, o.CategoryId });

                builder.HasOne<Product>(s => s.Product)
                    .WithMany(g => g.ProductCategory)
                    .HasForeignKey(s => s.ProductId);

                builder.HasOne<Category>(s => s.Category)
                    .WithMany(g => g.ProductCategory)
                    .HasForeignKey(s => s.CategoryId);

            });

            modelBuilder.Entity<Brand>(builder =>
            {
                builder.ToTable("Brand");
                builder.Property(x => x.AccountId).HasColumnName("AccountId");
                builder.Property(x => x.BrandName).IsRequired();

                builder.HasOne(s => s.Account)
                    .WithOne(x => x.Brand)
                    .HasForeignKey<Brand>(x => x.AccountId);//.HasConstraintName("Brand_AccountId");

            });

            modelBuilder.Entity<Product>(builder =>
            {
                builder.ToTable("Product");
                builder.Property(x => x.BrandId).IsRequired();
                builder.Property(x => x.Name).IsRequired();
                builder.Property(x => x.Price).IsRequired();

                builder.HasOne<Brand>(s => s.Brand)
                    .WithMany(g => g.Products)
                    .HasForeignKey(s => s.BrandId);

            });

            modelBuilder.Entity<InfoRequest>(builder =>
            {
                builder.ToTable("InfoRequest");
                builder.Property(x => x.ProductId).IsRequired();
                builder.Property(x => x.RequestText).IsRequired();
                //builder.Property(x => x.City).IsRequired();
                //builder.Property(x => x.Email).IsRequired();
                builder.Property(x => x.InsertDate).IsRequired();
                builder.Property(x => x.Name).IsRequired();
                builder.Property(x => x.LastName).IsRequired();
                builder.Property(x => x.NationId).IsRequired();
                //builder.Property(x => x.Phone).IsRequired();
                //builder.Property(x => x.PostalCode).IsRequired();

                builder.HasOne<Product>(s => s.Product)
                    .WithMany(g => g.InfoRequests)
                    .HasForeignKey(s => s.ProductId);

                builder.HasOne<User>(s => s.User)
                    .WithMany(g => g.InfoRequests)
                    .HasForeignKey(s => s.UserId);

                builder.HasOne<Nation>(x => x.Nation);

            });

            modelBuilder.Entity<InfoRequestReply>(builder =>
            {
                builder.ToTable("InfoRequestReply");
                builder.Property(x => x.InfoRequestId).IsRequired();
                builder.Property(x => x.AccountId).IsRequired();
                builder.Property(x => x.ReplyText).IsRequired();
                builder.Property(x => x.InsertDate).IsRequired();

                builder.HasOne<InfoRequest>(s => s.InfoRequest)
                    .WithMany(g => g.InfoRequestReply)
                    .HasForeignKey(s => s.InfoRequestId);

                builder.HasOne<Account>(s => s.Account)
                    .WithMany(g => g.InfoRequestReply)
                    .HasForeignKey(s => s.AccountId);

            });

            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Nation>().ToTable("Nation");

        }

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<InfoRequest> InfoRequests { get; set; } = null!;
        public DbSet<InfoRequestReply> InfoRequestReplies { get; set; } = null!;
        public DbSet<Nation> Nations { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

    }
}
