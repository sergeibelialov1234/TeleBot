using Microsoft.EntityFrameworkCore;
using Tele.Bot.Models;

namespace Tele.Bot.Context;

public class RentContext : DbContext
{
    public DbSet<Rent> Rentals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=mydatabase.db");
    }
}