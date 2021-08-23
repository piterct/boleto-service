using Boleto.Service.Domain.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Boleto.Service.Tests.Entity
{
    [TestClass]
    public class BoletoBancarioTests
    {
        private readonly DateTime _dataBaseBacen;
        public BoletoBancarioTests()
        {           
            _dataBaseBacen = new DateTime(1997, 10, 7);
        }

        [TestMethod]
        public  void LinhaDigitavel_Invalida()
        {
            string linhaDigitavelInvalida = "01399699255870000180185108001018874650000010000";
           
            var boletoBancario = new BoletoBancario(linhaDigitavelInvalida, string.Empty, _dataBaseBacen);

            var codigoBarras =  boletoBancario.CalculaCodigoBarras().Result;

            Assert.AreEqual(boletoBancario.ValidaDigitoCodigodeBarras().Result, false, "Linha digitavel inválida!");
        }

        [TestMethod]
        public void LinhaDigitavel_Valida()
        {
            string linhaDigitavelValida = "03399699255870000180185108001018874650000010000";

            var boletoBancario = new BoletoBancario(linhaDigitavelValida, string.Empty, _dataBaseBacen);

            var codigoBarras = boletoBancario.CalculaCodigoBarras().Result;

            Assert.AreEqual(boletoBancario.ValidaDigitoCodigodeBarras().Result, true, "Linha digitavel valida!");
        }


        [TestMethod]
        public void Data_Vencimento_Boleto_Invalida()
        {
            string linhaDigitavelValida = "03399699255870000180185108001018874650000010000";

            DateTime dataVencimentoValida = new DateTime(2019, 03, 16);

            var boletoBancario = new BoletoBancario(linhaDigitavelValida, string.Empty, _dataBaseBacen);

            var codigoBarras = boletoBancario.CalculaCodigoBarras().Result;

            DateTime dataVencimentoBoleto = boletoBancario.DataVencimento();

            Assert.AreNotEqual(dataVencimentoValida, dataVencimentoBoleto, "Data vencimento boleto inválida!");
        }

        [TestMethod]
        public void Data_Vencimento_Boleto_Valida()
        {
            string linhaDigitavelValida = "03399699255870000180185108001018874650000010000";

            DateTime dataVencimentoValida = new DateTime(2018, 03, 16);

            var boletoBancario = new BoletoBancario(linhaDigitavelValida, string.Empty, _dataBaseBacen);

            var codigoBarras = boletoBancario.CalculaCodigoBarras().Result;

            DateTime dataVencimentoBoleto = boletoBancario.DataVencimento();

            Assert.AreEqual(dataVencimentoValida, dataVencimentoBoleto, "Data vencimento boleto valida!");
        }



        [TestMethod]
        public void Valor_Boleto_Invalido()
        {
            string linhaDigitavelValida = "03399699255870000180185108001018874650000010000";

            decimal valorBoletoInvalido = 555M;

            var boletoBancario = new BoletoBancario(linhaDigitavelValida, string.Empty, _dataBaseBacen);

            var codigoBarras = boletoBancario.CalculaCodigoBarras().Result;

            decimal valorBoleto = boletoBancario.ValorBoleto();

            Assert.AreNotEqual(valorBoletoInvalido, valorBoleto, "Valor boleto inválido!");
        }


        [TestMethod]
        public void CalculaDigitoVerificadorCodigoBarras_Valido()
        {
            string linhaDigitavelValida = "03399699255870000180185108001018874650000010000";

            var boletoBancario = new BoletoBancario(linhaDigitavelValida, string.Empty, _dataBaseBacen);

           var digitoVerificador = boletoBancario.CalculaDigitoVerificadorCodigoBarras("399903512").Result;

            Assert.AreEqual(digitoVerificador, 8, "Digito verificado codigo de barras valido!");
        }

        [TestMethod]
        public void CalculaDigitoVerificadorCodigoBarras_Invalido()
        {
            string linhaDigitavelValida = "03399699255870000180185108001018874650000010000";

            var boletoBancario = new BoletoBancario(linhaDigitavelValida, string.Empty, _dataBaseBacen);

            var digitoVerificador = boletoBancario.CalculaDigitoVerificadorCodigoBarras("39990351254521").Result;

            Assert.AreNotEqual(digitoVerificador, 8, "Digito verificado codigo de barras valido!");
        }

    }
}
