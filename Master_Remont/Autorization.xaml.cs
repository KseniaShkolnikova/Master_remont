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
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        private Master_RemontEntities context = new Master_RemontEntities();
        public Autorization()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void Autorizationbtn_Click(object sender, RoutedEventArgs e)
        {
            bool verification = false;
            foreach (var item in context.Clients)
            {
                if (email.Text == item.Email && password.Password == item.Pasword)
                {
                    Client_MainWindow mainWindow = new Client_MainWindow(item);
                    mainWindow.Show();
                    this.Hide();
                    verification = true;
                }
            }
            foreach (var item in context.Employees)
            {
                if (email.Text == item.Email && password.Password == item.Pasword)
                {
                    if (item.Specialization_ID == 1)
                    {
                        Administration_Navigation admin = new Administration_Navigation();
                        admin.Show();
                        this.Hide();
                    }
                    else if (item.Specialization_ID == 2)
                    {

                    }
                    else if (item.Specialization_ID == 3)
                    {
                        Diagnost diagnost = new Diagnost();
                        diagnost.Show();
                        this.Hide();
                    }
                    else if(item.Specialization_ID == 4)
                    {
                        Master_po_remonty master_Po_Remonty = new Master_po_remonty();
                        master_Po_Remonty.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Ваша должность не была определена. Попробуйте еще раз или обратитесь в поддержку", "Не удалось войти");
                        break;
                    }
                    verification = true;
                }

            }
            if (verification == false) 
            {
                MessageBox.Show("Возможно пороль или почта указаны неверно","Не удалось войти");
                email.Text = null; password.Password = null;
            }

        }
    }
}
