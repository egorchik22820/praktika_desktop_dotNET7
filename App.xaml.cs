using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using praktika_desktop_dotNET7;
using praktika_desktop_dotNET7.Models; // Путь к твоему DbContext
using praktika_desktop_dotNET7.Models.Entities; // Путь к сущностям, если они вынесены в общий проект

namespace DesktopApp
{
    public partial class App : Application
    {
        public static AppDbContext? DbContext { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb; Database=MyCompany; Persist Security Info=False; MultipleActiveResultSets=true; Trusted_Connection=True; TrustServerCertificate=True;");

            DbContext = new AppDbContext(optionsBuilder.Options);

            try
            {
                // Проверим, есть ли подключение
                DbContext.Database.EnsureCreated(); // или EnsureExists/CanConnect
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }

            // Пример: запуск главного окна
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            DbContext?.Dispose();
            base.OnExit(e);
        }
    }
}
