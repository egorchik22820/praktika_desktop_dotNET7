using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using praktika_desktop_dotNET7.Models.Entities;
using praktika_desktop_dotNET7.Models.Enums;
using RecepiesMODULS.AppData;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace praktika_desktop_dotNET7.Pages.Admin
{
    public partial class AddEditPage : Page
    {
        private Frame _frame;
        public Service CurrentService { get; set; }
        public List<ServiceCategory> Categories { get; set; }
        public List<ServiceTypeEnum> ServiceTypes { get; set; }
        public string PageTitle { get; set; }
        private byte[] _imageData;
        private BitmapImage _serviceImage; // Добавляем поле для временного хранения изображения

        public AddEditPage(Frame frame, Service service = null)
        {
            InitializeComponent();

            LoadCategories();
            LoadServiceTypes();

            _frame = frame;
            if (service == null)
            {
                // Режим добавления
                PageTitle = "Добавление услуги";
                CurrentService = new Service();
            }
            else
            {
                // Режим редактирования
                PageTitle = "Редактирование услуги";
                CurrentService = service;

                // Загрузка изображения если есть
                if (service.ServicePhoto != null)
                {
                    _imageData = service.ServicePhoto.Data;
                    _serviceImage = LoadImageFromBytes(_imageData);
                }
            }

            DataContext = this;
            _frame = frame;
        }

        private void LoadCategories()
        {
            using (var db = new AppDbContextFactory().CreateDbContext(null))
            {
                Categories = db.ServiceCategories.ToList();
            }
        }

        private void LoadServiceTypes()
        {
            ServiceTypes = Enum.GetValues(typeof(ServiceTypeEnum)).Cast<ServiceTypeEnum>().ToList();
        }

        private BitmapImage LoadImageFromBytes(byte[] imageData)
        {
            var image = new BitmapImage();
            using (var stream = new MemoryStream(imageData))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }
            return image;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                Title = "Выберите изображение услуги"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Загрузка изображения в память
                    _imageData = File.ReadAllBytes(openFileDialog.FileName);
                    _serviceImage = LoadImageFromBytes(_imageData);
                    CurrentService.Photo = Path.GetFileName(openFileDialog.FileName);

                    // Обновляем привязку
                    OnPropertyChanged(nameof(ServiceImage));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                }
            }
        }

        // Свойство только для отображения (не сохраняется в БД)
        public BitmapImage ServiceImage => _serviceImage;

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new AppDbContextFactory().CreateDbContext(null))
                {
                    if (CurrentService.Id == 0)
                    {
                        // Добавление новой услуги
                        if (_imageData != null)
                        {
                            CurrentService.ServicePhoto = new ServicePhoto
                            {
                                FileName = CurrentService.Photo,
                                Data = _imageData
                            };
                        }

                        db.Services.Add(CurrentService);
                    }
                    else
                    {
                        // Обновление существующей
                        var existingService = db.Services
                            .Include(s => s.ServicePhoto)
                            .FirstOrDefault(s => s.Id == CurrentService.Id);

                        if (existingService != null)
                        {
                            existingService.Title = CurrentService.Title;
                            existingService.Description = CurrentService.Description;
                            existingService.DescriptionShort = CurrentService.DescriptionShort;
                            existingService.ServiceCategoryId = CurrentService.ServiceCategoryId;
                            existingService.Type = CurrentService.Type;
                            existingService.Photo = CurrentService.Photo;

                            if (_imageData != null)
                            {
                                if (existingService.ServicePhoto == null)
                                {
                                    existingService.ServicePhoto = new ServicePhoto
                                    {
                                        FileName = CurrentService.Photo,
                                        Data = _imageData
                                    };
                                }
                                else
                                {
                                    existingService.ServicePhoto.FileName = CurrentService.Photo;
                                    existingService.ServicePhoto.Data = _imageData;
                                }
                            }
                        }
                    }

                    db.SaveChanges();
                    MessageBox.Show("Услуга успешно сохранена!");
                    NavigationService?.Navigate(new AdminServicesPage(_frame));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}