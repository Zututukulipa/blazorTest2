using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using ElsaWebApp.Services.DataAccess;

namespace DataFiller
{
    public class MessageAdd
    {
        private readonly MessageService _service;
        private readonly UserService _userService;
        private HttpClient _client;
        private Random _random = new Random();

        public MessageAdd(HttpClient client)
        {
            _client = client;
            _service = new MessageService(client);
            _userService = new UserService(client);
        }


        public async Task<PrivateMessage> AddMessage()
        {
            PrivateMessage message = new PrivateMessage()
            {
                MessageContent = "Sending test message",
                TimeSent = DateTime.Now
            };

            var msg = await _service.InsertMessage(message);
            return msg;
        }

        public async Task<List<PrivateMessage>> GetMessages()
        {
            return await _service.GetMessages();
        }
    }
}