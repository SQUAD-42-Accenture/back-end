using Microsoft.EntityFrameworkCore;
using SERVPRO.API;
using SERVPRO.API.User.Repository;
using SERVPRO.API.UserRepository;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inje��o de depend�ncia
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
