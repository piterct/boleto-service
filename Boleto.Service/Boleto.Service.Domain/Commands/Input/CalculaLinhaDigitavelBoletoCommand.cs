using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Text.RegularExpressions;

namespace Boleto.Service.Domain.Commands.Input
{
    public class CalculaLinhaDigitavelBoletoCommand : Notifiable
    {
        public string CodigoBarras { get; set; }

        public void Validate()
        {
            AddNotifications(
           new Contract()
               .Requires()
               .IsNotNullOrEmpty(CodigoBarras, "CodigoBarras", "CodigoBarras obrigatório!")
                .IsLowerThan(CodigoBarras == null ? 0 : Regex.Match(this.CodigoBarras, @"\d+").Value.Length, 44, "CodigoBarras", "Tamanho mínimo 44 caracteres!")
                .IsGreaterThan(CodigoBarras == null ? 0 : Regex.Match(this.CodigoBarras, @"\d+").Value.Length, 44, "CodigoBarras", "Tamanho máximo 44 caracteres!")
                );
        }
    }
}
