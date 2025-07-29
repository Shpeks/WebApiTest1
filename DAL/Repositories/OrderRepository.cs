using System.Globalization;
using Application.Interfaces;
using Application.Models;
using DAL.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

/// <summary>
/// Репозиторий для работы с заказами
/// Создание заказов и фильтрация по дате
/// </summary>
public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Создаёт новый заказ и добавляет связанные книги с количеством.
    /// </summary>
    /// <returns>Идентификатор созданного заказа.</returns>
    /// <exception cref="ArgumentException">Выбрана книга с некорректным идентификатором или количеством.</exception>
    public async Task<int> CreateAsync(OrderCreateDto orderDto)
    {
        var order = new OrderEntity
        {
            OrderDate = DateTime.UtcNow,
        };
        
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        
        var orderBooks = orderDto.Books.Select(b =>
        {
            if (b.BookId <= 0)
                throw new ArgumentException("Книга не выбрана");
            if (b.Amount <= 0)
                throw new ArgumentException("Не выбрано количество");
            
            return new OrderBooksEntity
            {
                OrderId = order.Id,
                BookId = b.BookId,
                AmountBook = b.Amount,
            };
        });
        
        await _context.OrderBooks.AddRangeAsync(orderBooks);
        await _context.SaveChangesAsync();
        
        return order.Id;
    }
    
    /// <summary>
    /// Возвращает список заказов, отфильтрованный по заданному диапазону дат.
    /// </summary>
    /// <param name="orderDto">Фильтр с датой начала и окончания.</param>
    /// <returns>Список заказов в указанный период или <c>null</c>, если формат даты некорректен.</returns>
    /// <exception cref="ArgumentException">Если дата начала позже даты окончания.</exception>
    public async Task<List<OrdersFilterListDto>> GetListFilteredAsync(OrderFilterDto orderDto)
    {
        DateTime fromData;
        DateTime toData;
        
        try
        {
            fromData = DateTime.SpecifyKind(
                DateTime.ParseExact(orderDto.FromDate, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                DateTimeKind.Utc);

            toData = DateTime.SpecifyKind(
                DateTime.ParseExact(orderDto.ToDate, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                DateTimeKind.Utc);
        }
        catch (FormatException)
        {
            return null;
        }

        if (fromData > toData)
            throw new ArgumentException("Дата начала больше даты конца");

        var orderListEntity = _context.Orders
            .Where(o => o.OrderDate >= fromData && o.OrderDate <= toData)
            .AsQueryable();

        var orderList = await orderListEntity
            .Select(o => new OrdersFilterListDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
            })
            .ToListAsync();

        return orderList;
    }
}