using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sicop.Client;
using Sicop.Client.Http;
using SicopMonitor.Web.ViewModels;
using TimeZoneConverter;

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
            var cliente = new SicopClient(_configuration["UrlBase"].ToString(), _httpClient);
            var cronometro = new Stopwatch();
            cronometro.Start();
            var resposta = await cliente.AtualizarStatusAsync();
            cronometro.Stop();
            var status = new StatusViewModel(
                mensagem: resposta,
                dataHora: TimeZoneInfo.ConvertTime(
                    DateTime.Now, TZConvert.GetTimeZoneInfo("E. South America Standard Time")),
                tempoRespostaSegundos: cronometro.Elapsed.TotalSeconds);
            return View(status);
        }
    }
}
