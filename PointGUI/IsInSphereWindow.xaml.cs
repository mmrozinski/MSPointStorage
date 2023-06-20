using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace PointGUI
{
    /// <summary>
    /// Interaction logic for IsInSphereWindow.xaml
    /// </summary>
    public partial class IsInSphereWindow : Window
    {
        public IsInSphereWindow(List<MSPointStorage.Point> points)
        {
            InitializeComponent();

            PointListBox1.ItemsSource = points;
            PointListBox2.ItemsSource = points;
        }

        private static bool IsNumeric(string text)
        {
            Regex regex = new("[0-9.]");
            return regex.IsMatch(text);
        }

        private void NumericTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }

        private void CheckButtonClick(object sender, RoutedEventArgs e)
        {
            if (PointListBox1.SelectedItem is not null && PointListBox2.SelectedItem is not null && Double.TryParse(RadiusTextBox.Text, out _))
            {
                bool isInSphere = MSPointStorage.Point.IsInSphere((MSPointStorage.Point) PointListBox1.SelectedItem, (MSPointStorage.Point) PointListBox2.SelectedItem, 
                    Double.Parse(RadiusTextBox.Text));

                MessageBox.Show(isInSphere ? "Point is in sphere!" : "Point is NOT in sphere!");
            }
            else
            {
                MessageBox.Show("Select two points and input a valid radius!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HelpButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Select a point on the left, the sphere's center point on the right and input the sphere's radius to check if the point is inside of the sphere.", 
                "Help", MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}
