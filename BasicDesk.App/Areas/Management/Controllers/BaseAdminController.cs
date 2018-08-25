using BasicDesk.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicDesk.App.Areas.Management.Controllers
{
    [Area(WebConstants.ManagementArea)]
    [Authorize(Roles = WebConstants.AdminRole)]
    public abstract class BaseAdminController : Controller{}
}