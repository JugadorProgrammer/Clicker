namespace ClickerServer.Services.DataBaseService.Models
{
    /// <summary>
    /// Url - модель двнных для базы данных
    /// </summary>
    public class Url
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Полученный адресс
        /// </summary>
        public string RecivedUrl { get; set; }
        /// <summary>
        /// Адресс после "сжатия"
        /// </summary>
        public string GeneratedUrl { get; set; }
        /// <summary>
        /// Время создания данного экземпляра
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
