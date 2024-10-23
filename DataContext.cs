using Microsoft.EntityFrameworkCore;

namespace PaginationAndFiltering;

public class DataContext(DbContextOptions<DataContext> options):DbContext(options)
{
    public DbSet<Student> Students { get; set; }
}