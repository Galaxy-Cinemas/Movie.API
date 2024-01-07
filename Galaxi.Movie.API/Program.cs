using Microsoft.Extensions.Configuration;
using Galaxi.Movie.Persistence;
using System.Reflection;
using MediatR;
using Galaxi.Movie.Domain.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var service = builder.Services.BuildServiceProvider();
var configuration = service.GetService<IConfiguration>();


builder.Services.AddInfrastructure(configuration);
builder.Services.AddAutoMapper(typeof(MovieDtosProfile).Assembly);
builder.Services.AddMediatR(Assembly.Load("Galaxi.Movie.Domain"));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
