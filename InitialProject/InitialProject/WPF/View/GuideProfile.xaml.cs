﻿using InitialProject.Domain.Model;
using InitialProject.WPF.ViewModel;
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

namespace InitialProject.WPF.View
{
    /// <summary>
    /// Interaction logic for GuideProfile.xaml
    /// </summary>
    public partial class GuideProfile : Window
    {
        public GuideProfile(User user)
        {
            this.Width = 430;
            this.Height = 750;
            InitializeComponent();
            DataContext = new GuideProfileViewModel(user);
        }
    }
}
