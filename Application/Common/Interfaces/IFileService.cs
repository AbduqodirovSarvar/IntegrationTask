using System.Collections.Generic;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Domain.Commons;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Reads a CSV file from the provided IFormFile and maps the content to a list of objects of type TEntity.
        /// </summary>
        Task<List<TEntity>> ReadCsvFileAsync<TEntity, TCsvModel, TMap>(IFormFile file)
            where TEntity : AudiTable
            where TCsvModel : class
            where TMap : ClassMap<TCsvModel>;
    }
}
