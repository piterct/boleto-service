using System.Text.RegularExpressions;

namespace Boleto.Service.Domain.Entity
{
    public class BoletoBancario
    {
        public BoletoBancario(string linhaDigitavel)
        {
            LinhaDigitavel = linhaDigitavel;
        }

        public string LinhaDigitavel { get; private set; }
        public string CodigoBarras { get; set; }

        public string CalculaCodigoBarras()
        {
            this.CodigoBarras = Regex.Match(this.LinhaDigitavel, @"\d+").Value;

            TamanhoCodigoBarras();

            this.CodigoBarras = this.CodigoBarras.Substring(0, 4)
                + this.CodigoBarras.Substring(32, 15)
                + this.CodigoBarras.Substring(4, 5)
                + this.CodigoBarras.Substring(10, 10)
                + this.CodigoBarras.Substring(21, 10);

            return CodigoBarras;
        }

        private void TamanhoCodigoBarras()
        {
            if (this.CodigoBarras.Length < 47)
            {
                this.CodigoBarras = this.CodigoBarras + "00000000000".Substring(0, 47 - this.CodigoBarras.Length);
            }
        }

        
    }
}
