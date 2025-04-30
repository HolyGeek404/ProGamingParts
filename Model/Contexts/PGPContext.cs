using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Model.Entities;

namespace Model.Contexts;

public partial class PGPContext(DbContextOptions<PGPContext> options) : DbContext(options)
{
    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Cooler> Coolers { get; set; }

    public virtual DbSet<Cpu> Cpus { get; set; }

    public virtual DbSet<Gpu> Gpus { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<DeliveryAddress> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(x => x.CartId);

            entity.ToTable("Cart");
            entity.Property(e => e.Type).HasMaxLength(10);
        });

        modelBuilder.Entity<Cooler>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.ToTable("Cooler");

            entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
            entity.Property(e => e.BearingType).HasMaxLength(50);
            entity.Property(e => e.Compatibility).HasMaxLength(100);
            entity.Property(e => e.Connector).HasMaxLength(50);
            entity.Property(e => e.CoolerType).HasMaxLength(50);
            entity.Property(e => e.Depth).HasMaxLength(50);
            entity.Property(e => e.FanSize).HasMaxLength(50);
            entity.Property(e => e.Fans).HasMaxLength(50);
            entity.Property(e => e.HeatPipes).HasMaxLength(50);
            entity.Property(e => e.Height).HasMaxLength(50);
            entity.Property(e => e.Highlight).HasMaxLength(50);
            entity.Property(e => e.Manufacture).HasMaxLength(50);
            entity.Property(e => e.Mtbflifetime)
                .HasMaxLength(50)
                .HasColumnName("MTBFLifetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasMaxLength(50);
            entity.Property(e => e.ProducentCode).HasMaxLength(50);
            entity.Property(e => e.Rmp)
                .HasMaxLength(50)
                .HasColumnName("RMP");
            entity.Property(e => e.Rpmcontroll)
                .HasMaxLength(50)
                .HasColumnName("RPMControll");
            entity.Property(e => e.Size).HasMaxLength(50);
            entity.Property(e => e.SupplyCurrent).HasMaxLength(50);
            entity.Property(e => e.SupplyVoltage).HasMaxLength(50);
            entity.Property(e => e.Team).HasMaxLength(50);
            entity.Property(e => e.Warranty).HasMaxLength(50);
            entity.Property(e => e.Weight).HasMaxLength(50);
            entity.Property(e => e.Width).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(15)
                .HasDefaultValue("COOLER")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<Cpu>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
            entity.Property(e => e.AdditionalInfo).HasMaxLength(150);
            entity.Property(e => e.Architecture).HasMaxLength(50);
            entity.Property(e => e.CacheMemory).HasMaxLength(10);
            entity.Property(e => e.Cores).HasMaxLength(20);
            entity.Property(e => e.Familiy).HasMaxLength(50);
            entity.Property(e => e.Frequency).HasMaxLength(50);
            entity.Property(e => e.IncludedCooler).HasMaxLength(10);
            entity.Property(e => e.IntegredGpu).HasMaxLength(15);
            entity.Property(e => e.IntegredGpuModel).HasMaxLength(50);
            entity.Property(e => e.Litography).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ProductImg).HasMaxLength(500);
            entity.Property(e => e.RecomendedChipset).HasMaxLength(20);
            entity.Property(e => e.Series).HasMaxLength(50);
            entity.Property(e => e.Socket).HasMaxLength(30);
            entity.Property(e => e.SupportedChipsets).HasMaxLength(50);
            entity.Property(e => e.SupportedRam).HasMaxLength(100);
            entity.Property(e => e.Tdp)
                .HasMaxLength(15)
                .HasColumnName("TDP");
            entity.Property(e => e.Team).HasMaxLength(10);
            entity.Property(e => e.Threads).HasMaxLength(50);
            entity.Property(e => e.UnlockedMultipler).HasMaxLength(15);
            entity.Property(e => e.Warranty).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(15)
                .HasDefaultValue("CPU")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<Gpu>(entity =>
        {
            entity.Property(e => e.ProductId).ValueGeneratedOnAdd();

            entity.ToTable("GPU");
            entity.Property(e => e.CoolingType).HasMaxLength(50);
            entity.Property(e => e.CoreRatio).HasMaxLength(50);
            entity.Property(e => e.CoresNumber).HasMaxLength(50);
            entity.Property(e => e.GpuProcessorLine).HasMaxLength(50);
            entity.Property(e => e.GpuProcessorName).HasMaxLength(50);
            entity.Property(e => e.Height).HasMaxLength(50);
            entity.Property(e => e.Length).HasMaxLength(50);
            entity.Property(e => e.Manufacturer).HasMaxLength(50);
            entity.Property(e => e.MemoryBus).HasMaxLength(50);
            entity.Property(e => e.MemoryRatio).HasMaxLength(50);
            entity.Property(e => e.MemorySize).HasMaxLength(50);
            entity.Property(e => e.MemoryType).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.OutputsType).HasMaxLength(100);
            entity.Property(e => e.PcieType)
                .HasMaxLength(50)
                .HasColumnName("PCIeType");
            entity.Property(e => e.Pgpcode)
                .HasMaxLength(50)
                .HasColumnName("PGPCode");
            entity.Property(e => e.PowerConnector).HasMaxLength(50);
            entity.Property(e => e.ProducentCode).HasMaxLength(100);
            entity.Property(e => e.ProductImg).HasMaxLength(500);
            entity.Property(e => e.RecommendedPsupower)
                .HasMaxLength(50)
                .HasColumnName("RecommendedPSUPower");
            entity.Property(e => e.SupportedLibraries).HasMaxLength(100);
            entity.Property(e => e.Team).HasMaxLength(50);
            entity.Property(e => e.Warranty).HasMaxLength(50);
            entity.Property(e => e.Width).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(15)
                .HasDefaultValue("GPU")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

       

        modelBuilder.Entity<User>()
            .HasMany(u => u.CartList)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);


        modelBuilder.Entity<DeliveryAddress>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Street).HasMaxLength(100); 
            entity.Property(e => e.BlockNumber).HasMaxLength(10);
            entity.Property(e => e.ApartmentNumber).HasMaxLength(10);
            entity.Property(e => e.Email).HasMaxLength(100).IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.HasMany(od => od.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            entity.Property(e => e.Block).HasMaxLength(10);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Flat).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Pay).HasMaxLength(10);
            entity.Property(e => e.PostalCode).HasMaxLength(50);
            entity.Property(e => e.Shipping).HasMaxLength(10);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.UserId).HasMaxLength(50);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(x => x.OrderDetailId);

            entity.Property(e => e.Type).HasMaxLength(10);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasOne(u => u.DeliveryAddress)
                .WithOne(d => d.User).HasForeignKey<DeliveryAddress>(d => d.UserId);

            entity.HasMany(u => u.CartList)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            entity.HasMany(u => u.OrderList)
                .WithOne(u => u.User)
                .HasForeignKey(o => o.UserId);

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(10);
        });

    }
}