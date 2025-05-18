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

namespace praktika_desktop_dotNET7.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminServiceInfoPage.xaml
    /// </summary>
    public partial class AdminServiceInfoPage : Page
    {
        public Service CurrentService { get; set; }
        public AdminServiceInfoPage(Service service)
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
