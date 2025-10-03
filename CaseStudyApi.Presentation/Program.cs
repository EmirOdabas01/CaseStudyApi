using CaseStudyApi.BusinessLogic.Interfaces.Product;
using CaseStudyApi.BusinessLogic.Services.Product;
using CaseStudyApi.DataAccess;
using CaseStudyApi.DataAccess.Interfaces;
using CaseStudyApi.DataAccess.Repositories;
using CaseStudyApi.Presentation.MiddleWares;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using CaseStudyApi.BusinessLogic.Validators.ProductValidator;
using CaseStudyApi.BusinessLogic.Interfaces;
using CaseStudyApi.BusinessLogic.Services;
using CaseStudyApi.BusinessLogic.Interfaces.ProductImageFile;
using CaseStudyApi.Presentation.Interfaces;
using CaseStudyApi.Presentation.Services;
using CaseStudyApi.Domain.Entities.Identity;
using CaseStudyApi.DataAccess.Interfaces.User;
using CaseStudyApi.BusinessLogic.Services.User;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<ProductValidator>());
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CaseStudyDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<CaseStudyDbContext>();
builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
builder.Services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
builder.Services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductImageFileService, ProductImageFileService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IUserService, UserService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleWare>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
