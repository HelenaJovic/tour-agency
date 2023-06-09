﻿using InitialProject.Domain.Model;
using InitialProject.WPF.ViewModel;
using Microsoft.VisualBasic.ApplicationServices;
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
using User = InitialProject.Domain.Model.User;

namespace InitialProject.WPF.View
{
    /// <summary>
    /// Interaction logic for ReportGuest2.xaml
    /// </summary>
    public partial class ReportGuest2 : Window
    {
        public ReportGuest2(User user, ReportGuest2ViewModel reportGuest2ViewModel)
        {
            InitializeComponent();
            DataContext = reportGuest2ViewModel;
        }
    }
}
