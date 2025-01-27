using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
using System.IO;  
namespace Master_Remont
{
    /// <summary>
    /// Логика взаимодействия для Admin_Status.xaml
    /// </summary>
    public partial class Admin_Status : Page
    {
        private Master_RemontEntities context = new Master_RemontEntities();
        public Admin_Status()
        {
            InitializeComponent();
            List<Statuses> statuses = new List<Statuses>();
            foreach (var item in context.Statuses)
            {
                if (item.Names == "Завершен" || item.Names == "Отменен")
                {
                    statuses.Add(item);
                }
            }
            combobox.ItemsSource = statuses;
            combobox.DisplayMemberPath = "Names";
            List<Orders> orders = new List<Orders>();
            foreach (var item in context.Orders)
            {
                if (item.Status_ID != 4 && item.Status_ID != 5 && item.Status_ID != 6)
                {
                    orders.Add(item);
                }
            }
            datagrid.ItemsSource = orders;
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = datagrid.SelectedItem as Orders;
            try
            {
                nomber.Text = selected.NumberOrder.ToString();
            }
            catch { }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            var selected = datagrid.SelectedItem as Orders;
            if (selected != null)
            {
                selected.Statuses = combobox.SelectedItem as Statuses;
                if (selected.Statuses != null)
                {
                    combobox.SelectedItem = null;
                    nomber.Text = null;
                    if (selected.Statuses.Names == "Завершен")
                    {
                        if(selected.SpareParts != null)
                        {
                            selected.RepairCost = 1000 + selected.SpareParts.Price * 2;
                        }
                        else
                        {
                            selected.RepairCost =1000;
                        }
                        GeneratePdfReceipt(selected);
                        context.SaveChanges();
                        List<Orders> orders = new List<Orders>();
                        foreach (var item in context.Orders)
                        {
                            if (item.Status_ID != 4 && item.Status_ID != 5 && item.Status_ID != 6)
                            {
                                orders.Add(item);
                            }
                        }
                        datagrid.ItemsSource = orders;
                    }

                }
                else
                {
                    MessageBox.Show("Сначала необходимо выбрать запись из списка", "Ошибка изменения статуса");
                }

            }
            else
            {
                MessageBox.Show("Сначала необходимо выбрать запись из списка", "Ошибка изменения статуса");
            }
        }
        private void GeneratePdfReceipt(Orders order)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = $"Receipt_{order.NumberOrder}.pdf";
            string filePath = System.IO.Path.Combine(desktopPath, fileName);
            PdfDocument pdf = new PdfDocument();
            PdfPage page = pdf.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont titleFont = new XFont("Arial", 18);
            XFont contentFont = new XFont("Arial", 12);
            XFont repaircost = new XFont("Arial", 14, XFontStyleEx.Bold);
            double margin = 40;
            double yPosition = margin;
            gfx.DrawString($"Номер заказа: {order.NumberOrder}", titleFont, XBrushes.Black, new XRect(margin, yPosition, page.Width - 2 * margin, page.Height), XStringFormats.TopCenter);
            yPosition += 40;
            gfx.DrawString($"Клиент: {order.Clients.Surname} {order.Clients.Names} {order.Clients.Middlename}", contentFont, XBrushes.Black, margin, yPosition);
            yPosition += 20;
            gfx.DrawString($"Контактные данные клиента: {order.Clients.Email}", contentFont, XBrushes.Black, margin, yPosition);
            yPosition += 20;
            gfx.DrawString($"Мастер: {order.Employees.Surname} {order.Employees.Names} {order.Employees.Middlename}", contentFont, XBrushes.Black, margin, yPosition);
            yPosition += 20;
            gfx.DrawString($"Контактные данные мастера: {order.Employees.Email}", contentFont, XBrushes.Black, margin, yPosition);
            yPosition += 20;
            gfx.DrawString($"Дата начала заказа: {order.ReceptionDate}", contentFont, XBrushes.Black, margin, yPosition);
            yPosition += 20;
            gfx.DrawString($"Дата завершения заказа: {DateTime.Now.ToString("dd-MM-yyyy")}", contentFont, XBrushes.Black, margin, yPosition);
            yPosition += 20;
            gfx.DrawString($"Техника: {order.Equipments.Brand} {order.Equipments.Model}", contentFont, XBrushes.Black, margin, yPosition);
            yPosition += 20;
            if(order.Descriptionn == null)
            {
                gfx.DrawString($"Поломка: незначительная", contentFont, XBrushes.Black, margin, yPosition);
            }
            else
            {
                gfx.DrawString($"Поломка: {order.Descriptionn}", contentFont, XBrushes.Black, margin, yPosition);
            }
            yPosition += 30;
            gfx.DrawString($"Итоговая сумма заказа: {order.RepairCost.ToString()} рублей", repaircost, XBrushes.Black, margin, yPosition);

            pdf.Save(filePath);
            MessageBox.Show($"Квитанция была сохранена на рабочем столе {filePath}", "Создание квитанции"); 


        }

    }
}
