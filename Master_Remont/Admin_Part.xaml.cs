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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Master_Remont
{
    /// Проверки на нажатия кнопок, на децимал (2 знака полсе , ), применение 2 фильиров одновременно
    public partial class Admin_Part : Page
    {
        private Master_RemontEntities context = new Master_RemontEntities();
        public Admin_Part()
        {
            InitializeComponent();
            datagrid.ItemsSource = context.SpareParts.ToList();
            List<string> prise = new List<string>();
            prise.Add("По возрастанию");
            prise.Add("По убыванию");
            combobox_for_price.ItemsSource = prise;
            combobox_for_manufacture.ItemsSource = context.SpareParts.ToList();
            combobox_for_manufacture.DisplayMemberPath = "Manufacturer";
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            if ( name.Text != "" && manufacture.Text != "" && price.Text != "" && stock.Text != "")
            {
                bool valid1 = false;
                bool valid2 = false;
                foreach (var item in price.Text) {
                    if (char.IsDigit(item) == false && item != ',')
                    {
                        MessageBox.Show("Поле для заполнения цены может содержать только число","Ошибка создания");
                        break;
                    }
                    else
                    {
                        valid1 = true;
                    }
                }
                foreach (var item in price.Text)
                {
                    if (char.IsDigit(item) == false && item != ',')
                    {
                        if (price.Text.Contains(','))
                        {
                            var parts = price.Text.Split(',');
                            if (parts.Length > 1 && parts[1].Length != 2)
                            {
                                MessageBox.Show("После запятой должно быть ровно две цифры", "Не удалось добавить запчасть");
                                break;
                            }
                            else
                            {
                                valid1 = true; 
                            }
                        }
                        else
                        {
                            valid1 = true; 
                        }
                    }
                    else
                    {
                        valid1 = true;
                    }
                }

                foreach (var item in stock.Text)
                {
                    if (char.IsDigit(item) == false)
                    {
                        MessageBox.Show("Поле для заполнения кол-ва на складе может содержать только число", "Ошибка создания");
                        break;
                    }
                    else
                    {
                        valid2 = true;
                    }
                }
                if( valid1 == true && valid2 == true)
                {
                    SpareParts spareParts = new SpareParts();
                    spareParts.SparePartsName = name.Text;
                    spareParts.Manufacturer = manufacture.Text;
                    spareParts.Price = Convert.ToDecimal(price.Text);
                    spareParts.QuantityInStock = Convert.ToInt16(stock.Text);
                    bool valid = true;
                    foreach (var item in context.SpareParts)
                    {
                        if (item.SparePartsName == spareParts.SparePartsName && item.Manufacturer == spareParts.Manufacturer)
                        {
                            valid = false;
                            break;
                        }

                    }
                    if (valid == true)
                    {
                        context.SpareParts.Add(spareParts);
                        context.SaveChanges();
                        datagrid.ItemsSource = context.SpareParts.ToList();
                        name.Text = null;
                        manufacture.Text = null;
                        stock.Text = null;
                        price.Text = null;
                        combobox_for_manufacture.ItemsSource = context.SpareParts.ToList();
                        combobox_for_manufacture.DisplayMemberPath = "Manufacturer";
                    }
                    else
                    {
                        MessageBox.Show("Такая деталь уже существует", "Не удалось добавить");
                    }
                }

            }
            else
            {
                MessageBox.Show("Не все поля заполнены", "Не удалось добавить");
            }

        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text != "" && manufacture.Text != "" && price.Text != "" && stock.Text != "")
            {
                bool valid1 = false;
                bool valid2 = false;
                foreach (var item in price.Text)
                {
                    if (char.IsDigit(item) == false && item != ',')
                    {
                        MessageBox.Show("Поле для заполнения цены может содержать только число", "Ошибка создания");
                        break;
                    }
                    else
                    {
                        valid1 = true;
                    }
                }
                foreach (var item in price.Text)
                {
                    if (char.IsDigit(item) == false && item != ',')
                    {
                        if (price.Text.Contains(','))
                        {
                            var parts = price.Text.Split(',');
                            if (parts.Length > 1 && parts[1].Length != 2)
                            {
                                MessageBox.Show("После запятой должно быть ровно две цифры", "Не удалось добавить запчасть");
                                break;
                            }
                            else
                            {
                                valid1 = true;
                            }
                        }
                        else
                        {
                            valid1 = true;
                        }
                    }
                    else
                    {
                        valid1 = true;
                    }
                }
                foreach (var item in stock.Text)
                {
                    if (char.IsDigit(item) == false)
                    {
                        MessageBox.Show("Поле для заполнения кол-ва на складе может содержать только число", "Ошибка изменения");
                        break;
                    }
                    else
                    {
                        valid2 = true;
                    }
                }
                if (valid1 == true && valid2==true)
                {
                    bool valid = true;
                    foreach (var item in context.SpareParts)
                    {
                        if (item.SparePartsName == name.Text && item.Manufacturer == manufacture.Text && item.Price.ToString() == price.Text && item.QuantityInStock.ToString() == stock.Text)
                        {
                            valid = false;
                            break;
                        }

                    }
                    if (valid == true)
                    {
                        var selexted = datagrid.SelectedItem as SpareParts;
                        selexted.SparePartsName = name.Text;
                        selexted.Manufacturer = manufacture.Text;
                        selexted.Price = Convert.ToDecimal(price.Text);
                        selexted.QuantityInStock = Convert.ToInt16(stock.Text);
                        context.SaveChanges();
                        datagrid.ItemsSource = context.SpareParts.ToList();
                        name.Text = null;
                        manufacture.Text = null;
                        stock.Text = null;
                        price.Text = null;
                        combobox_for_manufacture.ItemsSource = context.SpareParts.ToList();
                        combobox_for_manufacture.DisplayMemberPath = "Manufacturer";
                    }
                    else
                    {
                        MessageBox.Show("Такая деталь уже существует", "Не удалось изменить");
                    }
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены", "Не удвлось изменить");
            }
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selexted = datagrid.SelectedItem as SpareParts;
            try
            {
                name.Text = selexted.SparePartsName;
                price.Text = selexted.Price.ToString();
                stock.Text = selexted.QuantityInStock.ToString();
                manufacture.Text = selexted.Manufacturer;
            }
            catch { }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selexted = datagrid.SelectedItem as SpareParts;
                if (selexted != null)
                {
                    context.SpareParts.Remove(selexted);
                    context.SaveChanges();
                    datagrid.ItemsSource = context.SpareParts.ToList();
                    name.Text = null;
                    manufacture.Text = null;
                    stock.Text = null;
                    price.Text = null;
                    combobox_for_manufacture.ItemsSource = context.SpareParts.ToList();
                    combobox_for_manufacture.DisplayMemberPath = "Manufacturer";
                }
                else
                {
                    MessageBox.Show("Сначала выберите запчасть", "Не удалось удалить");
                }
            }
            catch 
            {
                MessageBox.Show("Данную запчасть нельзя удвлить, так как она уже используется в заказе", "Не удалось удалить");
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            combobox_for_price.SelectedItem = null;
            combobox_for_manufacture.SelectedItem = null;
            datagrid.ItemsSource = context.SpareParts.ToList() ;
        }

        private void combobox_for_price_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combobox_for_price.SelectedItem != null)
            {
                List<SpareParts> spareParts = context.SpareParts.ToList();
                List<SpareParts> spareParts_filtr;

                if (combobox_for_price.SelectedItem == "По возрастанию")
                {
                    spareParts_filtr = spareParts.OrderBy(sp => sp.Price).ToList();
                    datagrid.ItemsSource = spareParts_filtr;
                }
                else if (combobox_for_price.SelectedItem == "По убыванию")
                {
                    spareParts_filtr = spareParts.OrderByDescending(sp => sp.Price).ToList();
                    datagrid.ItemsSource = spareParts_filtr;
                }
                else
                {
                    spareParts_filtr = spareParts;
                }
            }
            
        }

        private void combobox_for_manufacture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = combobox_for_manufacture.SelectedItem as SpareParts;
            if (selected != null)
            {
                List<SpareParts> spareParts_filtr = new List<SpareParts>();
                foreach (var item in context.SpareParts)
                {
                    if (item.Manufacturer == selected.Manufacturer)
                    {
                        spareParts_filtr.Add(item);
                    }
                }
                datagrid.ItemsSource = spareParts_filtr;
            }
        }

    }
}
