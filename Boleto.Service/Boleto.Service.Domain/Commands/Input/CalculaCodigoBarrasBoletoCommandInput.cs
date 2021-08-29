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
               .IsNotNullOrEmpty(this.LinhaDigitavel, "LinhaDigitavel", "LinhaDigitavel obrigatório!")
                .IsGreaterThan(this.LinhaDigitavel == null ? 0 : Regex.Replace(this.LinhaDigitavel, @"[^\d]", "").Length, 36, "LinhaDigitavel", "Tamanho mínimo 36 caracteres!")
                .IsLowerThan(LinhaDigitavel == null ? 0 : Regex.Replace(this.LinhaDigitavel, @"[^\d]", "").Length, 48, "LinhaDigitavel", "Tamanho máximo 47 caracteres!")
                );
        }
    }
}
