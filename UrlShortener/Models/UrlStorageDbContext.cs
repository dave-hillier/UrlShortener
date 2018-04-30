using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Models
{
  public class UrlMapping
  {
    public int Id { get; set; }

    [Required, MaxLength(2048)]
    public string Url { get; set; }
  }

  public class UrlStorageDbContext : DbContext
  {
    public UrlStorageDbContext(DbContextOptions<UrlStorageDbContext> options)
      : base(options)
    {
    }

    public DbSet<UrlMapping> Urls { get; set; }
  }
}