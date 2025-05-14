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

namespace praktika_desktop_dotNET7.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        public ObservableCollection<Service> services { get; set; }
        public ServicesPage()
        {
            InitializeComponent();

            services = new ObservableCollection<Service>();
            ServicesListView.ItemsSource = services;

            LoadView();
        }

        public void LoadView()
        {
            services.Clear();

            using (var db = new AppDbContextFactory().CreateDbContext(null))
            {
                var allServices = db.Services.ToList();

                foreach (var service in allServices)
                {
                    services.Add(service);
                }
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
