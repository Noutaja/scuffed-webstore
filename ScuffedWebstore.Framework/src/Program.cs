using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Types;
using ScuffedWebstore.Framework.Middleware;
using ScuffedWebstore.Framework.src.Authorization;
using ScuffedWebstore.Framework.src.Database;
using ScuffedWebstore.Framework.src.Repositories;
using ScuffedWebstore.Framework.src.Services;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.Services;
using ScuffedWebstore.Service.src.Shared;
using Swashbuckle.AspNetCore.Filters;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Bearer token authentication",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

//Add
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAddressRepo, AddressRepo>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();

builder.Services.AddScoped<IImageRepo, ImageRepo>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddSingleton<IAuthorizationHandler, AddressAdminOrOwnerHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, OrderAdminOrOwnerHandler>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    }
);

builder.Services.AddAuthorization(policy =>
{
    policy.AddPolicy("AdminOrOwner", policy => policy.Requirements.Add(new AdminOrOwnerRequirement()));
    policy.AddPolicy("Admin", policy => policy.RequireRole(ClaimTypes.Role, "Admin"));
    policy.AddPolicy("Customer", policy => policy.RequireRole(ClaimTypes.Role, "Customer"));
});

builder.Services.AddTransient<ExceptionHandlerMiddleware>();

NpgsqlDataSourceBuilder dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("AzureDb"));
dataSourceBuilder.MapEnum<UserRole>();
dataSourceBuilder.MapEnum<OrderStatus>();
NpgsqlDataSource dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(dataSource)
    .UseSnakeCaseNamingConvention()
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors()
    .AddInterceptors(TimestampInterceptor.Instance));

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

WebApplication app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});


app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseCors();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    opt.RoutePrefix = string.Empty;
});

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
