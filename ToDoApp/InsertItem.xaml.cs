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
    /// InsertItem.xaml etkileşim mantığı
    /// </summary>
    public partial class InsertItem : Window
    {
        TODOEntities db;
        int list_id;
        public InsertItem(int id)
        {
            InitializeComponent();
            list_id = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new TODOEntities();
                ITEM_INFO newItem = new ITEM_INFO()
                {
                    item_name = txtName.Text,
                    item_desc = txtDesc.Text,
                    item_deadline = datePicker.SelectedDate,
                    creation_date = DateTime.Now,
                    item_status = "Not Completed",
                    list_id = list_id
                };

                db.ITEM_INFO.Add(newItem);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                ItemPage.isExpired();
                ItemPage.dataGrid.ItemsSource = db.ITEM_INFO.Where(a => a.list_id == list_id).ToList();
                this.Hide();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException)
            {
                MessageBox.Show("Wrong");
            }
            
        }

        private void name_GotFocus(object sender, RoutedEventArgs e)
        {
            txtName.Text = "";
        }

        private void desc_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDesc.Text = "";
        }
    }
}
