using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sicop.Client;
using Sicop.Client.Http;
using SicopMonitor.Web.ViewModels;

namespace SicopMonitor.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHTTPClient _httpClient;

        public HomeController(IConfiguration configuration, IHTTPClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var cultura = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = cultura;
            Thread.CurrentThread.CurrentUICulture = cultura;

            var cliente = new SicopClient(_configuration["UrlBase"].ToString(), _httpClient);
            System.DateTime datahora = TimeZoneInfo.ConvertTime(
                DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            var status = new StatusViewModel { Mensagem = await cliente.AtualizarStatusAsync(), DataHora = datahora };
            return View(status);
        }
    }
}
