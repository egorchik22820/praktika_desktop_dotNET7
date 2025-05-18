using Microsoft.EntityFrameworkCore;
using praktika_desktop_dotNET7.Models.Entities;
using RecepiesMODULS.AppData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace praktika_desktop_dotNET7.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminServicesPage.xaml
    /// </summary>
    public partial class AdminServicesPage : Page
    {
        public ObservableCollection<Service> Services { get; set; }
        private Frame _mainFrame;
        public AdminServicesPage(Frame frame)
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

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new ServicesPage(_mainFrame));
        }

        private void ServicesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ServicesListView.SelectedItem is Service selectedService)
            {
                _mainFrame.Navigate(new AdminServiceInfoPage(selectedService));
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new AddEditPage(_mainFrame, null));
            LoadView();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesListView.SelectedItem is Service selectedService)
            {
                _mainFrame.Navigate(new AddEditPage(_mainFrame, selectedService));
                LoadView();
            }
            else
            {
                MessageBox.Show("Выберите услугу для редактирования");
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesListView.SelectedItem is Service selectedService)
            {
                var result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить услугу \"{selectedService.Title}\"?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (var db = new AppDbContextFactory().CreateDbContext(null))
                        {
                            // Загружаем услугу со всеми зависимостями
                            var serviceToDelete = db.Services
                                .Include(s => s.ServicePhoto)
                                .FirstOrDefault(s => s.Id == selectedService.Id);

                            if (serviceToDelete != null)
                            {
                                // Удаляем связанное фото (если есть)
                                if (serviceToDelete.ServicePhoto != null)
                                {
                                    db.ServicePhotos.Remove(serviceToDelete.ServicePhoto);
                                }

                                // Удаляем саму услугу
                                db.Services.Remove(serviceToDelete);
                                await db.SaveChangesAsync();

                                // Удаляем из ObservableCollection
                                Services.Remove(selectedService);

                                MessageBox.Show("Услуга успешно удалена!", "Успех",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении услуги: {ex.Message}",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите услугу для удаления",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
