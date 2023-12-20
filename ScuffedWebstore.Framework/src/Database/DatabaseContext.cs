using Microsoft.EntityFrameworkCore;
using Npgsql;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Types;

namespace ScuffedWebstore.Framework.src.Database;
public class DatabaseContext : DbContext
{
    private readonly IConfiguration _config;

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }

    static DatabaseContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DatabaseContext(DbContextOptions options, IConfiguration config) : base(options)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        NpgsqlDataSourceBuilder dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("LocalDb"));
        dataSourceBuilder.MapEnum<UserRole>();
        dataSourceBuilder.MapEnum<OrderStatus>();
        NpgsqlDataSource dataSource = dataSourceBuilder.Build();

        optionsBuilder
            .UseNpgsql(dataSource)
            .UseSnakeCaseNamingConvention()
            .AddInterceptors(new TimestampInterceptor());
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<UserRole>();
        modelBuilder.Entity<User>(entity => entity.Property(e => e.Role).HasColumnType("user_role"));
        modelBuilder.Entity<User>(entity => entity.HasIndex(e => e.Email).IsUnique());

        modelBuilder.HasPostgresEnum<OrderStatus>();
        modelBuilder.Entity<Order>(entity => entity.Property(e => e.Status).HasColumnType("order_status"));

        modelBuilder.Entity<Product>().ToTable(p => p.HasCheckConstraint("CK_Product_Price_Positive", "price>=0"));

        modelBuilder.Entity<OrderProduct>().HasKey(entity => new { entity.ProductID, entity.OrderID });
        modelBuilder.Entity<OrderProduct>().ToTable(p => p.HasCheckConstraint("CK_OrderProduct_Price_Positive", "price>=0"));
        modelBuilder.Entity<OrderProduct>().ToTable(p => p.HasCheckConstraint("CK_OrderProduct_Amount_Positive", "amount>=0"));

        modelBuilder.Entity<Review>().HasKey(entity => new { entity.ProductID, entity.UserID });

        base.OnModelCreating(modelBuilder);
    }
}