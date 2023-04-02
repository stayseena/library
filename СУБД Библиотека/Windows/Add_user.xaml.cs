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
    /// Логика взаимодействия для Add_user.xaml
    /// </summary>
    public partial class Add_user : Window
    {
        private User _newuser = new();
        public Add_user(User selectedUser)
        {
            InitializeComponent();

            if (selectedUser != null)
                _newuser = selectedUser;

            Role_box.ItemsSource = ApplicationContext.Getcontext().Role.ToList();

            DataContext = _newuser;
        }

        private void Create_button_Click(object sender, RoutedEventArgs e)
        {
            if (Role_box.SelectedIndex == 1)
            {
                _newuser.RoleID = 1;
            }
            else
            {
                _newuser.RoleID = 0;
            }

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_newuser.Name))
                errors.AppendLine("Вы не ввели имя");
            if (string.IsNullOrWhiteSpace(_newuser.Phone))
                errors.AppendLine("Вы не ввели пароль");
            if (string.IsNullOrWhiteSpace(_newuser.Login))
                errors.AppendLine("Вы не указали адрес электронной почты");
            if (string.IsNullOrWhiteSpace(_newuser.Password))
                errors.AppendLine("Придумайте и запишите пароль");
            if (Role_box.SelectedItem == null)
                errors.AppendLine("Вы не выбрали тип пользователя");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_newuser.ID == 0)
            {
                ApplicationContext.Getcontext().User.Add(_newuser);
            }
            try
            {
                ApplicationContext.Getcontext().SaveChanges();
                MessageBox.Show("Новый пользователь добавлен");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            ApplicationContext.Getcontext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            Close();
        }
    }
}
