using Application.Models;

namespace Application.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с книгами.
/// Определяет методы получения информации о книгах.
/// </summary>
public interface IBookRepository
{
    /// <summary>
    /// Получает книгу по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор книги.</param>
    /// <returns>Данные о книге, если найдена; иначе <c>null</c>.</returns>
    Task<BookDto?> GetByIdAsync(int id);
}