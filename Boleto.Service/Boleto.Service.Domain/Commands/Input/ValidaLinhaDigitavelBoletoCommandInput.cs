using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Text.RegularExpressions;

namespace Boleto.Service.Domain.Commands.Input
{
    public class ValidaLinhaDigitavelBoletoCommandInput : Notifiable
    {
        public string LinhaDigitavel { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }

        public void Validate()
        {
            AddNotifications(
           new Contract()
               .Requires()
               .IsNotNullOrEmpty(LinhaDigitavel, "LinhaDigitavel", "LinhaDigitavel obrigatório!")
                .IsGreaterThan(LinhaDigitavel.Length, 36, "LinhaDigitavel", "Tamanho mínimo 36 caracteres!")
                .IsLowerThan(Regex.Match(this.LinhaDigitavel, @"\d+").Value.Length, 48, "LinhaDigitavel", "Tamanho máximo 47 caracteres!")
                );
        }
    }
}
