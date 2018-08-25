using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.App.Hubs
{
    public class ChatHub : Hub
    {
        public async Task PostQuestion(string user, string message)
        {
            await this.Clients.All.SendAsync("showQuestion", user, message);
        }
    }
}
