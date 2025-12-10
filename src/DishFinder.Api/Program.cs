using DishFinder.Api.Middlewares;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Application.Mapping;
using DishFinder.Application.Services;
using DishFinder.Application.Validators;
using DishFinder.Infrastructure.Auth;
using DishFinder.Infrastructure.Persistence;
using DishFinder.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IDishRepository, DishRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<IBookmarkRepository, BookmarkRepository>();
builder.Services.AddScoped<IOwnerRequestRepository, OwnerRequestRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IBookmarkService, BookmarkService>();
builder.Services.AddScoped<IOwnerRequestService, OwnerRequestService>();
builder.Services.AddScoped<IAdminReviewService, AdminReviewService>();
builder.Services.AddScoped<IAdminPhotoService, AdminPhotoService>();
builder.Services.AddScoped<IAdminOwnerRequestService, AdminOwnerRequestService>();
builder.Services.AddScoped<IAdminDashboardService, AdminDahsboardService>();
builder.Services.AddScoped<IOwnerRestaurantService, OwnerRestaurantService>();
builder.Services.AddScoped<IOwnerMenuService, OwnerMenuService>();
builder.Services.AddScoped<IOwnerReviewService, OwnerReviewService>();

builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddSingleton<IPasswordHasher<DishFinder.Domain.Entities.User>, PasswordHasher<DishFinder.Domain.Entities.User>>();

builder.Services.AddCors(cors => cors.AddDefaultPolicy(policy => 
    policy.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .WithExposedHeaders("Content-Disposition")));


builder.Services.AddAutoMapper(typeof(DomainProfile));
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});


builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DishFinder API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
