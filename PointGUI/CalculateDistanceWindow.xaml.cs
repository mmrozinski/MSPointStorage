using System.Collections.Generic;
using System.Windows;

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

                MessageBox.Show("Distance between the points equals " + distance.ToString());
            }
            else
            {
                MessageBox.Show("Select two points!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
