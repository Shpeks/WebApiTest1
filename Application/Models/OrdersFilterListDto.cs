namespace Application.Models;

/// <summary>
/// DTO для отображения отфильтрованного списка заказов.
/// Содержит базовую информацию о заказе.
/// </summary>
public class OrdersFilterListDto
{
    public int Id { get; set; }
    
    /// <summary>
    /// Дата оформления заказа.
    /// </summary>
    public DateTime OrderDate { get; set; }
}