using Boleto.Service.Domain.Commands.Output;
using Flunt.Notifications;
using System.Collections.Generic;

namespace Boleto.Service.Domain.Commands.Result
{
    public class ValidaLinhaDigitavelBoletoCommandResult
    {
        public ValidaLinhaDigitavelBoletoCommandResult(bool success, string message, ValidaLinhaDigitavelBoletoCommandOutput data, int statusCode, IEnumerable<Notification> notifications)
        {
            Success = success;
            Message = message;
            Data = data;
            StatusCode = statusCode;
            Notifications = notifications;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public ValidaLinhaDigitavelBoletoCommandOutput Data { get; set; }
        public int StatusCode { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
    }
}
