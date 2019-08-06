using Sicop.Client.Http;

namespace Sicop.Tests
{
    public class FakeHttpClient : IHTTPClient
    {
        private static readonly string RetornoOnline = @"H4sIAAAAAAAEAO29B2AcSZYlJi9tynt/SvVK1+B0oQiAYBMk2JBAEOzBiM3mkuwdaUcjKasqgcplVmVdZhZAzO2dvPfee++999577733ujudTif33/8/XGZkAWz2zkrayZ4hgKrIHz9+fB8/Ih7XeXtSLZvjZdU8yZo8fbcol81nH83bdvXo7t2rq6txk59nPxjnzfiiuhxP6rvL81X+UXqZ101WffbR7nhn56Ojx22+WFUv62p6tLuz++nju+7vx9liUuTLNj/afXzX/v54lrXZ0d7O7sPtnQfbOwdvdh8+2vv00T16lb95nC0r/gdIcTt6Wf963Bar6uzlF0cvX335k2evv3x19iV1qJ9xM/q3M66j/wd91AHF6QAAAA==";
        
        //falha inesperada
        private static readonly string RetornoErro = @"H4sIAAAAAAAAAF2OQQrCMBBF94XeIeQAmequkhas4M6VJxh1ags1EzJDUjy9oq7cfd7jwfeJ9MBB9oFlQCGzPpYgnZ1U4w6glOKERnw6Enfn7C4JwhjJmkxJkDu7cU1j+7oyxl/Pitpv29bDd37oemKdM/dHXCY0cyCJlPCGHn6mrjz8vXiXLze8aByZAAAA";


        public enum TipoRetorno { Online, Offline, Erro }

        public static TipoRetorno Tipo { get; set; }

        public string Get(string url)
        {
            switch (Tipo)
            {
                case TipoRetorno.Online:
                    return RetornoOnline;
                case TipoRetorno.Offline:
                    throw new System.Exception("Offline");
                default:
                    return RetornoErro;
            }
        }
    }
}