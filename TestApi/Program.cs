using Application.Interfaces;
using DAL.Data;
using DAL.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using TestApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddControllers();

services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<IBookRepository, BookRepository>();

services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    context.Database.Migrate();

    if (!context.Books.Any())
    {
        var book1 = new BookEntity
            { Name = "Book 1", PublicationDate = DateTime.SpecifyKind(new DateTime(2021, 11, 11), DateTimeKind.Utc) };
        var book2 = new BookEntity
            { Name = "Book 2", PublicationDate = DateTime.SpecifyKind(new DateTime(2022, 12, 22), DateTimeKind.Utc) };
        
        context.Books.AddRange(book1, book2);
        context.SaveChanges();
        
        var order1 = new OrderEntity { OrderDate = DateTime.UtcNow };
        var order2 = new OrderEntity { OrderDate = DateTime.SpecifyKind(new DateTime(2023, 2, 25), DateTimeKind.Utc) };
        
        context.Orders.AddRange(order1, order2);
        context.SaveChanges();

        context.OrderBooks.AddRange(
            new OrderBooksEntity { OrderId = order1.Id, BookId = book1.Id, AmountBook = 5 },
            new OrderBooksEntity { OrderId = order1.Id, BookId = book2.Id, AmountBook = 7 },
            new OrderBooksEntity { OrderId = order2.Id, BookId = book2.Id, AmountBook = 10 }
        );
        
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
