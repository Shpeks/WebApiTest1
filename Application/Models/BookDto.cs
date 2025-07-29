namespace Application.Models;

/// <summary>
/// DTO для представления информации о книге.
/// Используется для передачи данных между слоями приложения.
/// </summary>
public class BookDto
{
    public int Id { get; set; }
    
    /// <summary>
    /// Название книги.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Дата публикации книги.
    /// </summary>
    public DateTime PublicationDate { get; set; }
    
}