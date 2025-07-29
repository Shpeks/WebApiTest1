using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace TestApi.Controllers;

/// <summary>
/// Контроллер для управления заказами книг.
/// Предоставляет методы для получения книги по ID, создания заказа и фильтрации заказов по дате.
/// </summary>
[ApiController]
[Route("[controller]")]
public class OrderBookController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBookRepository _bookRepository;

    public OrderBookController(IOrderRepository orderRepository, IBookRepository bookRepository)
    {
        _orderRepository = orderRepository;
        _bookRepository = bookRepository;
    }
    
    /// <summary>
    /// Получает книгу по её идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookByIdAsync(int id)
    {
        var bookDto = await _bookRepository.GetByIdAsync(id);
        
        if (bookDto == null)
            return NotFound($"Книга с Id={id} не найдена");
        
        return Ok(bookDto);
    }
    
    /// <summary>
    /// Создаёт новый заказ на книгу
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync(OrderCreateDto orderDto)
    {
        var orderId = await _orderRepository.CreateAsync(orderDto);
        
        return Ok($"OrderId = {orderId}");
    }
    
    /// <summary>
    /// Получает список заказов, отфильтрованный по дате
    /// </summary>
    [HttpPost("/filter")]
    public async Task<IActionResult> GetFilteredOrders(OrderFilterDto orderDto)
    {
        var orderList = await _orderRepository.GetListFilteredAsync(orderDto);
        
        if (orderList == null)
            return NotFound("Некорректная дата");
        
        return Ok(orderList);
    }
}