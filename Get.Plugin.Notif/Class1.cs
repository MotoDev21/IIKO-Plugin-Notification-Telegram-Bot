using Resto.Front.Api;
using Resto.Front.Api.Attributes;
using Resto.Front.Api.Data.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Get.Plugin.Notif.BotTask;

namespace Get.Plugin.Notif
{
    [PluginLicenseModuleId(******)] //Ваш ID Лицензии 
   
        public sealed class MyPlugin : IFrontPlugin
        {
            public static readonly string Sender = "WRF_Matvey";

            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);
            public MyPlugin()
            {
            MessageBox(new IntPtr(0), "Плагин запущен!", "Уведомление плагина", 0);
            PluginStart("Плагин запущен!");
            BotWorker();
            }

            public static void PluginStart(string NotificationBotText)
            {
            try
            {
                ITerminalsGroup hostTerminalsGroup = PluginContext.Operations.GetHostTerminalsGroup();
                ITerminal hostTerminal = PluginContext.Operations.GetHostTerminal();
                //var logger = PluginContext.Log;
                // Вызовs функции ShowOkPopup
                PluginContext.Operations.AddNotificationMessage(NotificationBotText, MyPlugin.Sender, new TimeSpan?(TimeSpan.FromSeconds(40.0)));
                PluginContext.Operations.AddNotificationMessage(hostTerminalsGroup.Name, MyPlugin.Sender, new TimeSpan?(TimeSpan.FromSeconds(40.0)));
                PluginContext.Operations.AddNotificationMessage(hostTerminal.Name, MyPlugin.Sender, new TimeSpan?(TimeSpan.FromSeconds(40.0)));
                PluginContext.Log.Info(MyPlugin.Sender + " started!");
            }
            catch (Exception ex)
            {
                PluginContext.Log.Error(string.Format("{0}", (object)ex));
            }
            }


            // add your plugin logic here
            private bool disposedValue;

            private void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: освободить управляемое состояние (управляемые объекты)
                    }

                    // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить метод завершения
                    // TODO: установить значение NULL для больших полей
                    disposedValue = true;
                }
            }

            // // TODO: переопределить метод завершения, только если "Dispose(bool disposing)" содержит код для освобождения неуправляемых ресурсов
            // ~MyPlugin()
            // {
            //     // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
            //     Dispose(disposing: false);
            // }

            void IDisposable.Dispose()
            {
                // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }



    }

