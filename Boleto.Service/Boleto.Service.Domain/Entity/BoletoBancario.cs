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

            return CodigoBarras;
        }
    }
}
