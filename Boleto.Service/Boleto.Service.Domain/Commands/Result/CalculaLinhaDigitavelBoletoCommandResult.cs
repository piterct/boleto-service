using Boleto.Service.Domain.Commands.Output;
using Flunt.Notifications;
using System.Collections.Generic;

namespace Boleto.Service.Domain.Commands.Result
{
    public class CalculaLinhaDigitavelBoletoCommandResult
    {
        public CalculaLinhaDigitavelBoletoCommandResult(bool success, string message, CalculaLinhaDigitavelBoletoCommandOutPut data, int statusCode, IEnumerable<Notification> notifications)
        {
            Success = success;
            Message = message;
            Data = data;
            StatusCode = statusCode;
            Notifications = notifications;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public CalculaLinhaDigitavelBoletoCommandOutPut Data { get; set; }
        public int StatusCode { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
    }
}
