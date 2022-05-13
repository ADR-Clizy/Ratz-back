using DatabaseConnection;
using Microsoft.EntityFrameworkCore;
using Ratz_API.Extensions;
using Ratz_API.QrCodeAggregate.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
AddJWTTokenServicesExtensions.AddJWTTokenServices(builder.Services, builder.Configuration);
builder.Services.AddScoped<IUserRepository, SQLUserRepository>();
builder.Services.AddScoped<IQrCodeRepository, SqlQrCodeRepository>();
builder.Services.AddDbContextPool<RatzDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RatzDbConnection")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
