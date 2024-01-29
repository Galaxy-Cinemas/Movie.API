using Microsoft.Extensions.Configuration;
using Galaxi.Movie.Persistence;
using System.Reflection;
using MediatR;
using Galaxi.Movie.Domain.Profiles;
using Galaxi.Movie.Persistence.Persistence;
using Microsoft.EntityFrameworkCore;
using Galaxi.Movie.Persistence.Repositorys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var service = builder.Services.BuildServiceProvider();
var configuration = service.GetService<IConfiguration>();

//var MyAllowSpecificOrigins = "_corsMovieApiOriginacion";

builder.Services.AddInfrastructure(configuration);
builder.Services.AddAutoMapper(typeof(MovieProfile).Assembly);
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddMediatR(Assembly.Load("Galaxi.Movie.Domain"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

//builder.Services.AddCors(p => p.AddPolicy(MyAllowSpecificOrigins, build =>
//{
//    build.WithOrigins("*").WithMethods("PUT", "POST", "GET").AllowAnyHeader();
//}));

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy",
//        builder => builder.WithOrigins(new[]
//        {
//                        "http://localhost:4200",
//                        "http://localhost:50928"
//        })
//        .AllowAnyMethod()
//        .AllowAnyHeader()
//        .AllowCredentials());
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("*")
        .AllowAnyMethod()
        .AllowAnyHeader());
        //.AllowCredentials());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

ApplyMigration();

app.MapControllers();

app.Run();

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<MovieContextDb>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
