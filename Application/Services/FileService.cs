using Application.Common.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Commons;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FileService : IFileService
    {
        public async Task<List<T>> ReadCsvFileAsync<T>(IFormFile file) where T : AudiTable
        {
            var records = new List<T>();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream(); // Open the file stream for the uploaded file
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true // Assuming the first row is the header
                });
                // Read the CSV records asynchronously
                await foreach (var record in csv.GetRecordsAsync<T>())
                {
                    records.Add(record);
                }
                
            }

            return records;
        }

        public T? MapCsvRowToObject<T>(string[] csvRow) where T : AudiTable
        {
            if (csvRow == null || csvRow.Length == 0)
                return null;

            var obj = Activator.CreateInstance<T>();

            var properties = typeof(T).GetProperties();
            for (int i = 0; i < csvRow.Length && i < properties.Length; i++)
            {
                var property = properties[i];
                if (property.CanWrite)
                {
                    var value = csvRow[i];
                    var convertedValue = Convert.ChangeType(value, property.PropertyType);
                    property.SetValue(obj, convertedValue);
                }
            }

            return obj;
        }
    }
}
