using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
using Word = Microsoft.Office.Interop.Word;

namespace СУБД_Библиотека.Windows
{
    /// <summary>
    /// Логика взаимодействия для Add_rent.xaml
    /// </summary>
    public partial class Add_rent : Window
    {
        private Rent _newrent = new Rent();
        public Add_rent(Rent selectedRent)
        {
            InitializeComponent();

            if (selectedRent != null)
                _newrent = selectedRent;

            Reader_box.ItemsSource = ApplicationContext.Getcontext().User.Where(p => p.Status < 4).ToList();
            Publication_box.ItemsSource = ApplicationContext.Getcontext().Publication.Where(p => p.Actual == true).ToList();

            DataContext = _newrent;
        }

        private void Create_button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (Reader_box.SelectedItem == null)
                errors.AppendLine("Вы не выбрали читателя");
            if (Publication_box.SelectedItem == null)
                errors.AppendLine("Вы не выбрали публикацию");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _newrent.Reader = Int32.Parse(Reader_box.SelectedValue.ToString());
            _newrent.Publication = Int32.Parse(Publication_box.SelectedValue.ToString());

            if (_newrent.ID == 0)
            {
                _newrent.Date_of_taking = DateTime.Today.ToString();
                //_newrent == ApplicationContext.Getcontext().Publication.Where(p => p.Actual == true);
                ApplicationContext.Getcontext().Rent.Add(_newrent);
            }
            try
            {
                ApplicationContext.Getcontext().SaveChanges();
                MessageBox.Show("Новая запись добавлена");
                Check();
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

        private void Check()
        {
            var date1 = DateTime.Today;
            DateTime now = DateTime.Now;

            var application = new Word.Application();
            Word.Document document = application.Documents.Add();
            Word.Paragraph paragraph1 = document.Paragraphs.Add();
            Word.Range rangel = paragraph1.Range;

            paragraph1 = document.Paragraphs.Add();
            rangel.Font.Size = 16;
            rangel.Bold = 2;
            rangel.Font.Name = "Courier New";
            rangel.Text = "БИЛЕТ БИБЛИОТЕКИ";
            rangel.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            paragraph1 = document.Paragraphs.Add();
            rangel = paragraph1.Range;
            rangel.Font.Size = 14;
            rangel.Font.Name = "Courier New";
            rangel.Text = "ВМУК 'Страничное перо' ";
            rangel.InsertParagraphAfter();

            paragraph1 = document.Paragraphs.Add();
            rangel = paragraph1.Range;
            rangel.Font.Size = 14;
            rangel.Font.Name = "Courier New";
            rangel.Text = "Читатель: " + Reader_box.SelectedIndex;
            rangel.InsertParagraphAfter();

            paragraph1 = document.Paragraphs.Add();
            rangel = paragraph1.Range;
            rangel.Font.Size = 14;
            rangel.Font.Name = "Courier New";
            rangel.Text = "Выдаваемая публикация: " + Publication_box.ItemStringFormat;
            rangel.InsertParagraphAfter();

            paragraph1 = document.Paragraphs.Add();
            rangel = paragraph1.Range;
            rangel.Font.Size = 14;
            rangel.Font.Name = "Courier New";
            rangel.Text = "_______________________________________________________";
            rangel.InsertParagraphAfter();

            paragraph1 = document.Paragraphs.Add();
            rangel = paragraph1.Range;
            rangel.Font.Size = 14;
            rangel.Font.Name = "Courier New";
            rangel.Text = "Дата выдачи публикации: " + date1.ToString("dd.MM.yyyy");
            rangel.InsertParagraphAfter();

            paragraph1 = document.Paragraphs.Add();
            rangel = paragraph1.Range;
            rangel.Font.Size = 14;
            rangel.Font.Name = "Courier New";
            rangel.Text = "Сроки продления или возврата: 2 недели, начиная от сегодняшнего дня";
            rangel.InsertParagraphAfter();

            paragraph1 = document.Paragraphs.Add();
            rangel = paragraph1.Range;
            rangel.Font.Size = 14;
            rangel.Font.Name = "Courier New";
            rangel.Text = "_______________________________________________________";
            rangel.InsertParagraphAfter();

            paragraph1 = document.Paragraphs.Add();
            rangel = paragraph1.Range;
            rangel.Font.Size = 14;
            rangel.Font.Name = "Courier New";
            rangel.Text = "Настоящим подтверждается, что читатель ознакомлен с правилами " +
                "выдачи и возврата публикаций данной библиотеки и намеревается их соблюдать.";
            rangel.InsertParagraphAfter();

            paragraph1 = document.Paragraphs.Add();
            rangel = paragraph1.Range;
            rangel.Font.Size = 14;
            rangel.Font.Name = "Courier New";
            rangel.Text = "Подпись читателя: _____________";
            rangel.InsertParagraphAfter();

            paragraph1 = document.Paragraphs.Add();
            rangel = paragraph1.Range;
            rangel.Font.Size = 14;
            rangel.Font.Name = "Courier New";
            rangel.Text = "Подпись работника библиотеки: _____________";
            rangel.InsertParagraphAfter();

            application.Visible = true;
        }
    }
}
