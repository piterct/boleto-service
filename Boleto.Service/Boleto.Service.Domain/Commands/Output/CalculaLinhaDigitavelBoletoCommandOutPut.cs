using System;

namespace Boleto.Service.Domain.Commands.Output
{
    public class CalculaLinhaDigitavelBoletoCommandOutPut
    {
        public string LinhaDigitavel { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
    }
}
