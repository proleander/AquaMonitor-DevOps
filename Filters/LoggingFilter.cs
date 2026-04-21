using Microsoft.AspNetCore.Mvc.Filters;

namespace AquaMonitor.Api.Filters
{
    public class LoggingFilter : IActionFilter
    {
        private readonly ILogger<LoggingFilter> _logger;

        public LoggingFilter(ILogger<LoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"➡️ Entrando em: {actionName}");

            // Loga os parâmetros da request
            foreach (var param in context.ActionArguments)
            {
                _logger.LogInformation($"Parametro: {param.Key} = {System.Text.Json.JsonSerializer.Serialize(param.Value)}");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"⬅️ Saindo de: {actionName}");

            if (context.Exception != null)
            {
                _logger.LogError(context.Exception, "Erro ocorrido na Action");
            }
        }
    }
}
