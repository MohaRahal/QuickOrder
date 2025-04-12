using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickOrder.Services
{
    public static class DatabaseHelper
    {
        public static async Task<string> InitializeDatabaseAsync(string dbName)
        {
            string destinationPath = Path.Combine(FileSystem.AppDataDirectory, dbName);

            if (!File.Exists(destinationPath))
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(dbName);
                using var dest = File.Create(destinationPath);
                await stream.CopyToAsync(dest);
            }

            return destinationPath;
        }
    }
}
