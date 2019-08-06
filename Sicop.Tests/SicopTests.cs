using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sicop.Client;
using Sicop.Client.Http;

namespace Sicop.Tests
{
    [TestClass]
    public class SicopTests
    {
        private IHTTPClient _httpClient;
        private SicopClient _cliente;

        [TestInitialize]
        public void Setup()
        {
            _httpClient = new FakeHttpClient();
            // _httpClient = new Sicop.Client.Http.DefaultHttpClient();
            _cliente = new SicopClient("http://www.sefaz.es.gov.br/wsSicopImportacao/", _httpClient);
        }

        [TestMethod]
        public async Task DeveDetectarServicoOnline()
        {
            FakeHttpClient.Tipo = FakeHttpClient.TipoRetorno.Online;
            Assert.AreEqual("Online", await _cliente.AtualizarStatusAsync());
        }

        [TestMethod]
        public async Task DeveDetectarServicoOffline()
        {
            FakeHttpClient.Tipo = FakeHttpClient.TipoRetorno.Offline;
            Assert.AreEqual("Offline", await _cliente.AtualizarStatusAsync());
        }

        [TestMethod]
        public async Task DeveDetectarServicoComErro()
        {
            FakeHttpClient.Tipo = FakeHttpClient.TipoRetorno.Erro;
            Assert.AreEqual("Erro", await _cliente.AtualizarStatusAsync());
        }
    }
}
