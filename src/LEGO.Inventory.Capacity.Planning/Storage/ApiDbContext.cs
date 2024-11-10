using LEGO.Inventory.Capacity.Planning.Domain;
using LEGO.Inventory.Capacity.Planning.Domain.DistributionCenters;
using LEGO.Inventory.Capacity.Planning.Domain.GoodsMovement;
using LEGO.Inventory.Capacity.Planning.Domain.Orders;
using Microsoft.EntityFrameworkCore;

public class ApiDbContext : DbContext

{

    protected readonly IConfiguration Configuration;

    public ApiDbContext( IConfiguration configuration){
        
        Configuration= configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet <SalesOrder> SalesOrders{get;set;}
    public DbSet<StockTransportOrder> StockTransportOrders {get;set;}
    public DbSet<LocalDistributionCenter> LocalDistributionCenter { get; set; } 
    public DbSet<RegionalDistributionCenter> RegionalDistributionCenter { get; set; } 
    public DbSet<GoodsReceipt> GoodsReceipts {get;set;}


}