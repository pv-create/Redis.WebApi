using Redis.Db.CasheRepository;
using Redis.Db.Repositories;
using Redis.Services.Contracts;
using Redis.Services.Data;
using Redis.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string databaseConnectionString = builder.Configuration["ConnectionStrings:SqlConnection"];

string casheConnectionString = builder.Configuration["CacheSettings:RedisCache"];

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = casheConnectionString;
});



builder.Services.AddTransient<UserRepository>(provider => new UserRepository(databaseConnectionString));
builder.Services.AddTransient<IUserRepository, UserCasheRepository>();
builder.Services.AddTransient<IUserService, UserService>();


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
