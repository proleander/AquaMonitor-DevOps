using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AquaMonitor.Api.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        e => e.Key,
                        e => e.Value.Errors.Select(err => err.ErrorMessage).ToArray()
                    );

                context.Result = new BadRequestObjectResult(new
                {
                    message = "Erro de validação",
                    errors
                });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // nada aqui
        }
    }
}
