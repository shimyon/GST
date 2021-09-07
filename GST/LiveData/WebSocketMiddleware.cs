using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveData
{
    public interface IWebSocketHolder
    {
        Task AddAsync(HttpContext context);
    }

    public static class WebSocketHolderMapper
    {
        public static IApplicationBuilder MapWebSocketHolder(this IApplicationBuilder app, PathString path)
        {
            return app.Map(path, (app) => app.UseMiddleware<WebSocketMiddleware>());
        }
    }

    public class WebSocketMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebSocketHolder webSocket;
        public WebSocketMiddleware(RequestDelegate next,
            IWebSocketHolder webSocket)
        {
            this.next = next;
            this.webSocket = webSocket;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest == false)
            {
                context.Response.StatusCode = 400;
                return;
            }
            await webSocket.AddAsync(context);
        }
    }

    public class WebSocketHolder : IWebSocketHolder
    {
        private readonly ILogger<WebSocketHolder> logger;
        private static readonly ConcurrentDictionary<string, WebSocket> clients = new();

        public WebSocketHolder(ILogger<WebSocketHolder> logger)
        {
            this.logger = logger;
        }
        public async Task AddAsync(HttpContext context)
        {
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            if (clients.TryAdd(CreateId(), webSocket))
            {
                await EchoAsync(webSocket);
            }
        }
        private string CreateId()
        {
            return Guid.NewGuid().ToString();
        }
        private async Task EchoAsync(WebSocket webSocket)
        {
            // for sending data
            byte[] buffer = new byte[1024 * 4];
            // Receiving and Sending messages until connections will be closed.
            while (true)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.CloseStatus.HasValue)
                {
                    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
                    clients.TryRemove(clients.First(w => w.Value == webSocket));
                    webSocket.Dispose();
                    break;
                }
                // Send to all clients
                foreach (var c in clients)
                {
                    var bytes = Encoding.Default.GetBytes(DateTime.Now.ToString());
                    var arraySegment = new ArraySegment<byte>(bytes);
                    //new ArraySegment<byte>(buffer, 0, result.Count)
                    await c.Value.SendAsync(
                        arraySegment, result.MessageType, result.EndOfMessage, CancellationToken.None);
                }
            }
        }

        public static async Task SendToAllAsync()
        {
            // Send to all clients
            foreach (var c in clients)
            {
                var bytes = Encoding.Default.GetBytes(DateTime.Now.ToString());
                var arraySegment = new ArraySegment<byte>(bytes);
                //new ArraySegment<byte>(buffer, 0, result.Count)
                await c.Value.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
