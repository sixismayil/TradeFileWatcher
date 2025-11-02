using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeFileWatcher.Models;

namespace TradeFileWatcher.Loaders
{
    public interface IFileLoader
    {
        string FileExtension { get; }
        IEnumerable<TradeRecord> Load(string filePath);
    }
}
