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
    /// Interaction logic for CalculateCubeVolumeWindow.xaml
    /// </summary>
    public partial class CalculateCubeVolumeWindow : Window
    {
        public CalculateCubeVolumeWindow(List<MSPointStorage.Point> points)
        {
            InitializeComponent();

            PointListBox1.ItemsSource = points;
            PointListBox2.ItemsSource = points;
        }

        private void CalculateButtonClick(object sender, RoutedEventArgs e)
        {
            if (PointListBox1.SelectedItem is not null && PointListBox2.SelectedItem is not null)
            {
                double volume = MSPointStorage.Point.CalculateCubeVolume((MSPointStorage.Point) PointListBox1.SelectedItem, (MSPointStorage.Point) PointListBox2.SelectedItem);

                MessageBox.Show(volume.ToString());
            }
            else
            {
                MessageBox.Show("Select two points!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
