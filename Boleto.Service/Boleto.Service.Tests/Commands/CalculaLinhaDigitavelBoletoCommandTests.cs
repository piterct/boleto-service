using Boleto.Service.Domain.Commands.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Boleto.Service.Tests.Commands
{
    [TestClass]
    public class CalculaLinhaDigitavelBoletoCommandTests
    {
        [TestMethod]
        public void Command_CalculaLinhaDigitavel_CodigoBarras_Legth_Menor_44_Invalido()
        {
            var command = new CalculaLinhaDigitavelBoletoCommandInput("121545421");
            command.Validate();
            Assert.AreEqual(command.Valid, false, "Command de calculo inválido!");
        }
    }
}
