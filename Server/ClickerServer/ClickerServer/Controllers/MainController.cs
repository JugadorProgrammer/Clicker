using ClickerServer.interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClickerServer.Controllers
{
    [Route("api/[controller]/[action]")]
    //[ApiController]
    public class MainController : ControllerBase
    {
        private readonly IDataBaseService _dataBaseService;
        public MainController(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }
        /// <summary>
        /// Сжатие url
        /// </summary>
        /// <param name="recivedUrl">url для сжатия</param>
        /// <returns>Url объект</returns>
        [HttpPost]
        public IActionResult ShortUrl(string recivedUrl)
        {
            var url = _dataBaseService.GetShortUrl(recivedUrl);
            return Ok(JsonConvert.SerializeObject(url));
        }

        /// <summary>
        /// Получение изначального url
        /// </summary>
        /// <param name="generateUrl">сжатый url</param>
        /// <returns>Url объект</returns>
        [HttpPost]
        public IActionResult LongUrl(string generateUrl)
        {
            var url = _dataBaseService.GetLongUrl(generateUrl);
            return Ok(JsonConvert.SerializeObject(url));
        }

    }
}
