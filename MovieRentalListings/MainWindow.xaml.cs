using System.Windows;
using MovieRentalListings.ViewModels;

namespace MovieRentalListings
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainViewModel();
            DataContext = _vm;

            // Start loading after window is loaded
            Loaded += async (_, __) => await _vm.LoadSampleDataAsync();
        }
    }
}