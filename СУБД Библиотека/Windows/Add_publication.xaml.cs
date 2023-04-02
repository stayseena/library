using Microsoft.Win32;
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
using System.IO;
using static System.Net.Mime.MediaTypeNames;


namespace СУБД_Библиотека.Windows
{
    /// <summary>
    /// Логика взаимодействия для Add_publication.xaml
    /// </summary>
    public partial class Add_publication : Window
    {
        private Publication _newpublication = new();
        public Add_publication(Publication selectedPublication)
        {
            InitializeComponent();

            if (selectedPublication != null)
                _newpublication = selectedPublication;

            DataContext = _newpublication;
            Authors_box.ItemsSource = ApplicationContext.Getcontext().Author.ToList();
            Genre_box.ItemsSource = ApplicationContext.Getcontext().Genre.ToList();
        }

        private void Create_button_Click(object sender, RoutedEventArgs e)
        {          
            StringBuilder errors = new();

            if (string.IsNullOrWhiteSpace(_newpublication.Title))
                errors.AppendLine("Вы не указали название публикации");
            if (string.IsNullOrWhiteSpace(_newpublication.Years))
                errors.AppendLine("Вы не указали год выпуска публикации");
            if (Authors_box.SelectedItem == null)
                errors.AppendLine("Вы не выбрали автора");
            if (Genre_box.SelectedItem == null)
                errors.AppendLine("Вы не выбрали жанр");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _newpublication.Author = Int32.Parse(Authors_box.SelectedValue.ToString());
            _newpublication.Genre = Int32.Parse(Genre_box.SelectedValue.ToString());

            if (_newpublication.ID == 0)
            {
                _newpublication.Actual = true;
                //_newpublication.Image = Image.Source;
                ApplicationContext.Getcontext().Publication.Add(_newpublication);
            }
            try
            {
                ApplicationContext.Getcontext().SaveChanges();
                MessageBox.Show("Новая публикация добавлена");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Image_button_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OPF = new();
            OPF.Filter = "Файлы png|*.png|Файлы jpg|*.jpg";
            OPF.ShowDialog();
            //byte[] image_bytes = File.ReadAllBytes(OPF.FileName);
            Image.Source = new BitmapImage(new Uri(OPF.FileName));

            //if (OPF.ShowDialog() == DialogResultConverter)
            //{
            //    Image = OPF.FileName;
            //    Bitmap MyImage = new Bitmap(Image);

            //}
        }
    }
}
