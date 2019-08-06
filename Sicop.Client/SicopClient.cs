using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Sicop.Client.Http;

namespace Sicop.Client
{
    public class SicopClient
    {
        private readonly string _urlBase;
        private readonly IHTTPClient _cliente;

        public SicopClient(string urlBase, IHTTPClient cliente)
        {
            _urlBase = urlBase;
            _cliente = cliente;
        }

        public async Task<string> AtualizarStatusAsync()
        {
            try
            {
                var resposta = _cliente.Get(_urlBase);
                var xml = ProcessarResposta(resposta);

                if (string.IsNullOrEmpty(xml))
                {
                    return await Task.FromResult("Offline");
                }
                else if (xml.ToLower().IndexOf("falha inesperada") != -1)
                {
                    return await Task.FromResult("Erro");
                }
                else
                {
                    return await Task.FromResult("Online");
                }
            }
            catch
            {
                return "Offline";
            }
        }

        private string ProcessarResposta(string resposta)
        {
            var gzip = Convert.FromBase64String(resposta);

            using (GZipStream stream = new GZipStream(new MemoryStream(gzip),
                CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int bytesLidos = 0;
                    do
                    {
                        bytesLidos = stream.Read(buffer, 0, size);
                        if (bytesLidos > 0)
                        {
                            memory.Write(buffer, 0, bytesLidos);
                        }
                    }
                    while (bytesLidos > 0);
                    return Encoding.Default.GetString(memory.ToArray());
                }
            }
        }
    }
}
