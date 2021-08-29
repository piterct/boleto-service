using Boleto.Service.Domain.Commands.Input;
using Boleto.Service.Domain.Commands.Output;
using Boleto.Service.Domain.Commands.Result;
using Boleto.Service.Domain.Entity;
using Boleto.Service.Shared.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Boleto.Service.Domain.Handlers
{
    public class BoletoHandler
    {
        private readonly DataBaseBacenSettings _dataBaseBacenSettings;
        public BoletoHandler(IOptions<ProjectSettings> projectSettings)
        {
            _dataBaseBacenSettings = projectSettings.Value.DataBaseBacenSettings;
        }
        public async ValueTask<CalculaCodigoBarrasBoletoCommandResult> Handle(CalculaCodigoBarrasBoletoCommandInput command)
        {
            command.Validate();
            if (command.Invalid)
                return new CalculaCodigoBarrasBoletoCommandResult(false, "Dados incorretos!", null, StatusCodes.Status400BadRequest, command.Notifications);

            var boletoBancario = new BoletoBancario(command.LinhaDigitavel.Replace(" ", "").Replace(".", ""), string.Empty,
                new DateTime(Convert.ToInt32(_dataBaseBacenSettings.Ano), Convert.ToInt32(_dataBaseBacenSettings.Mes), Convert.ToInt32(_dataBaseBacenSettings.Dia)));
            string codigoBarras = await boletoBancario.CalculaCodigoBarras();

            if (!await boletoBancario.ValidaDigitoCodigodeBarras())
                return new CalculaCodigoBarrasBoletoCommandResult(false, "Digito verificador inválido!", null,
               StatusCodes.Status400BadRequest, command.Notifications);

            return new CalculaCodigoBarrasBoletoCommandResult(true, "Sucesso!", new CalculaCodigoBarrasBoletoCommandOutput
            {
                CodigoBarras = codigoBarras,
                DataVencimento = boletoBancario.DataVencimento(),
                Valor = boletoBancario.ValorBoleto()
            },
                StatusCodes.Status200OK, command.Notifications);
        }

        public async ValueTask<CalculaLinhaDigitavelBoletoCommandResult> Handle(CalculaLinhaDigitavelBoletoCommandInput command)
        {
            command.Validate();
            if (command.Invalid)
                return new CalculaLinhaDigitavelBoletoCommandResult(false, "Dados incorretos!", null, StatusCodes.Status400BadRequest, command.Notifications);

            var boletoBancario = new BoletoBancario(string.Empty, command.CodigoBarras,
                new DateTime(Convert.ToInt32(_dataBaseBacenSettings.Ano), Convert.ToInt32(_dataBaseBacenSettings.Mes), Convert.ToInt32(_dataBaseBacenSettings.Dia)));

            int digitoCodigoBarras = await boletoBancario.CalculaDigitoVerificadorCodigoBarras(command.CodigoBarras.Substring(0, 4) + command.CodigoBarras.Substring(5, 99));

            if (digitoCodigoBarras != Convert.ToInt32(command.CodigoBarras.Substring(4, 1)))
            {
                return new CalculaLinhaDigitavelBoletoCommandResult(false, "O digito verificador do código de barras está inválido!", null, StatusCodes.Status400BadRequest, command.Notifications);
            }

            string codigoBarras = boletoBancario.CalculaLinhaDigitavel();

            return new CalculaLinhaDigitavelBoletoCommandResult(true, "Sucesso!", new CalculaLinhaDigitavelBoletoCommandOutPut
            {
                LinhaDigitavel = codigoBarras,
                DataVencimento = boletoBancario.DataVencimento(),
                Valor = boletoBancario.ValorBoleto()
            },
                StatusCodes.Status200OK, command.Notifications);
        }
    }
}
