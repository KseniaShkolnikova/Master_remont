using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Windows;
using System;
using System.Windows.Controls;

namespace Master_Remont
{
    public partial class Rukovoditel_Otcheti : Page
    {
        private Master_RemontEntities context = new Master_RemontEntities();
        private List<dynamic> mostFrequentTypes;
        private List<dynamic> mostUsedSpareParts;

        public Rukovoditel_Otcheti()
        {
            InitializeComponent();
            LoadData();
        }

        private string GetShortDateString(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }
            try
            {
                DateTime receptionDate = DateTime.ParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                return receptionDate.ToString("dd.MM.yyyy");
            }
            catch (FormatException)
            {
                return null;
            }
        }

        private DateTime? ParseDate(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }
            try
            {
                return DateTime.ParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return null;
            }
        }

        private void LoadData()
        {
            var orders = context.Orders
                .Include("Statuses")
                .Include("SpareParts")
                .Include("EquipmentTypes")
                .Include("Employees")
                .Where(o => o.Statuses.Names == "Завершен").ToList();

            var query = orders.AsEnumerable()
                .Select(order => new
                {
                    order.NumberOrder,
                    order.ReceptionDate,
                    Revenue = order.RepairCost - (order.Part_ID.HasValue ? order.SpareParts.Price : 0),
                    EquipmentType = order.EquipmentTypes?.Names,
                    SparePartsNames = order.Part_ID.HasValue ? order.SpareParts.SparePartsName : null,
                    Master = order.Employees?.Email,
                    MasterName = $"{order.Employees?.Surname} {order.Employees?.Names} {order.Employees?.Middlename}"
                });

            var frequentTypes = query.Where(x => x.EquipmentType != null)
                 .GroupBy(x => x.EquipmentType)
                 .Select(group => new { Type = group.Key, Count = group.Count() })
                 .OrderByDescending(x => x.Count)
                 .ToList();
            mostFrequentTypes = frequentTypes.Cast<dynamic>().ToList();

            var usedSpareParts = query
                .Where(x => !string.IsNullOrEmpty(x.SparePartsNames))
                .GroupBy(x => x.SparePartsNames)
                .Select(group => new { SparePartName = group.Key, Count = group.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();
            mostUsedSpareParts = usedSpareParts.Cast<dynamic>().ToList();

            var result = query
                .Select(item => new
                {
                    НомерЗаказа = item.NumberOrder,
                    ДатаПриема = GetShortDateString(item.ReceptionDate),
                    Выручка = item.Revenue.ToString(), 
                    ТипТехники = item.EquipmentType,
                    Запчасти = item.SparePartsNames,
                }).ToList<object>();

            result.Add(new { НомерЗаказа = "Топ типов техники:", Выручка = "", ТипТехники = string.Join(", ", mostFrequentTypes.Select(x => $"{x.Type} ({x.Count})")), Запчасти = "" });
            result.Add(new { НомерЗаказа = "Топ запчастей:", Выручка = "", ТипТехники = string.Join(", ", mostUsedSpareParts.Select(x => $"{x.SparePartName} ({x.Count})")), Запчасти = "" });

            datagrid.ItemsSource = result;
        }

        private void upload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int currentMonth = DateTime.Now.Month;
                int currentYear = DateTime.Now.Year;

                var orders = context.Orders
                    .Include("Statuses")
                    .Include("SpareParts")
                    .Include("EquipmentTypes")
                    .Include("Employees")
                    .Where(o => o.Statuses.Names == "Завершен").ToList();

                var filteredOrders = orders
                    .Where(o =>
                    {
                        if (string.IsNullOrEmpty(o.ReceptionDate) || o.ReceptionDate.Length != 10)
                        {
                            return false;
                        }
                        try
                        {
                            DateTime receptionDate = DateTime.ParseExact(o.ReceptionDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            return receptionDate.Month == currentMonth && receptionDate.Year == currentYear;
                        }
                        catch (FormatException)
                        {
                            return false;
                        }
                    })
                    .ToList();

                if (filteredOrders.Count == 0)
                {
                    MessageBox.Show("Нет завершенных заказов за текущий месяц.");
                    return;
                }

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = $"Отчет_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string filePath = Path.Combine(desktopPath, fileName);
                PdfDocument doc = new PdfDocument();
                PdfPage page = doc.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 12);
                XFont boldFont = new XFont("Arial", 12);
                gfx.DrawString($"Отчет по заказам - {DateTime.Now:MMMM}", boldFont, XBrushes.Black, new XPoint(250, 30));
                int yPosition = 40;
                gfx.DrawString($"Общее количество заказов за месяц: {filteredOrders.Count}", font, XBrushes.Black, new XPoint(50, yPosition));
                yPosition += 40;
                foreach (var order in filteredOrders)
                {
                    gfx.DrawString($"Номер заказа: {order.NumberOrder}", font, XBrushes.Black, new XPoint(50, yPosition));
                    yPosition += 20;
                    gfx.DrawString($"Дата приема: {GetShortDateString(order.ReceptionDate)}", font, XBrushes.Black, new XPoint(50, yPosition));
                    yPosition += 20;
                    gfx.DrawString($"Выручка: {order.RepairCost:F2}", font, XBrushes.Black, new XPoint(50, yPosition));
                    yPosition += 20;
                    gfx.DrawString($"Тип техники: {order.EquipmentTypes?.Names}", font, XBrushes.Black, new XPoint(50, yPosition));
                    yPosition += 20;
                    gfx.DrawString($"Запчасти: {order.SpareParts?.SparePartsName}", font, XBrushes.Black, new XPoint(50, yPosition));
                    yPosition += 40;
                }
                var revenueByMaster = orders
                    .GroupBy(order => order.Employees?.Email)
                    .Select(group => new
                    {
                        Master = group.Key,
                        TotalRevenue = group.Sum(x => x.RepairCost - (x.Part_ID.HasValue ? x.SpareParts.Price : 0))
                    })
                    .OrderByDescending(x => x.TotalRevenue)
                    .ToList();

                gfx.DrawString("Выручка по мастерам:", boldFont, XBrushes.Black, new XPoint(50, yPosition));
                yPosition += 20;
                foreach (var masterRevenue in revenueByMaster)
                {
                    gfx.DrawString($"{masterRevenue.Master}: {masterRevenue.TotalRevenue:F2} рублей", font, XBrushes.Black, new XPoint(50, yPosition));
                    yPosition += 20;
                }
                yPosition += 40;
                gfx.DrawString("Запчастей (за все время):", boldFont, XBrushes.Black, new XPoint(50, yPosition));
                yPosition += 20;
                foreach (var sparePart in mostUsedSpareParts)
                {
                    gfx.DrawString($"{sparePart.SparePartName} ({sparePart.Count})", font, XBrushes.Black, new XPoint(50, yPosition));
                    yPosition += 20;
                }

                // Сохранение PDF
                doc.Save(filePath);
                MessageBox.Show($" Отчет сохранен по пути: {filePath}", "Формирование отчета");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании PDF отчета: {ex.Message}", "Ошибка формирования отчета");
            }
        }
    }
}
