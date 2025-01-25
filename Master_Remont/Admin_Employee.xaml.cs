using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Master_Remont
{
    /// <summary>
    /// Логика взаимодействия для Admin_Employee.xaml
    /// </summary>
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

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Employees employees = new Employees();
            employees.Names = name.Text;
            employees.Surname = surname.Text;
            employees.Middlename = middlename.Text;
            employees.Email = email.Text;
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

        private void update_Click(object sender, RoutedEventArgs e)
        {
            var selexted = datagrid.SelectedItem as Employees;
            selexted.Names = name.Text;
            selexted.Middlename = middlename.Text;
            selexted.Email = email.Text;
            selexted.Surname = surname.Text;
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
                combobox_for_pozition.SelectedItem = selexted.Specializations;
            }
            catch { }
        }
    }
}
