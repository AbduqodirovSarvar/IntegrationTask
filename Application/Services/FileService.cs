using Application.Common.Interfaces;
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Commons;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Application.Services
{
    public class FileService(IMapper mapper) : IFileService
    {
        private readonly IMapper _mapper = mapper;
        public async Task<List<TEntity>> ReadCsvFileAsync<TEntity, TCsvModel, TMap>(IFormFile file)
            where TEntity : AudiTable
            where TCsvModel : class
            where TMap : ClassMap<TCsvModel>
        {
            var records = new List<TEntity>();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                using var reader = new StreamReader(stream);

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    HeaderValidated = null,
                    MissingFieldFound = null
                };

                using var csv = new CsvReader(reader, config);

                try
                {
                    // Register the class map
                    csv.Context.RegisterClassMap<TMap>();

                    // Read as TCsvModel and map to TEntity
                    var csvRecords = csv.GetRecordsAsync<TCsvModel>();

                    await foreach (var record in csvRecords)
                    {
                        records.Add(_mapper.Map<TEntity>(record));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while reading csv file", ex);
                }
            }

            return records;
        }
    }
}
