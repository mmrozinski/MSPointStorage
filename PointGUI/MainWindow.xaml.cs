using MSPointStorage;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PointGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<MSPointStorage.Point> Points = new List<MSPointStorage.Point>();
        public List<PointRepository> Repositories = new List<PointRepository>();

        private PointRepository? _currentRepository;

        public MainWindow()
        {
            InitializeComponent();

            PointListBox.ItemsSource = Points;
            RepositoryComboBox.ItemsSource = Repositories;
        }

        private void CreateRepositoryClick(object sender, RoutedEventArgs e)
        {
            AddRepositoryWindow addRepositoryWindow = new AddRepositoryWindow();

            if (addRepositoryWindow.ShowDialog() == true)
            {
                if (addRepositoryWindow.PointRepository is not null) 
                {
                    _currentRepository = addRepositoryWindow.PointRepository;
                    Repositories.Add(addRepositoryWindow.PointRepository);
                }

                ReadRepository();
                RepositoryComboBox.Items.Refresh();
                RepositoryComboBox.SelectedItem = _currentRepository;
            }
        }

        private void ReadRepositoryClick(object sender, RoutedEventArgs e)
        {
            ReadRepository();
        }

        private void AddPointClick(object sender, RoutedEventArgs e)
        {
            if (_currentRepository is null)
            {
                MessageBox.Show("Select a repository first!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AddPointWindow addPointWindow = new AddPointWindow();
            if (addPointWindow.ShowDialog() == true)
            {
                try
                {
                    _currentRepository.Save(addPointWindow.PointToAdd);

                    Points.Add(addPointWindow.PointToAdd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                ReadRepository();
            }
        }

        private void ReadRepository()
        {
            Points.Clear();

            if (_currentRepository is not null)
            {
                Points.AddRange(_currentRepository.GetAll());
            }

            PointListBox.Items.Refresh();
        }

        private void CalculateDistanceButtonClick(object sender, RoutedEventArgs e)
        {
            CalculateDistanceWindow calculateDistanceWindow = new CalculateDistanceWindow(Points);
            calculateDistanceWindow.ShowDialog();
        }

        private void CalculateVolumeButtonClick(object sender, RoutedEventArgs e)
        {
            CalculateCubeVolumeWindow calculateCubeVolumeWindow = new CalculateCubeVolumeWindow(Points);
            calculateCubeVolumeWindow.ShowDialog();
        }

        private void IsInSphereCheckButtonClick(object sender, RoutedEventArgs e)
        {
            IsInSphereWindow isInSphereWindow = new IsInSphereWindow(Points);
            isInSphereWindow.ShowDialog();
        }

        private void EditPointButtonClick(object sender, RoutedEventArgs e)
        {
            if (_currentRepository is null)
            {
                MessageBox.Show("Select a repository first!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (PointListBox.SelectedItem is null)
            {
                MessageBox.Show("Select a point first!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            EditPointWindow editPointWindow = new EditPointWindow((MSPointStorage.Point)PointListBox.SelectedItem);
            if (editPointWindow.ShowDialog() == true)
            {
                try
                {
                    _currentRepository.Update(editPointWindow.PointToEdit);

                    Points.Add(editPointWindow.PointToEdit);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                ReadRepository();
            }
        }

        private void DeletePointButtonClick(object sender, RoutedEventArgs e)
        {
            if (_currentRepository is null)
            {
                MessageBox.Show("Select a repository first!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (PointListBox.SelectedItem is null)
            {
                MessageBox.Show("Select a point first!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MSPointStorage.Point pointToDelete = (MSPointStorage.Point)PointListBox.SelectedItem;

            _currentRepository.Delete(pointToDelete);

            ReadRepository();
        }

        private void RepositoryComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentRepository = (PointRepository)RepositoryComboBox.SelectedItem;

            ReadRepository();
        }

        private void DeleteRepositoryClick(object sender, RoutedEventArgs e)
        {
            if (_currentRepository is null)
            {
                MessageBox.Show("Select a repository first!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _currentRepository.DeleteRepository();

            Repositories.Remove(_currentRepository);

            _currentRepository = null;
            RepositoryComboBox.SelectedItem = null;

            RepositoryComboBox.Items.Refresh();
            ReadRepository();
        }
    }
}
