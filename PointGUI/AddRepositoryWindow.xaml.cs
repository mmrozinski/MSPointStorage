using MSPointStorage;
using System;
using System.Windows;
using System.Windows.Input;

namespace PointGUI
{
    /// <summary>
    /// Interaction logic for AddRepositoryWindow.xaml
    /// </summary>
    public partial class AddRepositoryWindow : Window
    {
        private PointRepository? _repository;
        public PointRepository? PointRepository { 
            get
            {
                return _repository;
            }
        }

        public AddRepositoryWindow()
        {
            InitializeComponent();
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(ConnectionStringTextBox.Text) || String.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Connection String and name cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    Mouse.SetCursor(Cursors.Wait);
                    _repository = new PointRepository(ConnectionStringTextBox.Text, NameTextBox.Text);
                    
                    Window.GetWindow(this).DialogResult = true;
                    Window.GetWindow(this).Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid connection string or name!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Mouse.SetCursor(Cursors.Arrow);
            }
        }
    }
}
