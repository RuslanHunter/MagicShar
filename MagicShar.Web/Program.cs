using MagicShar.Models;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MagicShar.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var token = builder.Configuration["BotToken"]!;
            var webappUrl = builder.Configuration["BotWebAppUrl"]!;

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSingleton<TelegramBotClient>(sp => new TelegramBotClient(token));
            builder.Services.ConfigureTelegramBot<Microsoft.AspNetCore.Http.Json.JsonOptions>(opt => opt.SerializerOptions);
            builder.Services.AddHttpClient("tgwebhook").RemoveAllLoggers().AddTypedClient(httpClient => new TelegramBotClient(token, httpClient));


            var app = builder.Build();
            app.UseHttpsRedirection();

            app.MapGet("/bot/setWebhook", async (TelegramBotClient bot) => { await bot.SetWebhookAsync(webappUrl + "/bot"); return $"Webhook set to {webappUrl}/bot"; });
            app.MapPost("/bot", OnUpdate);

            app.MapPost("/api/magicball/GetAnswer", (MagicBallRequest request) => { 
                var otvet = MagicBall.GetAnswer(request.Question);
                return Results.Ok(otvet);
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.MapRazorPages();


            app.Run();

            // Ловим сообщения от бота и обрабатываем
            async void OnUpdate(TelegramBotClient bot, Update update)
            {
                switch (update)
                {
                    case { Message.Text: "/start" }:
                        await bot.SendTextMessageAsync(update.Message.Chat, "<b>Проверь Магию</b>",
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                            replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton.WithWebApp("Запустить МАГИЮ!", webappUrl));
                        break;
                    default: break;
                }
            }

            


        }
    }
}
