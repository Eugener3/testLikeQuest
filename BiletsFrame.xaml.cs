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

namespace testLikeQuest
{
    /// <summary>
    /// Interaction logic for BiletsFrame.xaml
    /// </summary>
    public partial class BiletsFrame : Page
    {
        public BiletsFrame()
        {
            InitializeComponent();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.userID = null;
            NavigationService.Navigate(new MainFrame());
        }
    }
}
