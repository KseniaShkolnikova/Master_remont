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
    /// Логика взаимодействия для Administration_Navigation.xaml
    /// </summary>
    public partial class Administration_Navigation : Window
    {
        public Administration_Navigation()
        {
            InitializeComponent();
            frame.Content = new Admin_Main();
        }

        private void Mainbtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Admin_Main();
        }

        private void Clienttbtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Admin_Clients();
        }

        private void Partbtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Admin_Part();
        }

        private void Employeebtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Admin_Employee();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void Status_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Admin_Status();
        }
    }
}
