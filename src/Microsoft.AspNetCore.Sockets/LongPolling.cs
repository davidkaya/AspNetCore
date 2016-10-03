﻿using System;
using System.Threading.Tasks;
using Channels;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Sockets
{
    public class LongPolling : IHttpTransport
    {
        private readonly HttpChannel _channel;
        private readonly Connection _connection;

        public LongPolling(Connection connection)
        {
            _connection = connection;
            _channel = (HttpChannel)connection.Channel;
        }

        public async Task ProcessRequest(HttpContext context)
        {
            var buffer = await _channel.Output.ReadAsync();

            if (buffer.IsEmpty && _channel.Output.Reading.IsCompleted)
            {
                // Client should stop if it receives a 204
                context.Response.StatusCode = 204;
                return;
            }

            if (!buffer.IsEmpty)
            {
                try
                {
                    context.Response.ContentLength = buffer.Length;
                    await buffer.CopyToAsync(context.Response.Body);
                }
                finally
                {
                    _channel.Output.Advance(buffer.End);
                }
            }
        }
    }
}
