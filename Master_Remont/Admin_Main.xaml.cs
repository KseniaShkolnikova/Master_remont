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
using System.Data.Entity.ModelConfiguration.Configuration;

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
                    orders.Add(item);
                }
            }
            context.SaveChanges();
            datagrid.ItemsSource = orders;
        }
        public int Generate()
        {
            int number = 0;
            while (true)
            {
                bool result = true;
                Random random = new Random();
                number = random.Next(100000, 1000000);
                foreach (var item in context.Orders)
                {
                    if (number.ToString() == item.NumberOrder)
                    {
                        result = false;
                        break;
                    }
                }
                if (result == true)
                {
                    break;
                }
            }
            return number;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if(combobox_client.SelectedItem != null && combobox_equpment.SelectedItem != null && combobox_employee.SelectedItem != null && combobox_type_equpment.SelectedItem != null)
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
                order.ReceptionDate = DateTime.Now.ToString("dd-MM-yyyy");
                order.NumberOrder = Generate().ToString();
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
                combobox_client.SelectedItem = null;
                combobox_equpment.SelectedItem = null;
                combobox_employee.SelectedItem = null;
                combobox_type_equpment.SelectedItem = null;
            }
            else
            {
                MessageBox.Show("Не все поля заполнены", "Не удалось добавить заказ");
            }
        }
    }
}
