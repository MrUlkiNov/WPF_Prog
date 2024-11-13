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
    /// Логика взаимодействия для Apps_window.xaml
    /// </summary>
    public partial class Apps_window : Window
    {
        public Apps_window()
        {
            InitializeComponent();
        }

        private void Button_Click_Calculater(object sender, RoutedEventArgs e)
        {
            Calculate calculate = new Calculate();
            calculate.Show();
            Hide();
        }
    }
}
