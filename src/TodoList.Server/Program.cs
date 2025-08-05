using Microsoft.EntityFrameworkCore;
using TodoList.Data.Contexts;
using TodoList.Data.Repositories;
using TodoList.Server.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationContext")));
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddAutoMapper(cfg => { cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"]; }, typeof(AutoMapperProfiles));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
