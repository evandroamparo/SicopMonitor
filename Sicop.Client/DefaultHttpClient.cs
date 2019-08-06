using System.ServiceModel;
using SicopService;

namespace Sicop.Client.Http
{
    public class DefaultHttpClient : IHTTPClient
    {
        public string Get(string url)
        {
            var client = new WSSICOPImportacaoSoapClient(new BasicHttpBinding(BasicHttpSecurityMode.None),
               new EndpointAddress(url));
            return client.NfpeAnoBaseAsync().Result.Body.NfpeAnoBaseResult;
        }
    }
}