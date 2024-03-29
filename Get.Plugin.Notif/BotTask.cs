using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using System.Runtime.InteropServices;
using static Get.Plugin.Notif.MyPlugin;

namespace Get.Plugin.Notif
{
    internal class BotTask
    {
        public static string NotificationBot = "Уведомление от бота";
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);
        public static void BotWorker()
        {
            var botClient = new TelegramBotClient("6347892364:AAH2BMZ08X9********************"); //Ваш токен бота
            using (var cts = new CancellationTokenSource())
            {
                // Настройка получения обновлений
                var receiverOptions = new ReceiverOptions
                {
                    AllowedUpdates = Array.Empty<UpdateType>() // Получать все типы обновлений
                };

                botClient.StartReceiving(
                    updateHandler: HandleUpdateAsync,
                    pollingErrorHandler: HandleErrorAsync,
                    receiverOptions: receiverOptions,
                    cancellationToken: cts.Token);

                MessageBox(new IntPtr(0), "Бот запущен!", "TelegramNotification", 0);
            }
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Проверяем, есть ли сообщение
            if (update.Type != UpdateType.Message || update.Message.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

            //MessageBox(new IntPtr(0), $"Получено новое сообщение: {messageText}", "TelegramNotification", 0);
            PluginStart(messageText);
            await botClient.SendTextMessageAsync(chatId, $"Вы сказали: {messageText}", cancellationToken: cancellationToken);

        }

        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            string ErrorMessage = "Произошла ошибка";
            if (exception is ApiRequestException apiRequestException)
            {
                ErrorMessage = $"Ошибка Telegram API:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}";
            }
            else
            {
                ErrorMessage = exception.ToString();
            }

            MessageBox(new IntPtr(0), ErrorMessage, "TelegramNotification", 0);
            return Task.CompletedTask;
        }
    }

    
}

