using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LabourExchange
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Threading.Mutex _Mutex;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //организуем запуск единственной копии этой программы
            bool createdNew;
            string mutName = "MyApp";
            _Mutex = new System.Threading.Mutex(true, mutName, out createdNew);
            if (!createdNew)
            {
                this.Shutdown();
            }

            //в случае возникновения неперехваченной ошибки
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            //#if (!DEBUG)
            //    DispatcherUnhandledException += App_DispatcherUnhandledException;
            //#endif

        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString());
        }
    }
}
