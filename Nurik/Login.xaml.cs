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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
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

                User authUser = null;
                using (ApplicationContext db = new ApplicationContext())
                {
                    authUser = db.Users.Where(b => b.Login == login && b.Password == password).FirstOrDefault();
                }
                if (authUser != null)
                {
                    MessageBox.Show("Все хорошо!");
                    Apps_window menu = new Apps_window();
                    menu.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Вы ввели некорректные данные!");
                }
            }
        }

    }
}
