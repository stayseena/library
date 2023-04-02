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
    /// Логика взаимодействия для PublicationPage.xaml
    /// </summary>
    public partial class PublicationPage : Page
    {
        public PublicationPage()
        {
            InitializeComponent();

            DGridPublication.ItemsSource = ApplicationContext.Getcontext().Book.ToList();
        }

        private void Delete_button_Click(object sender, RoutedEventArgs e) //удаление публикаций
        {
            var publicationsForRemoving = DGridPublication.SelectedItems.Cast<Book>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {publicationsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ApplicationContext.Getcontext().Book.RemoveRange(publicationsForRemoving);
                    ApplicationContext.Getcontext().SaveChanges();
                    MessageBox.Show("Данные удалены.");

                    DGridPublication.ItemsSource = ApplicationContext.Getcontext().Publication.ToList();
                }
                catch
                {
                    MessageBox.Show("Не удалось удалить данные. Выбранная публикация(ции) уже используется в системе. Удалите сначала записи, где она содержится (в таблице 'Аренда')");
                }
            }
        }

        private void AddPublication_button_Click(object sender, RoutedEventArgs e) //добавление публикации
        {
            Add_publication add = new(null);
            add.ShowDialog();
        }

        private void EditPublication_button_Click(object sender, RoutedEventArgs e) //редактирование публикации
        {
            Add_publication add = new((sender as Button).DataContext as Publication);
            add.ShowDialog();
        }

        private void Mouse_right_click(object sender, MouseButtonEventArgs e)
        {
            DGridPublication.ItemsSource = ApplicationContext.Getcontext().Book.ToList();
        }
    }
}
