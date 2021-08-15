using Boleto.Service.Domain.Commands.Input;
using Boleto.Service.Domain.Commands.Output;
using Boleto.Service.Domain.Commands.Result;
using Boleto.Service.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Boleto.Service.Domain.Handlers
{
    public class BoletoHandler
    {
        public async ValueTask<ValidaLinhaDigitavelBoletoCommandResult> Handle(ValidaLinhaDigitavelBoletoCommandInput command)
        {
            command.Validate();
            if (command.Invalid)
                return new ValidaLinhaDigitavelBoletoCommandResult(false, "Incorrect  data!", null, StatusCodes.Status400BadRequest, command.Notifications);

            var boleto = new BoletoBancario(command.LinhaDigitavel);

            return new ValidaLinhaDigitavelBoletoCommandResult(true, "Success!", new ValidaLinhaDigitavelBoletoCommandOutput { CodigoBarras = boleto.CalculaCodigoBarras() },
                StatusCodes.Status200OK, command.Notifications);
        }
    }
}
