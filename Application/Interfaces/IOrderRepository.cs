using Application.Models;

namespace Application.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с заказами.
/// Определяет методы для создания заказов и фильтрации по дате.
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Создаёт новый заказ на основе переданных данных.
    /// </summary>
    /// <param name="orderDto">Данные для создания заказа.</param>
    /// <returns>Идентификатор созданного заказа.</returns>
    Task<int> CreateAsync(OrderCreateDto orderDto);
    
    /// <summary>
    /// Получает список заказов, отфильтрованный по указанному временному диапазону.
    /// </summary>
    /// <param name="orderDto">Данные фильтрации (даты начала и окончания).</param>
    /// <returns>Список заказов, соответствующих фильтру.</returns>
    Task<List<OrdersFilterListDto>> GetListFilteredAsync(OrderFilterDto orderDto);
}