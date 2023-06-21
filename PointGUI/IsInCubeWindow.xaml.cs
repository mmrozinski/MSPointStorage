using System.Collections.Generic;
using System.Windows;

namespace PointGUI
{
    /// <summary>
    /// Interaction logic for IsInCubeWindow.xaml
    /// </summary>
    public partial class IsInCubeWindow : Window
    {
        public IsInCubeWindow(List<MSPointStorage.Point> points)
        {
            InitializeComponent();

            PointListBox1.ItemsSource = points;
            PointListBox2.ItemsSource = points;
            PointListBox3.ItemsSource = points;
        }

        private void CheckButtonClick(object sender, RoutedEventArgs e)
        {
            if (PointListBox1.SelectedItem is not null && PointListBox2.SelectedItem is not null && PointListBox3.SelectedItem is not null)
            {
                bool isInBox = MSPointStorage.Point.IsInBox((MSPointStorage.Point)PointListBox1.SelectedItem, (MSPointStorage.Point)PointListBox2.SelectedItem,
                    (MSPointStorage.Point)PointListBox3.SelectedItem);

                MessageBox.Show(isInBox ? "Point is in box!" : "Point is NOT in box!");
            }
            else
            {
                MessageBox.Show("Select all three points!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HelpButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Select a point on the left and two opposite vertices of a box on the left to check if the point is inside the box.",
                "Help", MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}
