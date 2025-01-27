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
            List<Statuses> statuses = new List<Statuses>();
            foreach (var item in context.Statuses)
            {
                if (item.Names == "На диагностике" || item.Names == "В ремонте")
                {
                    statuses.Add(item);
                }
            }
            combobox_status.ItemsSource = statuses;
            combobox_status.DisplayMemberPath = "Names";
            combobox_part.ItemsSource = context.SpareParts.ToList();
            combobox_part.DisplayMemberPath = "SparePartsName";
            List<Orders> orders = new List<Orders>();
            foreach (var item in context.Orders)
            {
                if (item.Status_ID == 1 || item.Status_ID == 5 )
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
                nomber.Text = selected.NumberOrder.ToString();
            }
            catch { }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            var selected = datagrid.SelectedItem as Orders;
            if (selected != null)
            {
                if (combobox_status.SelectedItem != null && combobox_part.SelectedItem!= null && description!= null)
                {
                    selected.Statuses = combobox_status.SelectedItem as Statuses;
                    bool valid = false;
                    SpareParts spareParts = null;
                    foreach (var item in context.SpareParts)
                    {
                        if (item.SparePartsName == combobox_part.Text)
                        {
                            spareParts = item;
                            valid = true;                  
                        }
                    }
                    if (valid == true)
                    {
                        int z = spareParts.QuantityInStock;
                        if (z > 0)
                        {
                            spareParts.QuantityInStock = z - 1;
                            selected.SpareParts = combobox_part.SelectedItem as SpareParts;
                            selected.Descriptionn = description.Text;

                            context.SaveChanges();
                            List<Orders> orders = new List<Orders>();
                            foreach (var item1 in context.Orders)
                            {
                                if (item1.Status_ID == 1 || item1.Status_ID == 5)
                                {
                                    orders.Add(item1);
                                }
                            }
                            datagrid.ItemsSource = orders;
                            combobox_status.SelectedItem = null;
                            combobox_part.SelectedItem = null;
                            description.Text = null;
                        }
                        else
                        {
                            MessageBox.Show("На складе нет запчасти", "Не уалось сохранить");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Деталь не найдена", "Не уалось сохранить");
                    }
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены","Не уалось сохранить");
                }
            }
            else
            {
                MessageBox.Show("Сначала выберите запись из списка", "Не уалось обновить");
            }
        }
            
    }
}

