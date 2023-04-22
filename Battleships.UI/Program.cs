using Battleships.Logic.Models;
using Battleships.Logic.Services;
using Battleships.UI.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddScoped<CoordinatesService>()
    .AddScoped<IInputOutputService, ConsoleOutputService>()
    .AddScoped<GameParameters, StandardGameParameters>()
    .AddScoped<IGameActionsService, GameActionsService>()
    .AddScoped<IGameRunnerService, GameRunnerService>()
    .BuildServiceProvider();

var app = serviceProvider.GetRequiredService<IGameRunnerService>();
app.Run();