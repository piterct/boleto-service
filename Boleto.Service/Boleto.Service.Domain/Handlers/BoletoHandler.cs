using Boleto.Service.Domain.Commands.Input;
using Boleto.Service.Domain.Commands.Result;
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

            return new ValidaLinhaDigitavelBoletoCommandResult(true, "Success!", null, StatusCodes.Status200OK, command.Notifications);
        }
    }
}
