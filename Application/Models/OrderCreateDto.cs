namespace Application.Models;

/// <summary>
/// DTO для создания нового заказа.
/// Содержит дату заказа и список заказанных книг.
/// </summary>
public class OrderCreateDto
{
    /// <summary>
    /// Дата и время создания заказа.
    /// Обычно устанавливается автоматически на сервере.
    /// </summary>
    public DateTime OrderDate { get; set; }
    
    /// <summary>
    /// Список книг, входящих в заказ.
    /// </summary>
    public List<OrderBookDto> Books { get; set; }
}