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
    /// Логика взаимодействия для ReadersPage.xaml
    /// </summary>
    public partial class ReadersPage : Page
    {
        public ReadersPage()
        {
            InitializeComponent();
            DGridUsers.ItemsSource = ApplicationContext.Getcontext().Userss.ToList();
        }

        private void Delete_button_Click(object sender, RoutedEventArgs e) //удаление читателей
        {
            var readersForRemoving = DGridUsers.SelectedItems.Cast<User>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {readersForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ApplicationContext.Getcontext().User.RemoveRange(readersForRemoving);
                    ApplicationContext.Getcontext().SaveChanges();
                    MessageBox.Show("Данные удалены.");

                    DGridUsers.ItemsSource = ApplicationContext.Getcontext().User.ToList();
                }
                catch
                {
                    MessageBox.Show("Не удалось удалить данные. Выбранный пользователь(ли) уже используется в системе. Удалите сначала записи, где он содержится (в таблице 'Аренда')");
                }
            }
        }

        private void Edit_button_Click(object sender, RoutedEventArgs e) //редактирование читателей
        {
            Add_user add = new((sender as Button).DataContext as User);
            add.ShowDialog();
        }

        private void AddUsers_button_Click(object sender, RoutedEventArgs e)
        {
            Add_user add = new(null);
            add.ShowDialog();
        }

        private void Mouse_right_click(object sender, MouseButtonEventArgs e)
        {
            DGridUsers.ItemsSource = ApplicationContext.Getcontext().Userss.ToList();
        }
    }
}
