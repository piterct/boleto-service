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

            var boletoBancario = new BoletoBancario(command.LinhaDigitavel);
            boletoBancario.CalculaCodigoBarras();

            if (!boletoBancario.ValidaDigitoCodigodeBarras())
                return new ValidaLinhaDigitavelBoletoCommandResult(false, "Digito verificador inválido!", null,
               StatusCodes.Status400BadRequest, command.Notifications);

            return new ValidaLinhaDigitavelBoletoCommandResult(true, "Sucesso!", new ValidaLinhaDigitavelBoletoCommandOutput { CodigoBarras = boletoBancario.CalculaCodigoBarras() },
                StatusCodes.Status200OK, command.Notifications);
        }
    }
}
