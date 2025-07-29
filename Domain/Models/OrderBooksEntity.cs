namespace Domain.Models;

/// <summary>
/// Сущность для связи заказов с книгами.
/// Представляет отношение "многие ко многим" между заказами и книгами, с указанием количества.
/// </summary>
public class OrderBooksEntity
{
    public int OrderId { get; set; }
    public OrderEntity OrderEntity { get; set; }
    
    public int BookId { get; set; }
    public BookEntity BookEntity { get; set; }
    
    public int AmountBook { get; set; }
}