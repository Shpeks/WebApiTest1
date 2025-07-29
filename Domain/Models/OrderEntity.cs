namespace Domain.Models;

/// <summary>
/// Сущность заказы
/// </summary>
public class OrderEntity
{
    public int Id { get; init; }
    
    /// <summary>
    /// Дата заказа
    /// </summary>
    public DateTime OrderDate { get; set; }
    
    public ICollection<OrderBooksEntity> OrderBooks { get; init; }
}