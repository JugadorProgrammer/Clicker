using ClickerServer.interfaces;
using ClickerServer.Services.DataBaseService.Models;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ClickerServer.Services.DataBaseService
{
    /// <summary>
    /// Сервис для работы с базой данных реализация
    /// </summary>
    public class DataBaseService : IDataBaseService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DataBaseService> _logger;
        public DataBaseService(IConfiguration configuration, ILogger<DataBaseService> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        /// <summary>
        /// Удаление по Id
        /// </summary>
        /// <param name="Id">Уникальный идентификатор</param>
        public void DeleteUrlById(int Id)
        {
            using (IDbConnection db = new SqlConnection(_configuration["DatabaseSettings:ConnectionString"]))
            {
                db.Query("DELETE FROM Url WHERE Id=@Id", new { Id });
            }
        }
        /// <summary>
        /// Получение изначального url
        /// </summary>
        /// <param name="generateUrl">Хэшированный url</param>
        /// <returns>Изаначальный url</returns>
        public Url? GetLongUrl(string generateUrl)
        {
            using (IDbConnection db = new SqlConnection(_configuration["DatabaseSettings:ConnectionString"]))
            {
                return db.Query<Url>("SELECT * FROM Url WHERE GeneratedUrl=@generateUrl", new { generateUrl }).FirstOrDefault();
            }
        }
        /// <summary>
        /// Получение укороченного url
        /// </summary>
        /// <param name="recivedUrl">Изначальный url</param>
        /// <returns>Укороченный url</returns>
        public Url GetShortUrl(string recivedUrl)
        {
            using (IDbConnection db = new SqlConnection(_configuration["DatabaseSettings:ConnectionString"]))
            {
                var resultUrl = db.Query<Url>("SELECT * FROM Url WHERE RecivedUrl = @recivedUrl", new { recivedUrl }).FirstOrDefault();
                if (resultUrl == null)
                {
                    resultUrl = new Url
                    {
                        RecivedUrl = recivedUrl,
                        GeneratedUrl = $"{_configuration["HostName"]}/{Guid.NewGuid().ToString("N").Substring(0, 10)}",
                        CreateTime = DateTime.Now,
                    };
                    var queryString = "INSERT INTO Url (RecivedUrl,GeneratedUrl,CreateTime) VALUES (@RecivedUrl,@GeneratedUrl,@CreateTime); SELECT CAST(SCOPE_IDENTITY() as int)";
                    resultUrl.Id = db.Query<int>(queryString, resultUrl).FirstOrDefault();
                }
                return resultUrl;
            }
        }
        
    }
}
