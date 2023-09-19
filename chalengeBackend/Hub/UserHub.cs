namespace ChalengeBackend.Hub
{

    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;

    public class UserHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveGetAllUserMessage", message);
        }
    }
}
