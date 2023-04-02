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
using static СУБД_Библиотека.Classes.User;

namespace СУБД_Библиотека
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Input_Click(object sender, RoutedEventArgs e) //открытие окна worker
        {
            try
            {
                using (ApplicationContext db = new())
                {
                    var currentUser = db.User.FirstOrDefault(p => p.Login == Email_box.Text && p.Password == Password_box.Password);

                    if (currentUser != null)
                    {
                        User.currentUser = currentUser;

                        if (User.currentUser.RoleID == 1)
                        {
                            Hide();
                            MessageBox.Show("Вы вошли в систему как администратор: " + currentUser.Name);
                            Worker enter = new();
                            enter.ShowDialog();
                            Close();
                        }
                        else if (User.currentUser.RoleID == 0)
                        {
                            Hide();
                            MessageBox.Show("Вы вошли в систему как читатель: " + currentUser.Name + ". Пройдите еще одну проверку и докажите что вы не робот.");
                            Captcha cap = new();
                            cap.ShowDialog();
                            Close();
                        }
                    }
                    else
                    {
                        StringBuilder errors = new();

                        if (string.IsNullOrWhiteSpace(Email_box.Text))
                            errors.AppendLine("Вы не ввели адрес электронной почты");
                        if (string.IsNullOrWhiteSpace(Password_box.Password))
                            errors.AppendLine("Вы не ввели пароль");

                        if (errors.Length > 0)
                        {
                            MessageBox.Show(errors.ToString());
                            return;
                        }

                        MessageBox.Show("Вы ввели неверные данные");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка. Возможно, у вас отсутствует подключение к интернету.");
            }
        }

        private void Exit_button_Click(object sender, RoutedEventArgs e) //закрытие программы
        {
            Close();
        }

        private void Create_button_Click(object sender, RoutedEventArgs e) //открытие окна registration
        {
            Hide();
            Registration reg = new();
            reg.ShowDialog();
            Close();
        }
    }
}
