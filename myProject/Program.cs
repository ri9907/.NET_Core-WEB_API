using Microsoft.EntityFrameworkCore;
using myProject;
using Repositories;
using Services;
using NLog.Web;
using Entities;
using MyFirstWebApiSite;
using Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ComputersContext>(option => option.UseSqlServer("Data Source=srv2\\PUPILS;Initial Catalog=Computers;Trusted_Connection=True;TrustServerCertificate=True"));
builder.Services.AddControllers();
builder.Services.AddTransient<IUserRepositories, UserRepositories>();
builder.Services.AddTransient<IUserServieces, UserServieces>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductServieces, ProductServieces>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderServieces, OrderServieces>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICategoryServieces, CategoryServieces>();
builder.Services.AddTransient<IRatingServieces, RatingServieces>();
builder.Services.AddTransient<IRatingRepository, RatingRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseNLog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseRatingMiddleware();

app.UseErrorMiddleware();

app.MapControllers();

app.Run();
