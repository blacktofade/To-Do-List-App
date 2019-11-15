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

namespace ToDoApp
{
    /// <summary>
    /// RegisterPage.xaml etkileşim mantığı
    /// </summary>
    public partial class RegisterPage : Window
    {
        TODOEntities db;
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                db = new TODOEntities();

                USER_INFO newUser = new USER_INFO()
                {
                    mail_adress = txtMail.Text,
                    username = txtNick.Text,
                    full_name = txtName.Text,
                    surname = txtSurname.Text,
                    user_password = txtPass.Text
                };
                db.USER_INFO.Add(newUser);
                db.SaveChanges();

                MessageBox.Show("You've just registrated !");

                this.Hide();
            }

            catch (Exception)
            {
                MessageBox.Show("The fields can not be blank.");
            }


        }

        private void name_GotFocus(object sender, RoutedEventArgs e)
        {
            txtName.Text = "";
        }
        private void surname_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSurname.Text = "";
        }
        private void mail_GotFocus(object sender, RoutedEventArgs e)
        {
            txtMail.Text = "";
        }
        private void nick_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNick.Text = "";
        }
        private void pass_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPass.Text = "";
        }

    }
}
