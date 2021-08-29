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
            this.CodigoBarras = Regex.Replace(this.LinhaDigitavel, @"[^\d]", "");

            TamanhoLinhaDigitavel();

            this.CodigoBarras = this.CodigoBarras.Substring(0, 4)
                + this.CodigoBarras.Substring(32, 15)
                + this.CodigoBarras.Substring(4, 5)
                + this.CodigoBarras.Substring(10, 10)
                + this.CodigoBarras.Substring(21, 10);

            return await Task.FromResult(CodigoBarras);
        }

        public async ValueTask<string> CalculaLinhaDigitavel()
        {
            this.LinhaDigitavel = this.CodigoBarras;

            var campo1 = this.LinhaDigitavel.Substring(0, 4) + this.LinhaDigitavel.Substring(19, 1) + '.' + this.LinhaDigitavel.Substring(20, 4);
            var campo2 = this.LinhaDigitavel.Substring(24, 5) + '.' + this.LinhaDigitavel.Substring(24 + 5, 5);
            var campo3 = this.LinhaDigitavel.Substring(34, 5) + '.' + this.LinhaDigitavel.Substring(34 + 5, 5);
            var campo4 = this.LinhaDigitavel.Substring(4, 1); // Digito verificador
            var campo5 = this.LinhaDigitavel.Substring(5, 14); // Vencimento + Valor

            if (Convert.ToInt64(campo5) == 0)
            {
                campo5 = "000";
            }

            this.LinhaDigitavel = campo1 + CalculoPadraoCodigoDeBarras(campo2).Result.ToString()
                + ' ' +
                campo2 + CalculoPadraoCodigoDeBarras(campo2).Result.ToString()
                + ' ' +
                campo3 + CalculoPadraoCodigoDeBarras(campo3).Result.ToString()
                + ' ' +
                campo4
                + ' ' +
                campo5;

            return await Task.FromResult(this.LinhaDigitavel);
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

        private void TamanhoLinhaDigitavel()
        {
            if (this.CodigoBarras.Length < 47)
            {
                this.CodigoBarras = this.CodigoBarras + "00000000000".Substring(0, 47 - this.CodigoBarras.Length);
            }
        }

        public async ValueTask<int> CalculoPadraoCodigoDeBarras(string numero)
        {
            numero = Regex.Match(numero, @"\d+").Value;

            var soma = 0;
            var peso = 2;
            int contador = numero.Length - 1;

            while (contador >= 0)
            {
                var multiplicacao = (Convert.ToInt32(numero.Substring(contador, 1)) * peso);

                if (multiplicacao >= 10)
                {
                    multiplicacao = 1 + (multiplicacao - 10);
                }

                soma = soma + multiplicacao;

                if (peso == 2)
                {
                    peso = 1;
                }
                else
                {
                    peso = 2;
                }

                contador = contador - 1;

            }

            var digito = 10 - (soma % 10);

            if (digito == 10) digito = 0;

            return await Task.FromResult(digito);
        }

        public int CalculaDigitoVerificador(string numero)
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
