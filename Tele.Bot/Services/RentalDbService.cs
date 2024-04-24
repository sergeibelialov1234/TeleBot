using Tele.Bot.Context;
using Tele.Bot.Models;

namespace Tele.Bot.Services;

public class RentalDbService : IRentalDbService
{
    private readonly RentContext _context;

    public RentalDbService(RentContext context)
    {
        _context = context;
    }

    public async Task SaveToDb(Rent entityToSave)
    {
        _context.Rentals.Add(entityToSave);
        await _context.SaveChangesAsync();
    } 
}

public interface IRentalDbService
{
    Task SaveToDb(Rent entityToSave);
}