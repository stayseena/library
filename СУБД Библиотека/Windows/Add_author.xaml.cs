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
using СУБД_Библиотека.Classes;
using СУБД_Библиотека.Pages;

namespace СУБД_Библиотека.Windows
{
    /// <summary>
    /// Логика взаимодействия для Add_author.xaml
    /// </summary>
    public partial class Add_author : Window
    {
        private Author _newauthor = new Author();
        public Add_author(Author selectedAuthor)
        {
            InitializeComponent();

            if (selectedAuthor != null)
                _newauthor = selectedAuthor;

            DataContext = _newauthor;
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Create_button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_newauthor.Name))
                errors.AppendLine("Вы ничего не указали");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_newauthor.ID == 0)
                ApplicationContext.Getcontext().Author.Add(_newauthor);
            try
            {
                ApplicationContext.Getcontext().SaveChanges();
                MessageBox.Show("Автор добавлен");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
