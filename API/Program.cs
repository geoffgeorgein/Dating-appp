using dating_app.Data;
using dating_app.Interfaces;
using dating_app.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();
builder.Services.AddScoped<ITokenService,TokenService>();

var app = builder.Build();

app.UseCors(x=>x.AllowAnyHeader().AllowAnyHeader().AllowAnyMethod()
.WithOrigins("http://localhost:4200","https://localhost:4200")
);

// Configure the HTTP request pipeline...
// if (app.Environment.IsDevelopment())
// {
   
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
