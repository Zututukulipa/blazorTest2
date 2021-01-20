using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using ElsaWebApp.Models.Database;
using Microsoft.AspNetCore.Components.Authorization;

namespace ElsaWebApp.Controllers.IdentityManagement
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;

        public CustomAuthStateProvider(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string userFirstname = await _sessionStorageService.GetItemAsync<string>("firstname");
            string uid = await _sessionStorageService.GetItemAsync<string>("userid");
            string userSurname = await _sessionStorageService.GetItemAsync<string>("surname");
            string userEmail = await _sessionStorageService.GetItemAsync<string>("email");
            string userPhone = await _sessionStorageService.GetItemAsync<string>("phone");
            string role = await _sessionStorageService.GetItemAsync<string>("role");

            ClaimsIdentity identity;
            if (role != null)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userFirstname),
                    new Claim(ClaimTypes.Surname, userSurname),
                    new Claim(ClaimTypes.Email, userEmail),
                    new Claim(ClaimTypes.HomePhone, userPhone),
                    new Claim(ClaimTypes.Sid, uid),
                    new Claim(ClaimTypes.Role, role)
                }, "User Authentication");
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var user = new ClaimsPrincipal(identity);
            Thread.CurrentPrincipal = user;
            return await Task.FromResult(new AuthenticationState(user));
        }

        public void AuthenticateUser(DbUser aUser)
        {
            string role = aUser.RoleId switch
            {
                1 => "administrator",
                2 => "professor",
                3 => "student",
                _ => "registered"
            };
            
            _sessionStorageService.SetItemAsync<string>("firstname", aUser.Firstname);
            _sessionStorageService.SetItemAsync<string>("surname", aUser.Surname);
            _sessionStorageService.SetItemAsync<string>("email", aUser.Email);
            _sessionStorageService.SetItemAsync<string>("phone", aUser.Phone);
            _sessionStorageService.SetItemAsync<string>("role", role);
            _sessionStorageService.SetItemAsync<string>("userid", aUser.UserId.ToString());
            
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, aUser.Firstname),
                new Claim(ClaimTypes.Surname, aUser.Surname),
                new Claim(ClaimTypes.Email, aUser.Email),
                new Claim(ClaimTypes.HomePhone, aUser.Phone),
                new Claim(ClaimTypes.Sid, aUser.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, aUser.RoleId.ToString()),
                new Claim(ClaimTypes.Role, role)
            }, "User Authentication");
            var user = new ClaimsPrincipal(identity);
            Thread.CurrentPrincipal = user;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void Deauthorize()
        {
            _sessionStorageService.ClearAsync();

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}