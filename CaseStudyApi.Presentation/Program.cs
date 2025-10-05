using CaseStudyApi.BusinessLogic.Interfaces;
using CaseStudyApi.BusinessLogic.Interfaces.Authentication;
using CaseStudyApi.BusinessLogic.Interfaces.Product;
using CaseStudyApi.BusinessLogic.Interfaces.ProductImageFile;
using CaseStudyApi.BusinessLogic.Services;
using CaseStudyApi.BusinessLogic.Services.Authentication;
using CaseStudyApi.BusinessLogic.Services.Product;
using CaseStudyApi.BusinessLogic.Services.User;
using CaseStudyApi.BusinessLogic.Validators.ProductValidator;
using CaseStudyApi.DataAccess;
using CaseStudyApi.DataAccess.Interfaces;
using CaseStudyApi.DataAccess.Interfaces.User;
using CaseStudyApi.DataAccess.Repositories;
using CaseStudyApi.Domain.Entities.Identity;
using CaseStudyApi.Presentation.Interfaces;
using CaseStudyApi.Presentation.MiddleWares;
using CaseStudyApi.Presentation.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<ProductValidator>());
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:3000", "https://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));
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
builder.Services.AddScoped<ITokenHandler, CaseStudyApi.BusinessLogic.Services.TokenHandler>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
        };
    });
var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleWare>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
