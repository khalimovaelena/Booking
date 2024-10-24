using Booking.Repositories;
using Booking.Repositories.Interfaces;
using Booking.Services;
using Booking.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//TODO: For real APP please add DependencyInjection class
builder.Services.AddScoped<ICustomersRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomersService, CustomersService>();

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

var customersService = app.Services.CreateScope().ServiceProvider.GetRequiredService<ICustomersService>();

app.Logger.LogInformation($"Customers list: {customersService?.GetCustomers()}");//TODO: log Customers values

app.Run();

