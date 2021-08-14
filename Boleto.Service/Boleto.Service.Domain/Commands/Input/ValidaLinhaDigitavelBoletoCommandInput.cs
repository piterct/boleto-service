using Flunt.Notifications;
using Flunt.Validations;
using System;

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
                );
        }
    }
}
