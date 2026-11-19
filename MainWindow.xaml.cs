using Kramskoi_AutoService.Pages;
using System;
using System.Windows;

namespace Kramskoi_AutoService
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frContent.Content = new Autho();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            frContent.GoBack();
        }

        private void frContent_ContentRendered(object sender, EventArgs e)
        {
            if (frContent.CanGoBack)
                btnBack.Visibility = Visibility.Visible;
            else
                btnBack.Visibility = Visibility.Hidden;
        }
    }
}
