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

namespace Master_Remont
{
    /// <summary>
    /// Логика взаимодействия для Master_po_remonty.xaml
    /// </summary>
    public partial class Master_po_remonty : Window
    {
        private Master_RemontEntities context = new Master_RemontEntities();
        public Master_po_remonty(string email)
        {
            InitializeComponent();
            List<Statuses> statuses = new List<Statuses>();
            foreach (var item in context.Statuses)
            {
                if (item.Names == "В ремонте" || item.Names == "Готов к выдаче")
                {
                    statuses.Add(item);
                }
            }
            combobox.ItemsSource =statuses;
            combobox.DisplayMemberPath = "Names";
            List<Orders> orders = new List<Orders>();
            foreach (var item in context.Orders)
            {
                if (item.Status_ID == 1 || item.Status_ID == 2 || item.Status_ID == 3)
                { 
                    if (item.Employees.Email == email)
                    {
                        orders.Add(item);
                    }
                }
            }
            datagrid.ItemsSource = orders;
        }


        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = datagrid.SelectedItem as Orders;
            try
            {
                combobox.SelectedItem = selected.Statuses;
                nomber.Text = selected.NumberOrder.ToString();
            }
            catch { }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            var selected = datagrid.SelectedItem as Orders;
            if (selected != null)
            {
                if (combobox.SelectedItem != null)
                {
                    selected.Statuses = combobox.SelectedItem as Statuses;

                    context.SaveChanges();
                    List<Orders> orders = new List<Orders>();
                    foreach (var item in context.Orders)
                    {
                        if (item.Status_ID == 1 || item.Status_ID == 2 || item.Status_ID == 3)
                        {
                            orders.Add(item);
                        }
                    }
                    datagrid.ItemsSource = orders;
                    combobox.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены","Не удалось сохранить");
                }
            }
            else
            {
                MessageBox.Show("Сначала необходимо выбрать запись из списка", "Не удалось сохранить");
            }
        }
    }
}
