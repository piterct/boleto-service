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

            var boletoBancario = new BoletoBancario(command.LinhaDigitavel.Replace(" ","").Replace(".",""), string.Empty,
                new DateTime(Convert.ToInt32(_dataBaseBacenSettings.Ano), Convert.ToInt32(_dataBaseBacenSettings.Mes), Convert.ToInt32(_dataBaseBacenSettings.Dia)));
            string codigoBarras = await boletoBancario.CalculaCodigoBarras();

            if (!await boletoBancario.ValidaDigitoCodigodeBarras())
                return new CalculaCodigoBarrasBoletoCommandResult(false, "Digito verificador inválido!", null,
               StatusCodes.Status400BadRequest, command.Notifications);         

            return new CalculaCodigoBarrasBoletoCommandResult(true, "Sucesso!", new CalculaCodigoBarrasBoletoCommandOutput { CodigoBarras = codigoBarras,
                DataVencimento = boletoBancario.DataVencimento(), Valor = boletoBancario.ValorBoleto() },
                StatusCodes.Status200OK, command.Notifications);
        }

        public async ValueTask<CalculaCodigoBarrasBoletoCommandResult> Handle(CalculaLinhaDigitavelBoletoCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new CalculaCodigoBarrasBoletoCommandResult(false, "Dados incorretos!", null, StatusCodes.Status400BadRequest, command.Notifications);

            var boletoBancario = new BoletoBancario(string.Empty, command.CodigoBarras,
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
    }
}
