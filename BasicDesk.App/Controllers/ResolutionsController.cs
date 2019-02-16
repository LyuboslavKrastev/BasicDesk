using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicDesk.App.Common.Interfaces;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BasicDesk.App.Controllers
{
    public class ResolutionsController : Controller
    {
        private readonly IRequestService requestService;
        private readonly IAlerter alerter;

        public ResolutionsController(IRequestService requestService, IAlerter alerter)
        {
            this.requestService = requestService;
            this.alerter = alerter;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int reqId, string resol)
        {
            await this.requestService.SaveResolutionAsync(reqId, resol);

            alerter.AddMessage(MessageType.Success, $"Successfully saved resolution for request {reqId}");

            return this.Redirect($"/Management/Requests/Manage?id={reqId}");
        }
    }
}