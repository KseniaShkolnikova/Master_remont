﻿using System;
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
    /// <summary>
    /// Логика взаимодействия для Admin_Clients.xaml
    /// </summary>
    public partial class Admin_Clients : Page
    {
        private Master_RemontEntities context = new Master_RemontEntities();
        public Admin_Clients()
        {
            InitializeComponent();
            datagrid.ItemsSource = context.Clients.ToList();
        }
    }
}
