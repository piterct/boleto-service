using Boleto.Service.Domain.Commands.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Boleto.Service.Tests.Commands
{
    [TestClass]
    public class CalculaCodigoBarrasBoletoCommandTests
    {
        [TestMethod]
        public void Command_CalculaCodigoBarras_LinhaDigitavel_Legth_Menor_36_Invalido()
        {
            var command = new CalculaCodigoBarrasBoletoCommandInput("121545421", 20M, DateTime.Now);
            command.Validate();
            Assert.AreEqual(command.Valid, false, "Command de calculo inválido!");
        }

        [TestMethod]
        public void Command_CalculaCodigoBarras_LinhaDigitavel_Valor_Nulo_Invalido()
        {
            var command = new CalculaCodigoBarrasBoletoCommandInput(null, 20M, DateTime.Now);
            command.Validate();
            Assert.AreEqual(command.Valid, false, "Command de calculo inválido!");
        }

        [TestMethod]
        public void Command_CalculaCodigoBarras_LinhaDigitavel_Valida()
        {
            var command = new CalculaCodigoBarrasBoletoCommandInput("03399699255870000180185108001018874650000010000", 20M, DateTime.Now);
            command.Validate();
            Assert.AreEqual(command.Valid, true, "Command de calculo válido!");
        }
    }
}
