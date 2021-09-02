using Boleto.Service.Domain.Commands.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Boleto.Service.Tests.Commands
{
    [TestClass]
    public class CalculaLinhaDigitavelBoletoCommandTests
    {
        [TestMethod]
        public void Command_CalculaLinhaDigitavel_CodigoBarras_Length_Menor_44_Invalido()
        {
            var command = new CalculaLinhaDigitavelBoletoCommandInput("121545421");
            command.Validate();
            Assert.AreEqual(command.Valid, false, "Command de calculo inválido!");
        }


        [TestMethod]
        public void Command_CalculaLinhaDigitavel_CodigoBarras_Length_Maior_44_Invalido()
        {
            var command = new CalculaLinhaDigitavelBoletoCommandInput("0339874650000010000969925870000180851080010154542121");
            command.Validate();
            Assert.AreEqual(command.Valid, false, "Command de calculo inválido!");
        }

        [TestMethod]
        public void Command_CalculaLinhaDigitavel_CodigoBarras_Length_Valido()
        {
            var command = new CalculaLinhaDigitavelBoletoCommandInput("03398746500000100009699258700001808510800101");
            command.Validate();
            Assert.AreEqual(command.Valid, true, "Command de calculo inválido!");
        }

    }
}
