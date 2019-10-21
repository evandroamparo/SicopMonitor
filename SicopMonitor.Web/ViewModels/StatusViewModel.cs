using System;

namespace SicopMonitor.Web.ViewModels
{
    public class StatusViewModel
    {
        public StatusViewModel(string mensagem, DateTime dataHora, double tempoRespostaSegundos)
        {
            this.Mensagem = mensagem;
            this.DataHora = dataHora;
            this.TempoRespostaSegundos = tempoRespostaSegundos;

        }
        public string Mensagem { get; private set; }
        public DateTime DataHora { get; private set; }
        public double TempoRespostaSegundos { get; private set; }
    }
}