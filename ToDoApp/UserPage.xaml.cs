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
    /// UserPage.xaml etkileşim mantığı
    /// </summary>
    public partial class UserPage : Window
    {
        TODOEntities db;
        public static DataGrid dataGrid;
        string myUser;
        public UserPage(string userName)
        {

            InitializeComponent();
            myUser = userName;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new TODOEntities();          
            dataGrid = myDataGrid;
            dataGrid.ItemsSource = db.LIST_INFO.Where(a => a.list_owner == myUser).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddList newList = new AddList(myUser);
            newList.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
           
                db = new TODOEntities();
                dataGrid = myDataGrid;

                int list_id = (dataGrid.SelectedItem as LIST_INFO).list_id;
                var deletedList = db.LIST_INFO.Where(d => d.list_id == list_id).Single();
                db.LIST_INFO.Remove(deletedList);              
                foreach (var item in db.ITEM_INFO)
                {
                    if(item.list_id == list_id)
                    {
                        db.ITEM_INFO.Remove(item);
                    }
                }

                db.SaveChanges();
                dataGrid.ItemsSource = db.LIST_INFO.Where(a => a.list_owner == myUser).ToList();
                    
            
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                int list_id = (myDataGrid.SelectedItem as LIST_INFO).list_id;

                ItemPage items = new ItemPage(list_id);
                items.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("You can't choose a list that doesn't exist.");
            }
           
        }

       
    }
}
