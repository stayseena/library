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
using static System.Net.Mime.MediaTypeNames;
using static СУБД_Библиотека.Classes.User;

namespace СУБД_Библиотека
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private User _newuser = new User();
        public Registration()
        {
            InitializeComponent();
            DataContext = _newuser;
        }

        private void Back_button_Click(object sender, RoutedEventArgs e) //открытие стартового окна
        {
            Hide();
            MainWindow enter = new MainWindow();
            enter.ShowDialog();
            Close();
        }

        private void Create_button_Click(object sender, RoutedEventArgs e) //кнопка сохранить нового пользователя
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_newuser.Name))
                errors.AppendLine("Укажите ваше полное имя");
            if (string.IsNullOrWhiteSpace(_newuser.Phone))
                errors.AppendLine("Введите ваш номер телефона в указанном формате");
            if (string.IsNullOrWhiteSpace(_newuser.Login))
                errors.AppendLine("Укажите ваш адрес электронной почты");
            if (string.IsNullOrWhiteSpace(_newuser.Password))
                errors.AppendLine("Придумайте и запишите пароль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_newuser.ID == 0)
            {
                (_newuser.Status) = 0;
                (_newuser.RoleID) = 0;
                ApplicationContext.Getcontext().User.Add(_newuser);
            }
            try
            {
                ApplicationContext.Getcontext().SaveChanges();
                MessageBox.Show("Поздравляю! Новый пользователь создан. Можете выбирать книгу");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
