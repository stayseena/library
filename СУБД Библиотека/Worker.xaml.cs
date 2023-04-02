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
using Excel = Microsoft.Office.Interop.Excel;

namespace СУБД_Библиотека
{
    /// <summary>
    /// Логика взаимодействия для Worker.xaml
    /// </summary>
    public partial class Worker : Window
    {
        public Worker()
        {
            InitializeComponent();
            Manager.Buttons = Buttons;
        }

        private void Readers_button_Click(object sender, RoutedEventArgs e) //переход на страницу readers
        {
            Manager.Buttons.Navigate(new ReadersPage());
        }

        private void Exit_button_Click(object sender, RoutedEventArgs e) //переход на стартовый экран
        {
            Close();
        }

        private void Publication_button_Click(object sender, RoutedEventArgs e) //переход на страницу publication
        {
            Manager.Buttons.Navigate(new PublicationPage());
        }

        private void Authors_button_Click(object sender, RoutedEventArgs e)
        {
            Manager.Buttons.Navigate(new AuthorsPage()); //переход на страницу авторов
        }

        private void Rent_button_Click(object sender, RoutedEventArgs e)
        {
            Manager.Buttons.Navigate(new RentPage()); //переход на страницу аренды
        }

        private void List_button_click(object sender, RoutedEventArgs e)
        {
            Hide();
            Books books = new Books();
            books.ShowDialog();
            Close();
        }
    }
}
