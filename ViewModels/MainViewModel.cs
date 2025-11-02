using System.Collections.ObjectModel;
using TradeFileWatcher.Models;
using TradeFileWatcher.Services;
using TradeFileWatcher.Loaders;
using System.Collections.Generic;
using System.Windows;

namespace TradeFileWatcher.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<TradeRecord> Trades { get; set; } = new();

        private readonly FileMonitorService _monitor;

        public MainViewModel()
        {
            // Configure folder path and loaders
            string folder = @"C:\Users\Acer\Desktop\Xalq\TradeFileWatcher\ExFiles"; // replace with your folder
            int interval = 5; // seconds
            List<IFileLoader> loaders = new List<IFileLoader>
            {
                new CsvFileLoader()
                // add TxtFileLoader, XmlFileLoader here
            };

            _monitor = new FileMonitorService(folder, interval, loaders);
            _monitor.RecordLoaded += record =>
            {
                Application.Current.Dispatcher.Invoke(() => Trades.Add(record));
            };

            _monitor.Start();
        }
    }
}
