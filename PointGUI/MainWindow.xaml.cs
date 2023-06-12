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
        private const string _connectionString = "Data Source=192.168.243.101;Initial Catalog=master;Persist Security Info=True;User ID=SA;Password=yourStrong(!)Password";
        private const string _repositoryName = "Points";

        

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
            PointListBox.Items.Refresh();
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
            }

            PointListBox.Items.Refresh();
        }

        private void ReadRepository()
        {
            Points.Clear();

            if (_repository is not null)
            {
                Points.AddRange(_repository.GetAll());
            }
        }

        private void CalculateDistanceButtonClick(object sender, RoutedEventArgs e)
        {
            CalculateDistanceWindow calculateDistanceWindow = new CalculateDistanceWindow(Points); 
            calculateDistanceWindow.ShowDialog();
        }
    }
}
