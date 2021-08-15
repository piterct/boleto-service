﻿using Boleto.Service.Domain.Commands.Result;
using Microsoft.AspNetCore.Mvc;

namespace Boleto.Service.API.Controllers
{
    public class BaseController : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetResult(CalculaCodigoBarrasBoletoCommandResult result) => StatusCode(result.StatusCode, result);
    }
}
