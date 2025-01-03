using Galaxi.Movie.Persistence;
using System.Reflection;
using MediatR;
using Galaxi.Movie.Domain.Profiles;
using Microsoft.EntityFrameworkCore;
using Galaxi.Movie.Persistence.Repositorys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using MassTransit;
using Galaxi.Movie.Domain.IntegrationEvents.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var service = builder.Services.BuildServiceProvider();
var configuration = service.GetService<IConfiguration>();

//var MyAllowSpecificOrigins = "_corsMovieApiOriginacion";

builder.Services.AddLogging(logginBuilder =>
{
    //1. Create Config
    var loggerConfig = new LoggerConfiguration()
                           .MinimumLevel.Debug()
                           .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                           .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                           .WriteTo.File
                           (
                                path: "/app/samba/logs/logs-movie-Serilog-.json",
                                formatter: new Serilog.Formatting.Json.JsonFormatter(),
                                rollingInterval: RollingInterval.Day
                           )
                          .WriteTo.Http(builder.Configuration.GetConnectionString("LogStash"), null);

    //2. Create Logger
    var logger = loggerConfig.CreateLogger();

    //3. Inject Service
    logginBuilder.Services.AddSingleton<ILoggerFactory>(
        provider => new SerilogLoggerFactory(logger, dispose: false));

});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CheckAvailableMovieConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(configuration.GetConnectionString("rabbitMqSettingsHost"), h =>
        {
            h.Username(configuration.GetConnectionString("rabbitMqSettingsUsername"));
            h.Password(configuration.GetConnectionString("rabbitMqSettingsPassword"));
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddInfrastructure(configuration);
builder.Services.AddAutoMapper(typeof(MovieProfile).Assembly);
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddMediatR(Assembly.Load("Galaxi.Movie.Domain"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("*")
        .AllowAnyMethod()
        .AllowAnyHeader());
});


// Add Authentication
var secretKey = Encoding.ASCII.GetBytes(
    builder.Configuration.GetValue<string>("SecretKey")
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

