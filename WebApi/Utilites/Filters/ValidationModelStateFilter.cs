using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Model;


namespace WebApi.Utilites.Filters
{
    public class ValidationModelStateFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var response = new ApiResponse(context.ModelState);
                context.Result= new JsonResult(response);
            }
        }
    }
}
