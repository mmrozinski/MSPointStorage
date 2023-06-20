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
using System.Windows.Shapes;

namespace PointGUI
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public ResultWindow(string message, string result)
        {
            InitializeComponent();

            ResultMessageLabel.Content = message;
            ResultValueTextBox.Text = result;
        }

        public ResultWindow(string message, string result, string title)
        {
            InitializeComponent();

            ResultMessageLabel.Content = message;
            ResultValueTextBox.Text = result;
            this.Title = title;
        }

        public static void Show(string message, string result)
        {
            ResultWindow window = new ResultWindow(message, result);
            window.ShowDialog();
        }

        public static void Show(string message, string result, string title)
        {
            ResultWindow window = new ResultWindow(message, result, title);
            window.ShowDialog();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
