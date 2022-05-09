using Abstractions;
using Bl;
using Dal;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Web.Automappings;
using Web.Infrastructure;
using Web.Models;
using Web.Models.Validators;

var customLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

const string corsOptionsName = "customCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsOptionsName,
                      policy =>
                      {
                          policy.WithOrigins(builder.Configuration.GetValue<string>("ClientHost"))
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                      });
});

builder.Services.AddControllers()
    .AddFluentValidation();

builder.Services.AddResponseCaching();

builder.Services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();


var connectionString = builder.Configuration.GetConnectionString("database");
var databaseRoot = new InMemoryDatabaseRoot();
builder.Services.AddDbContextPool<RegistryContext>(options =>
{
    options
        .UseLoggerFactory(customLoggerFactory)
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging();

    if (builder.Configuration.GetValue<bool>("UseInMemoryDb"))
    {
        options.UseInMemoryDatabase("InMemoryDb", databaseRoot, b => b.EnableNullChecks(false));
        options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    }
    else
    {
        options.UseSqlServer(connectionString, o => { o.CommandTimeout(300).MigrationsAssembly("Dal"); });
    }
}); 

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IProvinceService, ProvinceService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddAutoMapper(typeof(ModelsProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<RegistryContext>())
{
    if (context.Database.IsInMemory())
    {
        await context.Database.EnsureCreatedAsync();
    }
    else
    {
        await context.Database.MigrateAsync();
    }
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCors(corsOptionsName);

app.UseResponseCaching();

app.MapControllers();

app.Run();
