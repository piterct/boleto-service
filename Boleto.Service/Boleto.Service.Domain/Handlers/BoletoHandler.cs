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
        public async ValueTask<CalculaCodigoBarrasBoletoCommandResult> Handle(CalculaCodigoBarrasBoletoCommandInput command)
        {
            command.Validate();
            if (command.Invalid)
                return new CalculaCodigoBarrasBoletoCommandResult(false, "Dados incorretos!", null, StatusCodes.Status400BadRequest, command.Notifications);

            var boletoBancario = new BoletoBancario(command.LinhaDigitavel, string.Empty);
            await boletoBancario.CalculaCodigoBarras();

            if (!boletoBancario.ValidaDigitoCodigodeBarras())
                return new CalculaCodigoBarrasBoletoCommandResult(false, "Digito verificador inválido!", null,
               StatusCodes.Status400BadRequest, command.Notifications);

            return new CalculaCodigoBarrasBoletoCommandResult(true, "Sucesso!", new CalculaCodigoBarrasBoletoCommandOutput { CodigoBarras = await boletoBancario.CalculaCodigoBarras() },
                StatusCodes.Status200OK, command.Notifications);
        }
    }
}
