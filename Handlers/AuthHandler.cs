using DatabaseClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TeamsBackEnd.Models;
using WebApiClient;

namespace TeamsBackEnd.Handlers
{
    public class AuthHandler
    {
        private readonly IConfiguration _config;
        IWebRequestHandler _webRequestHandler;
        public AuthHandler(IConfiguration config,IWebRequestHandler webRequestHandler)
        {
            _webRequestHandler = webRequestHandler;
            _config = config;
        }

        public async Task<string> GetAccessTokenAsync(string code)
        {
            try
            {
                var url = $"{_config.GetValue<string>("GraphAuthUrl")}token";
                var clientId = _config.GetValue<string>("ClientId");
                var clientSecret = _config.GetValue<string>("ClientSecret");
                var redirectUrl = _config.GetValue<string>("RedirectUrl");
                string content = "grant_type=authorization_code&" +
                                "scope=user.read%20mail.read%20Team.ReadBasic.All%20TeamMember.Read.All%20TeamMember.ReadWrite.All%20&" +
                                "code=" + code + "&" +
                                $"client_id={clientId}&" +
                                $"redirect_uri={redirectUrl}&" +
                                $"&client_secret={clientSecret}";
                var responseString =await _webRequestHandler.WebRequestPostAsync(url, content);
                var tokenObj = JsonConvert.DeserializeObject<TokenModel>(responseString);
                return tokenObj.access_token;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetAccessToken {ex.Message}");
                return null;
            }
        }
    }
}
