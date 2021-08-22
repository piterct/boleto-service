using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Text.RegularExpressions;

namespace Boleto.Service.Domain.Commands.Input
{
    public class CalculaLinhaDigitavelBoletoCommand : Notifiable
    {
        public string CodigoBarras { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }

        public void Validate()
        {
            AddNotifications(
           new Contract()
               .Requires()
               .IsNotNullOrEmpty(CodigoBarras, "CodigoBarras", "CodigoBarras obrigatório!")
                .IsGreaterThan(CodigoBarras == null ? 0 : CodigoBarras.Length, 36, "CodigoBarras", "Tamanho mínimo 36 caracteres!")
                .IsLowerThan(CodigoBarras == null ? 0 : Regex.Match(this.CodigoBarras, @"\d+").Value.Length, 48, "CodigoBarras", "Tamanho máximo 47 caracteres!")
                );
        }
    }
}
