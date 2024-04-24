using Microsoft.AspNetCore.Components.Forms;
using Tele.Bot.Client;
using Tele.Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Tele.Bot.Services;

public class TelegramService : ITelegramService
{
    // DI сервисы
    // private readonly ВАШ СЕРВИС
    private readonly IRentService _rentService;
    private readonly IRentalDbService _rentalDbService;

    // не забываем добавить их в конструктор
    public TelegramService(IRentService rentService, IRentalDbService rentalDbService)
    {
        _rentService = rentService;
        _rentalDbService = rentalDbService;
    }
    
    public Task StartBot(CancellationTokenSource cts)
    {
        var botClient = new TelegramBotClient("BOT TOKEN");

 

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
        var result = await _rentService.GetRentals();

        var responseArray = result.Results.Take(5);

        foreach (var appartment in responseArray)
        {
            var dbEntity = new Rent()
            {
                Image = appartment.Image.ThumbnailMedium,
                ResponseId = appartment.Id,
                MaxPrice = appartment.MaxPrice,
                Name = appartment.Name,
                Address = appartment.Address,
                Internal4rentId = appartment.Internal4rentId,
                MinPrice = appartment.MinPrice,
                PropertyType = appartment.PropertyType
            };

            await _rentalDbService.SaveToDb(dbEntity);
            
            //
            await botClient.SendPhotoAsync(
                chatId: message.Chat.Id,
                appartment.Image.ThumbnailMedium,
                caption: $"Appartment name {appartment.Name}  \n with price: {appartment.MaxPrice}",
                cancellationToken: cancellationToken);
            
        }
    
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