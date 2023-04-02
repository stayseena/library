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

namespace СУБД_Библиотека
{
    /// <summary>
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
        public Captcha()
        {
            InitializeComponent();

            Code();
        }

        private void Code()
        {
            String allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z" +
               "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z,1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split(a);
            String pwd = "";
            string temp = "";
            Random r = new Random();

            for (int i = 0; i < 6; i++)

            {
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }
            Code_block.Text = pwd;
        }

        private void Code_Click(object sender, RoutedEventArgs e)

        {
            Code();
        }

        private void Input_Click(object sender, EventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Input_box.Text))
                errors.AppendLine("Вы ничего не ввели");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (Input_box.Text == Code_block.Text)
            {
                Hide();                
                Books books = new();
                books.ShowDialog();
                Close();
            }
            else
                MessageBox.Show("Вы неправильно ввели код. Попробуйте снова или измените код.");
        }
    }
}
