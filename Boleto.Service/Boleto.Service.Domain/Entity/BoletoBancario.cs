using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Boleto.Service.Domain.Entity
{
    public class BoletoBancario
    {
        public BoletoBancario(string linhaDigitavel, string codigoBarras, DateTime dataBaseBacen)
        {
            LinhaDigitavel = linhaDigitavel;
            CodigoBarras = codigoBarras;
            DataBaseBacen = dataBaseBacen;
        }

        public string LinhaDigitavel { get; private set; }
        public string CodigoBarras { get; private set; }
        public DateTime DataBaseBacen { get; set; }

        public async ValueTask<string> CalculaCodigoBarras()
        {
            this.CodigoBarras = Regex.Match(this.LinhaDigitavel, @"\d+").Value;

            TamanhoCodigoBarras();

            this.CodigoBarras = this.CodigoBarras.Substring(0, 4)
                + this.CodigoBarras.Substring(32, 15)
                + this.CodigoBarras.Substring(4, 5)
                + this.CodigoBarras.Substring(10, 10)
                + this.CodigoBarras.Substring(21, 10);

            return await Task.FromResult(CodigoBarras);
        }

        public async ValueTask<bool> ValidaDigitoCodigodeBarras()
        {
            if (CalculaDigitoVerificador(this.CodigoBarras.Substring(0, 4) + this.CodigoBarras.Substring(5, 39)) != Convert.ToInt32(this.CodigoBarras.Substring(4, 1)))
                return await Task.FromResult(false);

            return await Task.FromResult(true); ;
        }

        public DateTime DataVencimento()
        {
            return this.DataBaseBacen.AddDays(Convert.ToInt32(this.CodigoBarras.Substring(5, 4)));
        }

        public decimal ValorBoleto()
        {
            string valor = this.CodigoBarras.Substring(9, 8) + ',' + this.CodigoBarras.Substring(17, 2);

            return Convert.ToDecimal(valor);
        }

        private void TamanhoCodigoBarras()
        {
            if (this.CodigoBarras.Length < 47)
            {
                this.CodigoBarras = this.CodigoBarras + "00000000000".Substring(0, 47 - this.CodigoBarras.Length);
            }
        }

        private int CalculaDigitoVerificador(string numero)
        {
            var soma = 0;
            var peso = 2;
            var baseCalculo = 9;
            int contador = numero.Length - 1;
            int entrada = 0;

            for (var i = contador; i >= 0; i--)
            {
                int valorNumeroAoContrario = Convert.ToInt32(numero.Substring(contador - entrada, 1));

                soma = soma + (valorNumeroAoContrario * peso);

                if (peso < baseCalculo)
                {
                    peso++;
                }
                else
                {
                    peso = 2;
                }

                entrada++;
            }

            var digito = 11 - (soma % 11);

            if (digito > 9) digito = 0;

            if (digito == 0) digito = 1;

            return digito;
        }

    }
}
