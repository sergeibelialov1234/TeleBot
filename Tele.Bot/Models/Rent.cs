namespace Tele.Bot.Models;

public class Rent
{
    public int Id { get; set; }
    public int ResponseId { get; set; }

    public string? Internal4rentId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? MinPrice { get; set; }

    public string? MaxPrice { get; set; }

    public string? Image { get; set; }

    public string? PropertyType { get; set; }
}