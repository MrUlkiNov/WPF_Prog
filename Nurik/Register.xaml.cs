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
using System.Windows.Shapes;

namespace Nurik
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        ApplicationContext db;
        public Register()
        {
            InitializeComponent();
            db = new ApplicationContext();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = RegisterLogin.Text.Trim().ToLower();
            string password = RegisterPassword.Text.Trim().ToLower();

            if (login.Length < 5)
            {
                RegisterLogin.ToolTip = "Логин должен иметь больше пяти символов";
                RegisterLogin.Background = Brushes.DarkRed;
            }
            else if (password.Length < 5)
            {
                RegisterPassword.ToolTip = "Пароль должен содержать минимум пять символов";
                RegisterPassword.Background = Brushes.DarkRed;
            }
            else
            {


                RegisterLogin.ToolTip = "";
                RegisterLogin.Background = Brushes.Transparent;
                RegisterPassword.ToolTip = "";
                RegisterPassword.Background = Brushes.Transparent;

                User user = new User(login, password);

                db.Users.Add(user);
                db.SaveChanges();

                MainWindow menu = new MainWindow();
                menu.Show();
                Hide();
            }
        }
    }
}
