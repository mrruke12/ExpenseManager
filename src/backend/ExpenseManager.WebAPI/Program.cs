using ExpenseManager.Bot;
using ExpenseManager.Core.Interfaces;
using ExpenseManager.Core.Interfaces.Builders;
using ExpenseManager.Core.Interfaces.Repositories;
using ExpenseManager.Core.Services;
using ExpenseManager.Infrastructure.Persistence;
using ExpenseManager.Infrastructure.Persistence.Builders;
using ExpenseManager.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowVueDevClient", policy => {
        policy.WithOrigins(builder.Configuration["Frontend:Url"])
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// Добавляем поддержку sqlite
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Инициализируем логгер по настройкам из конфига
Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

// Добавляем логгер
builder.Host.UseSerilog();

Log.Information("Проект запущен");

// Включаем телеграмм бота
builder.Services.AddHostedService<BotManager>(
    provider => new BotManager(builder.Configuration["API:TGAPITOKEN"], provider)
);

builder.Services.AddScoped<IAppUserRepository, AppUserRepository>(provider => new AppUserRepository(provider.GetRequiredService<AppDbContext>()));
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>(provider => new BankAccountRepository(provider.GetRequiredService<AppDbContext>()));
builder.Services.AddScoped<IBankTransferRepository, BankTransferRepository>(provider => new BankTransferRepository(provider.GetRequiredService<AppDbContext>()));
builder.Services.AddScoped<IBankTransferCategoryRepository, BankTransferCategoryRepository>(provider => new BankTransferCategoryRepository(provider.GetRequiredService<AppDbContext>()));

builder.Services.AddTransient<IAppUserBuilder, AppUserBuilder>();
builder.Services.AddTransient<IBankAccountBuilder, BankAccountBuilder>();
builder.Services.AddTransient<IBankTransferBuilder, BankTransferBuilder>();
builder.Services.AddTransient<IBankTransferCategoryBuilder, BankTransferCategoryBuilder>();

builder.Services.AddScoped<ITokenService, TokenService>(provider => new TokenService(builder.Configuration));
builder.Services.AddSingleton<LocalizationService>();


// swagger for development
//builder.Services.AddSwaggerGen(c => {
//    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
//        Description = "JWT Authorization header using the Bearer scheme",
//        Type = SecuritySchemeType.Http,
//        Scheme = "bearer"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            Array.Empty<string>()
//        }
//    });

//    c.MapType<IFormFile>(() => new OpenApiSchema {
//        Type = "string",
//        Format = "binary"
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    //app.MapOpenApi();

    //app.UseSwagger();
    //app.UseSwaggerUI(c => {
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    //});
}

app.UseRouting();

app.UseCors("AllowVueDevClient");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();