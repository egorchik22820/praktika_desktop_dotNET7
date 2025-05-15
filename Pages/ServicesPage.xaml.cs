using praktika_desktop_dotNET7.Models.Entities;
using RecepiesMODULS.AppData;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace praktika_desktop_dotNET7.Pages
{
    public partial class ServicesPage : Page
    {
        
        public ObservableCollection<Service> Services { get; set; }
        private Frame _mainFrame;

        public ServicesPage(Frame frame)
        {
            InitializeComponent();
            Services = new ObservableCollection<Service>();
            DataContext = this; // Устанавливаем DataContext
            _mainFrame = frame;
            LoadView();
        }

        public void LoadView()
        {
            Services.Clear();

            using (var db = new AppDbContextFactory().CreateDbContext(null))
            {
                // Важно: загружаем связанные данные
                var allServices = db.Services
                    .Include(s => s.ServiceCategory) // Загружаем категорию
                    .Include(s => s.ServicePhoto)   // Загружаем фото
                    .ToList();

                foreach (var service in allServices)
                {
                    Services.Add(service);
                }
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Ваша логика входа
        }

        private void ServicesListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ServicesListView.SelectedItem is Service selectedService)
            {
                _mainFrame.Navigate(new ServiceInfoPage(selectedService));
            }
        }
    }
}