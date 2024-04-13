using Tele.Bot.Client;
using Tele.Bot.Services;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injections here:
//builder.Services.AddTransient<>();

builder.Services.AddSingleton<ITelegramService, TelegramService>();
builder.Services.AddTransient<IRestApiClient, RestApiClient>();

builder.Services.AddHttpClient<RestApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7241/");
});




var app = builder.Build();

using var cts = new CancellationTokenSource();

var provider = builder.Services.BuildServiceProvider();
var teleBot = provider.GetService<ITelegramService>();

teleBot.StartBot(cts);
app.Run();

// Send cancellation request to stop bot
cts.Cancel();

