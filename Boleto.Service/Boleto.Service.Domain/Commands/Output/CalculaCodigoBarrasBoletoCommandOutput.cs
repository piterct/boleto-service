using System;
using System.Collections.Generic;
using System.Text;

namespace Boleto.Service.Domain.Commands.Output
{
    public class CalculaCodigoBarrasBoletoCommandOutput
    {
        public string CodigoBarras { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
