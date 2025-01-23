using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace Master_Remont
{
    /// <summary>
    /// Логика взаимодействия для Admin_Main.xaml
    /// </summary>
    public partial class Admin_Main : Page
    {
        private Master_RemontEntities context = new Master_RemontEntities();
        public Admin_Main()
        {
            InitializeComponent();
            combobox_client.ItemsSource = context.Clients.ToList();
            combobox_client.DisplayMemberPath = "Email";
            List<Employees> employees = new List<Employees>();
            foreach (var item in context.Employees)
            {
                if (item.Specializations.Names == "Мастер по ремонту")
                {
                    employees.Add(item);
                }
            }
            combobox_employee.ItemsSource = employees;
            combobox_employee.DisplayMemberPath = "Email";
            combobox_equpment.ItemsSource = context.Equipments.ToList();
            combobox_equpment.DisplayMemberPath = "Brand";
            combobox_type_equpment.ItemsSource = context.EquipmentTypes.ToList();
            combobox_type_equpment.DisplayMemberPath = "Names";
            List<Orders> orders = new List<Orders>();
            foreach (var item in context.Orders)
            {
                if (item.Status_ID == 4 || item.Status_ID == 5)
                {
                    if (item.Status_ID == 4)
                    {
                        item.RepairCost = 1000 + item.SpareParts.Price * 2;
                    }
                    orders.Add(item);
                }
            }
            context.SaveChanges();
            datagrid.ItemsSource = orders;
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Orders order = new Orders();
            order.Clients = combobox_client.SelectedItem as Clients;
            order.Equipments = combobox_equpment.SelectedItem as Equipments;
            foreach (var item in context.Statuses)
            {
                if (item.Names == "Зарегестрирован")
                {
                    order.Statuses = item;
                }
            }
            order.Employees = combobox_employee.SelectedItem as Employees;
            order.EquipmentTypes = combobox_type_equpment.SelectedItem as EquipmentTypes;
            order.ReceptionDate = DateTime.Now.ToString("dd-mm-yyyy");
            if (number.Text.Length == 6)
            {
                order.NumberOrder = number.Text;
                context.Orders.Add(order);
                context.SaveChanges();
                List<Orders> orders = new List<Orders>();
                foreach (var item in context.Orders)
                {
                    if (item.Status_ID == 4 || item.Status_ID == 5)
                    {
                        orders.Add(item);
                    }
                }
                datagrid.ItemsSource = orders;
            }
            else
            {
                MessageBox.Show("Номер заказа должен содержать 6 цыфр", "Не удлось добавить заказ");
            }
            

        }
    }
}
