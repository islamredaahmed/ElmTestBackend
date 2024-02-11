using ElmTest.Controllers;
using Microsoft.EntityFrameworkCore;
using Migrations.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Context
builder.Services.AddDbContext<ElmTestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddControllers();
builder.Services.AddScoped<IBook, BookService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

// Seed Data
await app.Services.Seed();

app.Run();
