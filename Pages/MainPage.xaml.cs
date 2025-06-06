﻿using System;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Frame _mainFrame;
        public MainPage(Frame frame)
        {
            InitializeComponent();
            _mainFrame = frame;
        }

        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new Pages.ContactsPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new Pages.ServicesPage(_mainFrame));
        }
    }
}
