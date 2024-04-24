using Tele.Bot.Client;
using Tele.Bot.Context;
using Tele.Bot.Services;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injections here:
builder.Services.AddTransient<IRentService, RentService>();
builder.Services.AddTransient<IRentalDbService, RentalDbService>();
builder.Services.AddSingleton<ITelegramService, TelegramService>();
builder.Services.AddTransient<IRestApiClient, RestApiClient>();

builder.Services.AddHttpClient<RestApiClient>(client =>
{
    client.BaseAddress = new Uri("https://api.4rent.ca/");
});

builder.Services.AddEntityFrameworkSqlite()
    .AddDbContext<RentContext>();

var serviceProvide = builder.Services.BuildServiceProvider();
var context = serviceProvide.GetRequiredService<RentContext>();
context.Database.EnsureCreated();

var app = builder.Build();

using var cts = new CancellationTokenSource();

var provider = builder.Services.BuildServiceProvider();
var teleBot = provider.GetService<ITelegramService>();

teleBot.StartBot(cts);
app.Run();

// Send cancellation request to stop bot
cts.Cancel();

