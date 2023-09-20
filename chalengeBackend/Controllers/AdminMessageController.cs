using ChalengeBackend.Hub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ChalengeBackend.Controllers
{

    [Route("api/AdminMessage")]
    [ApiController]
    public class AdminMessageController : ControllerBase
    {
        private readonly IHubContext<AdminMessageHub> _hubContext;
        private static readonly Queue<string> messageQueue = new Queue<string>();
        private const int MaxQueueLength = 10;

        public AdminMessageController(IHubContext<AdminMessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JsonObject jsonObject) // Use JsonObject from System.Text.Json.Nodes
        {
            if (jsonObject == null)
            {
                return BadRequest("Invalid request.");
            }

            // Check if 'message' exists and is a string
            if (jsonObject.TryGetPropertyValue("message", out var messageNode) && messageNode != null)
            {
                await _hubContext.Clients.All.SendAsync("AdminMessagesReceived", messageNode);
            }
            messageQueue.Enqueue(jsonObject.ToString());


            if (messageQueue.Count >= MaxQueueLength)
            {
                var messages = messageQueue.ToList();
                messageQueue.Clear();
                return Ok(messages);
            }


            return Ok();
        }


    }
}

