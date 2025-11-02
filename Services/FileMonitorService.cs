using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using TradeFileWatcher.Loaders;
using TradeFileWatcher.Models;

namespace TradeFileWatcher.Services
{
    public class FileMonitorService
    {
        private readonly string _directory;
        private readonly int _intervalSeconds;
        private readonly List<IFileLoader> _loaders;
        private readonly HashSet<string> _processed = new HashSet<string>();
        private readonly Timer _timer;

        public event Action<TradeRecord> RecordLoaded;

        public FileMonitorService(string directory, int intervalSeconds, List<IFileLoader> loaders)
        {
            _directory = directory;
            _intervalSeconds = intervalSeconds;
            _loaders = loaders;
            _timer = new Timer(intervalSeconds * 1000);
            _timer.Elapsed += async (s, e) => await CheckForNewFilesAsync();
        }

        public void Start() => _timer.Start();

        private async Task CheckForNewFilesAsync()
        {
            var files = Directory.GetFiles(_directory);
            var newFiles = files.Where(f => !_processed.Contains(f)).ToList();

            foreach (var file in newFiles)
            {
                _processed.Add(file);
                await Task.Run(() => LoadFile(file));
            }
        }

        private void LoadFile(string path)
        {
            var loader = _loaders.FirstOrDefault(l => path.EndsWith(l.FileExtension, StringComparison.OrdinalIgnoreCase));
            if (loader == null) return;

            foreach (var record in loader.Load(path))
                RecordLoaded?.Invoke(record);
        }
    }
}
