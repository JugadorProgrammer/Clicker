using ClickerServer.Services.DataBaseService.Models;

namespace ClickerServer.interfaces
{
    /// <summary>
    /// Сервис для работы с базами данных
    /// </summary>
    public interface IDataBaseService
    {
        /// <summary>
        /// Получение укороченного Url
        /// </summary>
        /// <param name="recivedUrl">url сайта</param>
        /// <param name="hostName">название хоста</param>
        /// <returns>Url объект с соответсвующими полями</returns>
        Url GetShortUrl(string recivedUrl);
        /// <summary>
        /// Удаление Url  по Id
        /// </summary>
        /// <param name="Id">Уникальный идентификатор</param>
        void DeleteUrlById(int Id);
        /// <summary>
        /// Получение длинного url
        /// </summary>
        /// <param name="generateUrl">Короткий Url</param>
        /// <returns>Объект url</returns>
        Url? GetLongUrl(string generateUrl);
    }
}
