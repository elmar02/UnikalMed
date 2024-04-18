using Business.DependencyResolver;
using Business.Filters;
using Business.Validation.CategoryValidation;
using Core.DependencyResolver;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBusinessService();
builder.Services.AddCoreService();


// Fluent Validation Registration
// Add services to the container.
#region Fluent Validation Registration add services to the container.
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilters>())
    .AddFluentValidation(configuration =>
    {
        configuration.RegisterValidatorsFromAssemblyContaining<CreateCategoryValidation>();
        configuration.DisableDataAnnotationsValidation = true;
        configuration.ValidatorOptions.LanguageManager.Culture = new CultureInfo("az-AZ");
    })
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
#endregion


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "UnikalMedApp", Version = "v1", Description = "UnikalMed API swagger client." });
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Example: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\\"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }

    });
});

// JWT Auth
#region JWT Auth
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
            expires != null ? expires > DateTime.UtcNow.AddHours(4) : false,

        NameClaimType = ClaimTypes.Email
    };
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
