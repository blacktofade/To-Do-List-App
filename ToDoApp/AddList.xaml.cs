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
    /// AddList.xaml etkileşim mantığı
    /// </summary>
    public partial class AddList : Window
    {
        TODOEntities db;
        string myUser;
        public AddList(string id)
        {
            InitializeComponent();
            myUser = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (txtList.Text == "")
                MessageBox.Show("Name of the list can not be empty.");
            else
            { 
                db = new TODOEntities();
                LIST_INFO newList = new LIST_INFO()
                {
                    list_name = txtList.Text,
                    list_owner = myUser
                };

                db.LIST_INFO.Add(newList);
                db.SaveChanges();
                UserPage.dataGrid.ItemsSource = db.LIST_INFO.Where(a => a.list_owner == myUser).ToList();
                this.Hide();
            }
            
        }
        
    }
}
