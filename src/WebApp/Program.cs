using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

const string PROFILE_LOCAL = "local";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

if (builder.Environment.EnvironmentName == PROFILE_LOCAL)
    builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddDbContext<IUnitOfWork, SQLiteContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IWorkstationRepository, WorkstationRepository>();
builder.Services.AddScoped<WorkstationService>();
builder.Services.AddScoped<INonPaperMediaRepository, NonPaperMediaRepository>();
builder.Services.AddScoped<NonPaperMediaService>();


var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == PROFILE_LOCAL)
{
    app.MapOpenApi();
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
