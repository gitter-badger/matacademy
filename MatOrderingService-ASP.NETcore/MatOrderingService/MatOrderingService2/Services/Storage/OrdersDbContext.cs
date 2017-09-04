using MatOrderingService2.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatOrderingService2.Services.Storage
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options) {}
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapOrders(modelBuilder.Entity<Order>());
            MapProducts(modelBuilder.Entity<Product>());
            MapOrderItems(modelBuilder.Entity<OrderItem>());
        }

        private void MapOrders(EntityTypeBuilder<Order> builder)
        {

            builder.ToTable("Orders");

            builder.HasMany(m => m.OrderItems)
                .WithOne(o => o.Order)
                .HasForeignKey(f => f.OrderId)
                .HasPrincipalKey(p => p.Id);

            builder.HasIndex(b => b.OrderCode).IsUnique();

            builder.Property(p => p.Id)
                    .IsRequired();

            builder.Property(p => p.Status)
                    .IsRequired()
                    .HasDefaultValue(OrderStatus.New);

            builder.Property(p => p.CreatorId)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(p => p.IsDeleted)
                    .IsRequired();

            builder.Property(p => p.CreateDate)
                    .IsRequired()
                    .ForSqlServerHasDefaultValueSql("GETDATE()");

            builder.Property(p => p.OrderCode)
                    .IsRequired();

            builder.HasKey(p => p.Id);
        }

        private void MapOrderItems(EntityTypeBuilder<OrderItem> builder)
        {

            builder.ToTable("OrderItems");

            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.Count)
                .IsRequired();

            builder.HasOne(o => o.Order)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(f => f.OrderId)
                .HasPrincipalKey(p => p.Id);

            builder.HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(f => f.ProductId)
                .HasPrincipalKey(p => p.Id);

            builder.HasKey(p => p.Id);
        }

        private void MapProducts(EntityTypeBuilder<Product> builder)
        {

            builder.ToTable("Products");

            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.Code)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasKey(p => p.Id);
        }
    }
}
