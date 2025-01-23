using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Master_Remont
{
    /// <summary>
    /// Логика взаимодействия для Client_MainWindow.xaml
    /// </summary>
    public partial class Client_MainWindow : Window
    {
        private Master_RemontEntities context = new Master_RemontEntities();
        public Client_MainWindow(Clients clients)
        {
            InitializeComponent();
            List<Orders> orders = new List<Orders>();
            foreach (var item in context.Orders)
            {
                if (item.Client_ID == clients.ID_Client)
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
    }
}
