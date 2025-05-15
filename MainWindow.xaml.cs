using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace praktika_desktop_dotNET7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(new Pages.MainPage(MainFrame)); // Передаём Frame в Page
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.MainPage(MainFrame));
        }

        private void ServicesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.ServicesPage(MainFrame));
        }

        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.ContactsPage());
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new Pages.MainPage(MainFrame));
        }
    }
}