namespace Application.Models;

/// <summary>
/// DTO для описания книги в заказе.
/// Указывает идентификатор книги и количество.
/// </summary>
public class OrderBookDto
{
    public int BookId { get; set; }
    
    /// <summary>
    /// Количество экземпляров книги в заказе.
    /// </summary>
    public int Amount { get; set; }
}