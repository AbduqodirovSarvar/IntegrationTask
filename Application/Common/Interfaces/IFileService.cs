using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Commons;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Reads a CSV file from the provided IFormFile and maps the content to a list of objects of type T.
        /// </summary>
        /// <typeparam name="T">The type to which the CSV rows will be mapped.</typeparam>
        /// <param name="file">The IFormFile representing the uploaded CSV file.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of mapped objects.</returns>
        Task<List<T>> ReadCsvFileAsync<T>(IFormFile file) where T : AudiTable;

        /// <summary>
        /// Maps a CSV row to an object of type T.
        /// </summary>
        /// <typeparam name="T">The type to which the CSV row will be mapped.</typeparam>
        /// <param name="csvRow">A CSV row represented as a string array or list.</param>
        /// <returns>An object of type T mapped from the CSV row.</returns>
        T? MapCsvRowToObject<T>(string[] csvRow) where T : AudiTable;
    }
}
