using Microsoft.AspNetCore.Mvc.Filters;

namespace Nlayer.Apı.Filter
{
    public class ValidateFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors);

                errors.ToList().ForEach(x => { });
            }
        }
    }
}
