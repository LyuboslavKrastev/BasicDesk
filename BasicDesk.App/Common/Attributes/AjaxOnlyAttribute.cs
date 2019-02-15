using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
{
    public override bool IsValidForRequest(RouteContext routeContext,
        ActionDescriptor action)
    {
        HttpRequest request = routeContext.HttpContext.Request;
        if (request == null)
        {
            return false;
        }

        if (request.Headers != null)
        {
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        return false;
    }
}