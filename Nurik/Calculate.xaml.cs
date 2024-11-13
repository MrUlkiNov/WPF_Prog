using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
    public partial class Calculate : Window
    {
        private string connectionString = "Data Source=calculations.db;Version=3;";
        private string lastExpression = string.Empty;

        public Calculate()
        {
            InitializeComponent();
            InitializeDatabase();

            foreach (UIElement el in MainRoot.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += Button_Click;
                }
            }
        }

        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = "CREATE TABLE IF NOT EXISTS Results (Id INTEGER PRIMARY KEY AUTOINCREMENT, Expression TEXT, Result TEXT)";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = (string)((Button)e.OriginalSource).Content;

            if (str == "AC")
            {
                textLabel.Text = "";
                lastExpression = "";
            }
            else if (str == "=")
            {
                try
                {
                    string value = new DataTable().Compute(textLabel.Text, null).ToString();
                    lastExpression = textLabel.Text;
                    textLabel.Text = value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in calculation: " + ex.Message);
                }
            }
            else if (str != "Сохранить" && str != "История" && str != "Назад" && str != "Очистить историю")
            {
                textLabel.Text += str;
            }
        }

        private void SaveResult_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(lastExpression) && !string.IsNullOrEmpty(textLabel.Text))
            {
                string result = textLabel.Text;

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO Results (Expression, Result) VALUES (@Expression, @Result)";
                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Expression", lastExpression);
                        command.Parameters.AddWithValue("@Result", result);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Result saved!");
            }
            else
            {
                MessageBox.Show("No calculation to save!");
            }
        }

        private void ShowHistory_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Expression, Result FROM Results";
                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        StringBuilder history = new StringBuilder();
                        while (reader.Read())
                        {
                            history.AppendLine($"{reader["Expression"]} = {reader["Result"]}");
                        }

                        MessageBox.Show(history.ToString(), "History");
                    }
                }
            }
        }

        private void ClearHistory_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Results";
                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("History cleared!");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Apps_window appw = new Apps_window();
            appw.Show();
            Hide();
        }
    }
}


