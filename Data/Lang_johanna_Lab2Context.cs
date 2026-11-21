using Microsoft.EntityFrameworkCore;

public class Lang_johanna_Lab2Context(DbContextOptions<Lang_johanna_Lab2Context> options) : DbContext(options)
{
    public DbSet<Lang_johanna_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Lang_johanna_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Lang_johanna_Lab2.Models.Author> Author { get; set; } = default!;
    public DbSet<Lang_johanna_Lab2.Models.Category> Category { get; set; } = default!;
}
