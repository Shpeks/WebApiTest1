namespace Domain.Models;

/// <summary>
/// Сущность книг
/// </summary>
public class BookEntity
{
    public int Id { get; init; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Дата выпуска
    /// </summary>
    public DateTime PublicationDate { get; set; }
    
    public ICollection<OrderBooksEntity> OrderBooks { get; init; }
}