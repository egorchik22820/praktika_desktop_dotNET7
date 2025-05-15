using praktika_desktop_dotNET7.Models.Entities;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ServiceInfoPage.xaml
    /// </summary>
    public partial class ServiceInfoPage : Page
    {
        public Service CurrentService { get; set; }
        public ServiceInfoPage(Service service)
        {
            InitializeComponent();
            CurrentService = service;
            DataContext = CurrentService;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
