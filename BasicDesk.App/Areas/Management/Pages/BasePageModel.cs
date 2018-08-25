using BasicDesk.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BasicDesk.App.Areas.Management.Pages
{
    [Area(WebConstants.ManagementArea)]
    [Authorize(Roles = WebConstants.ManagementRoles)]
    public class BasePageModel : PageModel
    {
    }
}
