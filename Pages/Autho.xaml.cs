using Kramskoi_AutoService.Models;
using Kramskoi_AutoService.Services;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Kramskoi_AutoService.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        int click;

        public Autho()
        {
            InitializeComponent();
            click = 0;
        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            HideCaptcha();
            click = 0;
            NavigationService.Navigate(new Client(null, null));
        }

        private void GenerateCapctcha()
        {
            tbCaptcha.Visibility = Visibility.Visible;
            tblCaptcha.Visibility = Visibility.Visible;

            string capctchaText = CaptchaGenerator.GenerateCaptchaText(6);
            tblCaptcha.Text = capctchaText;
            tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
            tbCaptcha.Clear();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            click += 1;
            string login = tbLogin.Text.Trim();
            string password = tbPassword.Text.Trim();
            string passwordHash = Hash.HashPassword(password);

            AutoServiceEntities db = AutoServiceEntities.GetContext();

            var user = db.User.Where(x => x.Login == login && x.PasswordHash == passwordHash).FirstOrDefault();
            if (click == 1)
            {
                if (user != null)
                {
                    EnterUser(user);
                }
                else
                {
                    MessageBox.Show("Вы ввели логин или пароль неверно!");
                    tbPassword.Clear();
                    GenerateCapctcha();
                }
            }
            else if (click > 1)
            {
                if (user != null && tbCaptcha.Text.Trim() == tblCaptcha.Text)
                {
                    EnterUser(user);
                }
                else
                {
                    MessageBox.Show("Введите данные заново!");
                    click = 1;
                    tbPassword.Clear();
                    GenerateCapctcha();
                }
            }
        }

        private void EnterUser(User user)
        {
            string role = user.Role.RoleName.ToString();

            MessageBox.Show("Вы вошли под: " + role);
            ClearFields();
            HideCaptcha();
            LoadPage(role, user);
        }

        private void ClearFields()
        {
            tbLogin.Clear();
            tbPassword.Clear();
            tbCaptcha.Clear();
        }

        private void HideCaptcha()
        {
            tbCaptcha.Visibility = Visibility.Hidden;
            tblCaptcha.Visibility = Visibility.Hidden;
            tblCaptcha.Text = string.Empty;
        }

        private void LoadPage(string _role, User user)
        {
            click = 0;
            switch (_role)
            {
                case "Администратор":
                    NavigationService.Navigate(new Admin(user, _role));
                    break;
                case "Сотрудник":
                    NavigationService.Navigate(new Employee(user, _role));
                    break;
                case "Пользователь":
                    NavigationService.Navigate(new Client(user, _role));
                    break;
            }
        }
    }
}
