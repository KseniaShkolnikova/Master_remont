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
    /// Логика взаимодействия для Diagnost.xaml
    /// </summary>
    public partial class Diagnost : Window
    {
        private Master_RemontEntities context = new Master_RemontEntities();
        public Diagnost()
        {
            InitializeComponent();
            combobox_status.ItemsSource = context.Statuses.ToList();
            combobox_status.DisplayMemberPath = "Names";
            combobox_part.ItemsSource = context.SpareParts.ToList();
            combobox_part.DisplayMemberPath = "SparePartsName";
            List<Orders> orders = new List<Orders>();
            foreach (var item in context.Orders)
            {
                if (item.Status_ID != 4)
                {
                    orders.Add(item);
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
                combobox_status.SelectedItem = selected.Statuses;
                combobox_part.SelectedItem = selected.SpareParts;
                description.Text = selected.Descriptionn;
            }
            catch { }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            var selected = datagrid.SelectedItem as Orders;
            selected.Statuses = combobox_status.SelectedItem as Statuses;
            selected.SpareParts = combobox_part.SelectedItem as SpareParts;
            selected.Descriptionn = description.Text;

            context.SaveChanges();
            List<Orders> orders = new List<Orders>();
            foreach (var item in context.Orders)
            {
                if (item.Status_ID != 4)
                {
                    orders.Add(item);
                }
            }
            datagrid.ItemsSource = orders;
            combobox_status.SelectedItem = null;
            combobox_part.SelectedItem=null;
            description.Text = null;
        }
    }
}
