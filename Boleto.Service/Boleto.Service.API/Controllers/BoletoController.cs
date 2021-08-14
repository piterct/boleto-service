using Boleto.Service.Domain.Commands.Input;
using Boleto.Service.Domain.Commands.Result;
using Boleto.Service.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Boleto.Service.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BoletoController : BaseController
    {
        private readonly ILogger<BoletoController> _logger;

        public BoletoController(ILogger<BoletoController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("validaLinhaDigitavel")]
        [ProducesResponseType(typeof(ValidaLinhaDigitavelBoletoCommandResult), StatusCodes.Status200OK)]
        public async ValueTask<IActionResult> GetCustomers([FromServices] BoletoHandler handler, [FromBody] ValidaLinhaDigitavelBoletoCommandInput command)
        {
            try
            {
                var result = await handler.Handle(command);

                return GetResult(result);
            }
            catch (Exception exception)
            {
                _logger.LogError("An exception has occurred at {dateTime}. " +
                 "Exception message: {message}." +
                 "Exception Trace: {trace}", DateTime.UtcNow, exception.Message, exception.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
