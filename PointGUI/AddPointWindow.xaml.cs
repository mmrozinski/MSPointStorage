using MSPointStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddPointWindow.xaml
    /// </summary>
    public partial class AddPointWindow : Window
    {
        public MSPointStorage.Point PointToAdd
        {
            get
            {
                return new MSPointStorage.Point(Double.Parse(XCoordinateTextBox.Text), Double.Parse(YCoordinateTextBox.Text),
                    Double.Parse(ZCoordinateTextBox.Text), Int32.Parse(IdTextBox.Text));
            }
        }

        public AddPointWindow()
        {
            InitializeComponent();
        }

        private static bool IsNumeric(string text)
        {
            Regex regex = new("^[0-9]+$");
            return regex.IsMatch(text);
        }

        private void NumericTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (Double.TryParse(XCoordinateTextBox.Text, out _) && Double.TryParse(YCoordinateTextBox.Text, out _) && 
                Double.TryParse(ZCoordinateTextBox.Text, out _) && Int32.TryParse(IdTextBox.Text, out _))
            {
                Window.GetWindow(this).DialogResult = true;
                Window.GetWindow(this).Close();
            } 
            else
            {
                MessageBox.Show("Invalid value!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
