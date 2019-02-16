using BasicDesk.App.Common.Interfaces;
using BasicDesk.App.Helpers.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BasicDesk.App.Common
{
    public class Alerter : IAlerter
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ITempDataDictionaryFactory tempDataDictionaryFactory;

        public Alerter(IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

        public void AddMessage(MessageType type, string message)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var tempData = tempDataDictionaryFactory.GetTempData(httpContext);
            tempData.Put("__Message", new MessageModel()
            {
                Type = type,
                Message = message
            });
        }
    }
}
