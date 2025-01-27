using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Master_Remont
{
    /// <summary>
    /// Логика взаимодействия для Admin_Employee.xaml
    /// </summary>
    /// проверки на пцстые строки, кнопки, кол-во символов, пароль и тд.
    public partial class Admin_Employee : Page
    {
        private Master_RemontEntities context = new Master_RemontEntities();
        public Admin_Employee()
        {
            InitializeComponent();
            datagrid.ItemsSource = context.Employees.ToList();
            combobox_for_pozition.ItemsSource = context.Positions.ToList();
            combobox_for_pozition.DisplayMemberPath = "Names";
            combobox_specialization.ItemsSource = context.Specializations.ToList();
            combobox_specialization.DisplayMemberPath = "Names";
        }
        private static bool ValidationPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$";
            return Regex.IsMatch(password, pattern);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text != "" && surname.Text != "" && email.Text != "" && password.Password != "" && combobox_for_pozition.SelectedItem!= null && combobox_specialization.SelectedItem != null)
            {
                Employees employees = new Employees();
                employees.Names = name.Text;
                employees.Surname = surname.Text;
                employees.Middlename = middlename.Text;
                bool valid = true;
                foreach (var item in context.Clients)
                {
                    if (item.Email == email.Text)
                    {
                        valid = false;
                    }
                }
                foreach (var item in context.Employees)
                {
                    if (item.Email == email.Text)
                    {
                        valid = false;
                    }
                }
                if (valid == true)
                {
                    if (email.Text.Contains("@") && email.Text.Contains("."))
                    {
                        employees.Email = email.Text;
                        if (password.Password.Length >8 && ValidationPassword(password.Password))
                        {
                            employees.Pasword = password.Password;
                            employees.Positions = combobox_for_pozition.SelectedItem as Positions;
                            employees.Specializations = combobox_specialization.SelectedItem as Specializations;
                            context.Employees.Add(employees);
                            context.SaveChanges();
                            datagrid.ItemsSource = context.Employees.ToList();
                            name.Text = null;
                            surname.Text = null;
                            middlename.Text = null;
                            email.Text = null;
                            combobox_for_pozition.SelectedItem = null;
                            combobox_specialization.SelectedItem = null;
                            password.Password = null;
                        }
                        else
                        {
                            MessageBox.Show("Пароль должен содержать строчные и заглавные латинские буквы, цыфры", "Не удалось добавить сотрудника");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Почта должна содержать @ и .", "Не удалось добавить сотрудника");
                    }
                }
                else
                {
                    MessageBox.Show("Почта не уникальна", "Не удалось добавить сотрудника");
                }
            }
            else
            {
                MessageBox.Show("Не все необходимые поля заполнены", "Не удалось добавить сотрудника");
            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text != "" && surname.Text != "" && email.Text != "" && password.Password != "" && combobox_for_pozition.SelectedItem!= null && combobox_specialization.SelectedItem != null)
            {
                var selexted = datagrid.SelectedItem as Employees;
                selexted.Names = name.Text;
                selexted.Middlename = middlename.Text;
                bool valid = true;
                foreach (var item in context.Clients)
                {
                    if (item.Email == email.Text)
                    {
                        valid = false;
                    }
                }
                foreach (var item in context.Employees)
                {
                    if (item.Email == email.Text)
                    {
                        valid = false;
                    }
                }
                if (selexted.Email == email.Text)
                {
                    valid = true;
                }
                if (valid == true)
                {
                    selexted.Email = email.Text;
                    selexted.Surname = surname.Text;
                    if (password.Password.Length > 8 && ValidationPassword(password.Password))
                    {
                        selexted.Pasword = password.Password;
                        selexted.Positions = combobox_for_pozition.SelectedItem as Positions;
                        selexted.Specializations = combobox_specialization.SelectedItem as Specializations;
                        context.SaveChanges();
                        datagrid.ItemsSource = context.Employees.ToList();
                        name.Text = null;
                        surname.Text = null;
                        middlename.Text = null;
                        email.Text = null;
                        combobox_for_pozition.SelectedItem = null;
                        combobox_specialization.SelectedItem = null;
                        password.Password = null;
                    }
                    else
                    {
                        MessageBox.Show("Пароль должен содержать строчные и заглавные латинские буквы, цыфры", "Не удалось изменить сотрудника");
                    }
                }
                else
                {
                    MessageBox.Show("Почта не уникальна", "не удалось изменить сотрудника");
                }
            }
            else
            {
                MessageBox.Show("Не все необходимые поля заполнены", "Не удалось изменить сотрудника");
            }
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selexted = datagrid.SelectedItem as Employees;
            try
            {
                name.Text = selexted.Names;
                surname.Text = selexted.Surname;
                middlename.Text = selexted.Middlename;
                password.Password = selexted.Pasword;
                email.Text = selexted.Email;
                combobox_specialization.SelectedItem = selexted.Specializations;
                combobox_for_pozition.SelectedItem = selexted.Positions;
            }
            catch { }
        }
    }
}
