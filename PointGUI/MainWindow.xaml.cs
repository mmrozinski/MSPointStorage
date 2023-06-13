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

        private PointRepository? _repository;

        public MainWindow()
        {
            InitializeComponent();

            PointListBox.ItemsSource = Points;
        }

        private void CreateRepositoryClick(object sender, RoutedEventArgs e)
        {
            AddRepositoryWindow addRepositoryWindow = new AddRepositoryWindow();

            if (addRepositoryWindow.ShowDialog() == true)
            {
                _repository = addRepositoryWindow.PointRepository;

                ReadRepository();
            }
        }

        private void ReadRepositoryClick(object sender, RoutedEventArgs e)
        {
            ReadRepository();
        }

        private void AddPointClick(object sender, RoutedEventArgs e)
        {
            AddPointWindow addPointWindow = new AddPointWindow();
            if (addPointWindow.ShowDialog() == true)
            {
                try
                {
                    _repository.Save(addPointWindow.PointToAdd);

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

            if (_repository is not null)
            {
                Points.AddRange(_repository.GetAll());
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

        private void EditPointButton_Click(object sender, RoutedEventArgs e)
        {
            EditPointWindow editPointWindow = new EditPointWindow((MSPointStorage.Point) PointListBox.SelectedItem);
            if (editPointWindow.ShowDialog() == true)
            {
                try
                {
                    _repository.Update(editPointWindow.PointToEdit);

                    Points.Add(editPointWindow.PointToEdit);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                ReadRepository();
            }
        }

        private void DeletePointButton_Click(object sender, RoutedEventArgs e)
        {
            if (PointListBox.SelectedItem is null)
            {
                return;
            }

            MSPointStorage.Point pointToDelete = (MSPointStorage.Point) PointListBox.SelectedItem;

            _repository.Delete(pointToDelete);

            ReadRepository();
        }
    }
}
