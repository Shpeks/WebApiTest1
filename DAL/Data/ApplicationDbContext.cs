using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<BookEntity> Books { get; set; }
    public DbSet<OrderBooksEntity> OrderBooks { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<OrderBooksEntity>()
            .HasKey(p => new { p.OrderId, p.BookId });
        
        builder.Entity<OrderBooksEntity>()
            .HasOne(b => b.BookEntity)
            .WithMany(b => b.OrderBooks)
            .HasForeignKey(ob => ob.BookId);
        
        builder.Entity<OrderBooksEntity>()
            .HasOne(o => o.OrderEntity)
            .WithMany(o => o.OrderBooks)
            .HasForeignKey(ob => ob.OrderId);
    }
}