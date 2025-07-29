namespace Application.Models;

/// <summary>
/// DTO для фильтрации заказов по диапазону дат.
/// Используется при получении списка заказов в заданный период времени.
/// </summary>
public class OrderFilterDto
{
    /// <summary>
    /// Начальная дата диапазона фильтрации в формате "dd.MM.yyyy HH:mm".
    /// </summary>
    public string FromDate { get; set; }
    
    /// <summary>
    /// Конечная дата диапазона фильтрации в формате "dd.MM.yyyy HH:mm".
    /// </summary>
    public string ToDate { get; set; }
}