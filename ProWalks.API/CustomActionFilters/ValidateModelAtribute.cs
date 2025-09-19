using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ProWalks.API.CustomActionFilters
{
    public class ValidateModelAtribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}
