namespace Tele.Bot.Services;

public interface ITelegramService
{
    Task StartBot(CancellationTokenSource cts);
}