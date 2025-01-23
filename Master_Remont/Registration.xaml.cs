using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private Master_RemontEntities context = new Master_RemontEntities();
        public Registration()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Hide();
        }
        private static bool ValidationPassword (string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$";
            return Regex.IsMatch(password, pattern);
        }

        private void Registrations_Click(object sender, RoutedEventArgs e)
        {
            if (email.Text != "" && password.Password != "" && password2.Password != "" && surname.Text != "" && name.Text != "" && numberphone.Text != "")
            {
                if ( email.Text.Contains("@") && email.Text.Contains("."))
                {
                    if (ValidationPassword(password.Password))
                    {
                        if (password.Password.Length > 8)
                        {
                            if (password.Password == password2.Password)
                            {
                                bool validation = true;
                                foreach (var item in context.Clients)
                                {
                                    if (password.Password == item.Pasword)
                                    {
                                        validation = false;
                                        break;
                                    }
                                }
                                foreach (var item1 in context.Employees)
                                {
                                    if (password.Password == item1.Pasword)
                                    {
                                        validation = false;
                                        break;
                                    }
                                }
                                if (validation == true)
                                {
                                    validation = false;
                                    foreach (char c in numberphone.Text)
                                    {
                                        if (char.IsDigit(c) == false)
                                        {
                                            MessageBox.Show("Номер телефона должен содержать только цыфры", "Неверный номер телефона");
                                            break;
                                        }
                                        else
                                        {
                                            validation = true;
                                        }
                                    }
                                    if (validation == true)
                                    {
                                        if (numberphone.Text.StartsWith("89"))
                                        {
                                            if (numberphone.Text.Length == 11)
                                            {
                                                Clients clients = new Clients();
                                                clients.Email = email.Text;
                                                clients.Surname = surname.Text;
                                                clients.PhoneNumber = numberphone.Text;
                                                clients.Names = name.Text;
                                                clients.Middlename = middlename.Text;
                                                clients.Pasword = password.Password.ToString();
                                                context.Clients.Add(clients);
                                                context.SaveChanges();
                                                Client_MainWindow mainWindow = new Client_MainWindow(clients);
                                                mainWindow.Show();
                                                this.Hide();
                                            }
                                            else
                                            {
                                                MessageBox.Show("Номер телефона должен содержать 11 цыфр (начинается с 8)", "Неверный номер телефона");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Номер телефона должен начниаться с 89", "Неверный номер телефона");
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Ваша почта не уникальна", "Неверная почта");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Пароли не совпадают", "Неверный пароль");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пароль должен содержать более 8 символов", "Неверный пароль");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароль должен содержать строчные и заглавные латинские буквы, цыфры", "Неверный пароль");
                    }

                }
                else
                {
                    MessageBox.Show("Убедитесь, что почта содержит @ и .", "Почта введена неверно");
                }

            }
            else
            {
                MessageBox.Show("Убедитесь, что все поля заполнены", "Не все поля заполнены");
            }
        }
    }
}
