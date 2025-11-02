using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TradeFileWatcher.Models;

namespace TradeFileWatcher.Loaders
{
    public class CsvFileLoader : IFileLoader
    {
        public string FileExtension => ".csv";

        public IEnumerable<TradeRecord> Load(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var records = new List<TradeRecord>();

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length != 6 || parts[0].ToLower() == "date") continue;

                records.Add(new TradeRecord
                {
                    Date = DateTime.Parse(parts[0]),
                    Open = decimal.Parse(parts[1], CultureInfo.InvariantCulture),
                    High = decimal.Parse(parts[2], CultureInfo.InvariantCulture),
                    Low = decimal.Parse(parts[3], CultureInfo.InvariantCulture),
                    Close = decimal.Parse(parts[4], CultureInfo.InvariantCulture),
                    Volume = long.Parse(parts[5])
                });
            }
            return records;
        }
    }
}
