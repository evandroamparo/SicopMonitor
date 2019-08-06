using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sicop.Client;
using Sicop.Client.Http;

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
            return View(model: await cliente.AtualizarStatusAsync());
        }
    }
}
