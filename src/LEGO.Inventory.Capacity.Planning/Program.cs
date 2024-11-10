using LEGO.Inventory.Capacity.Planning.Services;
using LEGO.Inventory.Capacity.Planning.Services.Interfaces;
using LEGO.Inventory.Capacity.Planning.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IGoodsReceiptService, GoodsReceiptService>();
builder.Services.AddTransient<ISalesOrderService, SalesOrderService>();
builder.Services.AddTransient<IStockTransportOrderService, StockTransportOrderService>();
builder.Services.AddTransient<IPreparationService, PreparationService>();
builder.Services.AddTransient<IRegionalDistributionCenterService, RegionalDistributionCenterService>();
builder.Services.AddSingleton<IStorage, Storage>();
var authConf = builder.Configuration.GetSection("AuthConfig").Get<AuthConfiguration>() ?? throw new Exception("cannot find auth options!");

var tokenHandler = new JwtTokenHandler(authConf.TokenHeader, authConf.Audience); 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApiDbContext>(options=> options.UseSqlite(connectionString));
// builder.WebHost.UseUrls("http://*:5100");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            configureOptions: options =>
            {
                // change the values in appsettings.json
                options.Authority = authConf.Authority;
                options.Events = tokenHandler;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            }
        );
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
