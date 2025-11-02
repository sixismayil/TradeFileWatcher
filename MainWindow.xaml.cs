using System.Windows;
using TradeFileWatcher.ViewModels;

namespace TradeFileWatcher
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
