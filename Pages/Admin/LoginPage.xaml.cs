using Microsoft.VisualBasic.ApplicationServices;
using praktika_desktop_dotNET7.Models;
using praktika_desktop_dotNET7.Models.Entities;
using RecepiesMODULS.AppData;
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
using Microsoft.AspNetCore.Identity;

namespace praktika_desktop_dotNET7.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private Frame _mainFrame;
        private Models.Entities.AspNetUsers _admin;

        public LoginPage(Frame frame)
        {
            InitializeComponent();

            _mainFrame = frame;
            _admin = new Models.Entities.AspNetUsers();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            // Здесь ваша логика проверки авторизации
            if (IsValidAdmin(login, password))
            {
                // Успешный вход
                _mainFrame.Navigate(new AdminServicesPage(_mainFrame));
            }
            else
            {
                // Ошибка авторизации
                ErrorTextBlock.Text = "Неверный логин или пароль";
                ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }

        private bool IsValidAdmin(string login, string password)
        {
            using (var db = new AppDbContextFactory().CreateDbContext(null))
            {
                var admin = db.AspNetUsers.FirstOrDefault(a => a.UserName == login);
                if (admin == null) return false;

                var hasher = new PasswordHasher<IdentityUser>();
                var result = hasher.VerifyHashedPassword(null, admin.PasswordHash, password);

                return result == PasswordVerificationResult.Success;
            }
        }
    }
}
