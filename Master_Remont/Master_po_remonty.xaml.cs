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
        public Master_po_remonty()
        {
            InitializeComponent();
            datagrid.ItemsSource = context.Orders.ToList();
        }


        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
