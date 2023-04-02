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
using System.Windows.Navigation;
using System.Windows.Shapes;
using СУБД_Библиотека.Classes;
using СУБД_Библиотека.Windows;
using static СУБД_Библиотека.Classes.User;

namespace СУБД_Библиотека.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorsPage.xaml
    /// </summary>
    public partial class AuthorsPage : Page
    {
        public AuthorsPage()
        {
            InitializeComponent();

            DGridAuthors.ItemsSource = ApplicationContext.Getcontext().Author.ToList();
        }

        private void EditAuthor_button_Click(object sender, RoutedEventArgs e) //редактирование авторов
        {
            Add_author edit = new((sender as Button).DataContext as Author);
            edit.ShowDialog();
        }

        private void AddAuthor_button_Click(object sender, RoutedEventArgs e) //добавление авторов
        {
            Add_author add = new(null);
            add.ShowDialog();
        }

        private void Delete_button_Click(object sender, RoutedEventArgs e) //удаление авторов
        {
            var authorsForRemoving = DGridAuthors.SelectedItems.Cast<Author>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {authorsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ApplicationContext.Getcontext().Author.RemoveRange(authorsForRemoving);
                    ApplicationContext.Getcontext().SaveChanges();
                    MessageBox.Show("Данные удалены.");

                    DGridAuthors.ItemsSource = ApplicationContext.Getcontext().Author.ToList();
                }
                catch
                {
                    MessageBox.Show("Не удалось удалить данные. Выбранный автор(ы) уже используется в системе. Удалите сначала записи, где он содержится (в таблице 'Публикации')");
                }
            }
        }

        private void Mouse_right_click(object sender, MouseButtonEventArgs e)
        {
            DGridAuthors.ItemsSource = ApplicationContext.Getcontext().Author.ToList();
        }
    }
}
