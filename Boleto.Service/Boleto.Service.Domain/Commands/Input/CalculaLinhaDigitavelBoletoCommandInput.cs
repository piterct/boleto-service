using Flunt.Notifications;
using Flunt.Validations;
using System.Text.RegularExpressions;

namespace Boleto.Service.Domain.Commands.Input
{
    public class CalculaLinhaDigitavelBoletoCommandInput : Notifiable
    {
        public string CodigoBarras { get; set; }

        public void Validate()
        {
            AddNotifications(
           new Contract()
               .Requires()
               .IsNotNullOrEmpty(this.CodigoBarras, "CodigoBarras", "CodigoBarras obrigatório!")
               .IsGreaterOrEqualsThan(this.CodigoBarras == null ? 0 : Regex.Match(this.CodigoBarras, @"\d+").Value.Length, 44, "CodigoBarras", "Tamanho mínimo 44 caracteres!")
               .IsLowerOrEqualsThan(this.CodigoBarras == null ? 0 : Regex.Match(this.CodigoBarras, @"\d+").Value.Length, 44, "CodigoBarras", "Tamanho máximo 44 caracteres!")
                );
        }
    }
}
