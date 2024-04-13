using Tele.Bot.Client;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Tele.Bot.Services;

public class TelegramService : ITelegramService
{
    // DI сервисы
    private readonly IRestApiClient _restApiClient;
    // не забываем добавить их в конструктор
    public TelegramService(RestApiClient restApiClient)
    {
        _restApiClient = restApiClient;
    }
    
    public Task StartBot(CancellationTokenSource cts)
    {
        var botClient = new TelegramBotClient("token");

 

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
        };

        botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );

        return Task.CompletedTask;
    }
    
    
    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {

        var userId = update.Message.Chat.Id;
        // Only process Message updates: https://core.telegram.org/bots/api#message
        if (update.Message is not { } message)
            return;
        // Only process text messages
        if (message.Text is not { } messageText)
            return;


        // Работа
        if (messageText == "/start")
        {
            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Используй команды /say /test",
                cancellationToken: cancellationToken);
        
            return;
        }
    
        //
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "пРИВЕТ",
            cancellationToken: cancellationToken);
    }


    
    
    
    // Не трогаем, это метотд обработки ошибок. Выводит в консоль ошибку, если она произошла
    private async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);

    }
}