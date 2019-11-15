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

namespace ToDoApp
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        TODOEntities db;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            RegisterPage registerPage = new RegisterPage(); 
            registerPage.ShowDialog();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            int flag = 0;
            string myUser="";
            db = new TODOEntities();
            foreach (var user in db.USER_INFO)
            {
                if (user.username == txtName.Text && user.user_password == txtPass.Text)
                {
                    flag = 1;
                    myUser = user.username;
                }
                                                                        
                
            }
            if (flag == 1)
            {               

                UserPage userPage = new UserPage(myUser);
                userPage.ShowDialog();             
            }
            else
                MessageBox.Show("You entered wrong informations");
          
        }

        private void name_GotFocus(object sender, RoutedEventArgs e)
        {
            txtName.Text = "";
        }
        private void pass_GotFocus(object sender, RoutedEventArgs e)
        {       
            txtPass.Text = "";

        }
    }
}
