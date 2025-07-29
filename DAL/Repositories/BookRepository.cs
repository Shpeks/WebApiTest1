using Application.Interfaces;
using Application.Models;
using DAL.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

/// <summary>
/// Репозиторий для работы с книгами
/// Методы для получения информации о книгах
/// </summary>
public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Получает информацию о книге по её идентификатору
    /// </summary>
    /// <returns>
    /// DTO с информацией о книге, если найдена; иначе null
    /// </returns>
    public async Task<BookDto?> GetByIdAsync(int id)
    {
        var bookDto = await _context.Books
            .Where(b => b.Id == id)
            .Select(b => new BookDto
            {
                Id = b.Id,
                Name = b.Name,
                PublicationDate = DateTime.SpecifyKind(b.PublicationDate, DateTimeKind.Local),
            })
            .FirstOrDefaultAsync();
        
        return bookDto;
    }
}