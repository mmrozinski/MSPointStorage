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

namespace PointGUI
{
    /// <summary>
    /// Interaction logic for CalculateDistanceWindow.xaml
    /// </summary>
    public partial class CalculateDistanceWindow : Window
    {
        public CalculateDistanceWindow(List<MSPointStorage.Point> points)
        {
            InitializeComponent();

            PointListBox1.ItemsSource = points;
            PointListBox2.ItemsSource = points;
        }

        private void CalculateButtonClick(object sender, RoutedEventArgs e)
        {
            if (PointListBox1.SelectedItem is not null && PointListBox2.SelectedItem is not null)
            {
                double distance = MSPointStorage.Point.CalculateDistance((MSPointStorage.Point)PointListBox1.SelectedItem, (MSPointStorage.Point)PointListBox2.SelectedItem);

                MessageBox.Show(distance.ToString());
            }
        }
    }
}
