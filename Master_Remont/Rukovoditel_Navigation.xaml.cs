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
    /// Логика взаимодействия для Rukovoditel_Navigation.xaml
    /// </summary>
    public partial class Rukovoditel_Navigation : Window
    {
        public Rukovoditel_Navigation()
        {
            InitializeComponent();
            frame.Content = new Rukovoditel_Main();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void Mainbtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Rukovoditel_Main();
        }

        private void otchet_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Rukovoditel_Otcheti();
        }
    }
}
