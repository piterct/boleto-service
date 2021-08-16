using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Text.RegularExpressions;

namespace Boleto.Service.Domain.Commands.Input
{
    public class CalculaCodigoBarrasBoletoCommandInput : Notifiable
    {
        public CalculaCodigoBarrasBoletoCommandInput()
        {

        }
        public CalculaCodigoBarrasBoletoCommandInput(string linhaDigitavel, decimal valor, DateTime dataVencimento)
        {
            LinhaDigitavel = linhaDigitavel;
            Valor = valor;
            DataVencimento = dataVencimento;
        }

        public string LinhaDigitavel { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }

        public void Validate()
        {
            AddNotifications(
           new Contract()
               .Requires()
               .IsNotNullOrEmpty(LinhaDigitavel, "LinhaDigitavel", "LinhaDigitavel obrigatório!")
                .IsGreaterThan(LinhaDigitavel == null ? 0 : LinhaDigitavel.Length, 36, "LinhaDigitavel", "Tamanho mínimo 36 caracteres!")
                .IsLowerThan(LinhaDigitavel == null ? 0 : Regex.Match(this.LinhaDigitavel, @"\d+").Value.Length, 48, "LinhaDigitavel", "Tamanho máximo 47 caracteres!")
                );
        }
    }
}
