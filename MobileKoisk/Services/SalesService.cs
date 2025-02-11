using MobileKoisk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MobileKoisk.Services
{

    public class SalesService
    {
        List<SaleItem> saleItems = new List<SaleItem>();
        public async Task<List<SaleItem>> LoadJsonDataAsync()
        {

            //check items if already loaded
            if (saleItems?.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("Returning cached sale items");
                return saleItems;
            }

            try
            {
                // Verify file exists and is accessible
                string fileName = "sale.json";

                // Debug: List available resources
                var resources = await FileSystem.OpenAppPackageFileAsync("sale.json");
                System.Diagnostics.Debug.WriteLine($"Attempting to read {fileName}");

                using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);

                if (stream == null)
                {
                    System.Diagnostics.Debug.WriteLine($"File {fileName} not found");
                    return new List<SaleItem>();
                }

                using var reader = new StreamReader(stream);
                var content = await reader.ReadToEndAsync();

                System.Diagnostics.Debug.WriteLine($"Raw JSON Content: {content}");

                // Enhanced deserialization with error handling
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                saleItems = JsonSerializer.Deserialize<List<SaleItem>>(content, options);

                System.Diagnostics.Debug.WriteLine($"Deserialized {saleItems?.Count ?? 0} sale items");

                return saleItems ?? new List<SaleItem>();

            }
            catch (Exception ex)
            {

                // Comprehensive error logging
                System.Diagnostics.Debug.WriteLine($"Error loading sale items: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");

                // Consider logging to a file or crash reporting service in a real app
                return new List<SaleItem>();

            }





        }
    }
}
