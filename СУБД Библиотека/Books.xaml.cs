using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using СУБД_Библиотека.Classes;
using static СУБД_Библиотека.Classes.User;

namespace СУБД_Библиотека
{
    /// <summary>
    /// Логика взаимодействия для Books.xaml
    /// </summary>
    public partial class Books : Window
    {
        public Books()
        {

            InitializeComponent();
            
            var allAuthors = ApplicationContext.Getcontext().Author.ToList();
            allAuthors.Insert(0, new Author
            {
                Name = "Все авторы"
            });
            Author_box.ItemsSource = allAuthors;

            var allGenres = ApplicationContext.Getcontext().Genre.ToList();
            allGenres.Insert(0, new Genre
            {
                Name = "Все жанры"
            });
            Genre_box.ItemsSource = allGenres;


            CheckActual.IsChecked = false;
            Author_box.SelectedIndex = 0;
            Genre_box.SelectedIndex = 0;

            UpdateBooks();
        }

        private void UpdateBooks()
        {
            var currentBooks = ApplicationContext.Getcontext().Publication.ToList();

            if (Author_box.SelectedIndex > 0)
                currentBooks = currentBooks.Where(p => p.Author == Author_box.SelectedIndex).ToList();

            if (Genre_box.SelectedIndex > 0)
                currentBooks = currentBooks.Where(p => p.Genre == Genre_box.SelectedIndex).ToList();


            if (CheckActual.IsChecked.Value)
                currentBooks = currentBooks.Where(p => p.Actual == true).ToList();

            if (Search_box.Text.Length > 0)
                currentBooks = currentBooks.Where(p => Convert.ToString(p.Title).ToLower().Contains(Search_box.Text.ToLower())).ToList();

            ListBooks.ItemsSource = currentBooks.OrderBy(p => p.ID).ToList();
            LVCount.Content = Convert.ToString("По вашему запросу найдено публикаций: " + ListBooks.Items.Count);
        }
        private void Search_box_changed(object sender, TextChangedEventArgs e)
        {
            UpdateBooks();
        }

        private void Author_box_changed(object sender, SelectionChangedEventArgs e)
        {
            UpdateBooks();
        }

        private void CheckActual_checked(object sender, RoutedEventArgs e)
        {
            UpdateBooks();
        }

        private void Back_Click(object sender, RoutedEventArgs e) //переход на главную панель
        {
            Hide();
            MainWindow main = new();
            main.ShowDialog();
            Close();
        }

        private void Genre_box_changed(object sender, SelectionChangedEventArgs e)
        {
            UpdateBooks();
        }

        void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var book = ListViewItem.Selected;
            //ListViewItem currentItem = this.listViewFileRun.SelectedIndices;

            //if (book.Actual == 1)
            //{
            if (MessageBox.Show("Вы выбрали книгу '" + "название книги" + "'.Она сейчас в наличии. Хотите взять ее?", "Информация",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MessageBox.Show("Обратитесь к библиотекарю для офомления аренды. Он ознакомит вас со всей необходимой информацией.");
            }
            //}
            //else
            //{
            //    MessageBox.Show("Вы выбрали книгу '" + "название книги" + "'. В данный момент она занята. Пока вы ждете ее возвращения, можете выбрать и взять другую книгу. Удачи:)");
            //}
        }
    }
}
