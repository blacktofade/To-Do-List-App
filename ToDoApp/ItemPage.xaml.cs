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
    /// ItemPage.xaml etkileşim mantığı
    /// </summary>
    public partial class ItemPage : Window
    {
        TODOEntities db;
        public static DataGrid dataGrid;
        int list_id;
        
        public ItemPage(int id)
        {
            InitializeComponent();          
            list_id = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            InsertItem newItem = new InsertItem(list_id);
            newItem.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new TODOEntities();
            isExpired();
            dataGrid = myDataGrid2;
            dataGrid.ItemsSource = db.ITEM_INFO.Where(a => a.list_id == list_id).ToList();        
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new TODOEntities();
                dataGrid = myDataGrid2;
                int list_id = (dataGrid.SelectedItem as ITEM_INFO).list_id;
                string item_name = (dataGrid.SelectedItem as ITEM_INFO).item_name;
                DateTime creation_date = (myDataGrid2.SelectedItem as ITEM_INFO).creation_date;

                var deletedItem = db.ITEM_INFO.Where(
                    d => d.list_id == list_id && d.item_name == item_name && d.creation_date == creation_date)
                    .Single();

                db.ITEM_INFO.Remove(deletedItem);
                db.SaveChanges();
                isExpired();
                dataGrid.ItemsSource = db.ITEM_INFO.Where(a => a.list_id == list_id).ToList();
            }

            catch (Exception)
            {
                MessageBox.Show("You have to pick an item to delete");
            }
           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string filter = cmbFilter.Text;      
            db = new TODOEntities();        
            dataGrid = myDataGrid2;  
            if(filter=="Expired" || filter=="Complete" || filter =="Not Completed")
                dataGrid.ItemsSource = db.ITEM_INFO.Where(f => f.item_status == filter && f.list_id == list_id).ToList();
            else 
                dataGrid.ItemsSource = db.ITEM_INFO.Where(a => a.list_id == list_id).ToList();         
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string filterName = txtSearch.Text;
            db = new TODOEntities();
            dataGrid = myDataGrid2;
            dataGrid.ItemsSource = db.ITEM_INFO.Where(a => a.item_name == filterName && a.list_id == list_id).ToList();

        }

        public static void isExpired()
        {
            TODOEntities db;
            db = new TODOEntities();

            foreach (var item in db.ITEM_INFO)
            {
                if(item.item_deadline<DateTime.Now)
                {
                    item.item_status = "Expired";
                }
            }
            db.SaveChanges();
        }    

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {           
                string itemStatus = (sender as Button).Tag.ToString();
                if (itemStatus != "Expired")
                {
                    db = new TODOEntities();
                    dataGrid = myDataGrid2;
                    int list_id = (dataGrid.SelectedItem as ITEM_INFO).list_id;
                    string item_name = (dataGrid.SelectedItem as ITEM_INFO).item_name;
                    DateTime creation_date = (myDataGrid2.SelectedItem as ITEM_INFO).creation_date;

                    foreach (var item in db.ITEM_INFO)
                    {
                        if (item.item_name == item_name && item.list_id == list_id && item.creation_date == creation_date)
                        {
                            if (item.item_status == "Complete")
                                item.item_status = "Not Complete";
                            else if (item.item_status == "Not Complete")
                                item.item_status = "Complete";
                        }
                    }

                    db.SaveChanges();
                    dataGrid.ItemsSource = db.ITEM_INFO.Where(a => a.list_id == list_id).ToList();
                }
                else
                    MessageBox.Show("You can't change status of expired.");           
        }


        private void search_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
        }
    }
}
