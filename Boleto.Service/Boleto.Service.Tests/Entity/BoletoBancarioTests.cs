﻿using Boleto.Service.Domain.Entity;
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
    }
}
