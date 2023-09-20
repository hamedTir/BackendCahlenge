namespace ChalengeBackend.Hub
{
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;
    public class AdminMessageHub : Hub
    {
        public async Task SendMessagesToClient(string messages)
        {
            await Clients.All.SendAsync("AdminMessagesReceived", messages);
        }
    }
}
