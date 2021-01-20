using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal.Account.Manage;

namespace ElsaWebApp.Services.DataAccess
{
    public class MessageService
    {
        private HttpClient _http;
        public MessageService(HttpClient httpClient)
        {
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            _http = httpClient;
            _http.BaseAddress = new Uri("https://localhost:5001/");
        }

        public async Task<PrivateMessage> InsertMessage(PrivateMessage message)
        {
            var responseMessage = await _http.PostAsJsonAsync("message", message);
            var retMessage = await responseMessage.Content.ReadFromJsonAsync<PrivateMessage>();
            return retMessage;
        }

        public async Task<bool> SendMessage(PrivateMessage message, UserGroup group)
        {
            MessageRecipient recipient = new MessageRecipient()
            {
                
            };
            return true;
        }
        
        public async Task<bool> SendMessage(PrivateMessage message, DbUser user)
        {
            
            return true;
        }

        public async Task<List<PrivateMessage>> GetMessages()
        {
            return await _http.GetFromJsonAsync<List<PrivateMessage>>("api/message/GetAll");
        }
    }
}