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
    /// Логика взаимодействия для RentPage.xaml
    /// </summary>
    public partial class RentPage : Page
    {
        private Rent _updaterent = new Rent();
        public RentPage()
        {
            InitializeComponent();

            DGridRent.ItemsSource = ApplicationContext.Getcontext().Rentss.ToList();

            var date1 = DateTime.Today.ToString();
        }

        private void Delete_button_Click(object sender, RoutedEventArgs e) //удаление записи
        {
            var rentsForRemoving = DGridRent.SelectedItems.Cast<Rent>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {rentsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ApplicationContext.Getcontext().Rent.RemoveRange(rentsForRemoving);
                    ApplicationContext.Getcontext().SaveChanges();
                    MessageBox.Show("Данные удалены.");

                    DGridRent.ItemsSource = ApplicationContext.Getcontext().Rent.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void AddRent_button_Click(object sender, RoutedEventArgs e)
        {
            Add_rent add = new(null);
            add.ShowDialog();
        }

        private void EditRent_button_Click(object sender, RoutedEventArgs e)
        {
            var rentsForUpdate = DGridRent.SelectedItems.Cast<Rent>().ToList();

            if (MessageBox.Show($"Вы точно хотите сделать возврат на данную {rentsForUpdate.Count()} запись?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    _updaterent.Return_date = DateTime.Today.ToString();
                    ApplicationContext.Getcontext().Rent.UpdateRange(rentsForUpdate);                   
                    ApplicationContext.Getcontext().SaveChanges();
                    MessageBox.Show("Данные обновлены.");

                    DGridRent.ItemsSource = ApplicationContext.Getcontext().Rent.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Mouse_right_click(object sender, MouseButtonEventArgs e)
        {
            DGridRent.ItemsSource = ApplicationContext.Getcontext().Rentss.ToList();
        }
    }
}
