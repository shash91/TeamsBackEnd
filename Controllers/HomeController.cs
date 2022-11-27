using ApiClient;
using DatabaseClient;
using DatabaseClient.Data;
using DatabaseClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TeamsBackEnd.Filters;
using TeamsBackEnd.Handlers;
using WebApiClient;

namespace TeamsBackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        private string accessToken = null;
        IWebRequestHandler _webRequestHandler;
        IApiClient _apiClient;
        IDbClient _dbClient;
        public HomeController(IConfiguration config, ApplicationDbContext context, IWebRequestHandler webRequestHandler, IApiClient apiClient, IDbClient dbClient)
        {
            _webRequestHandler = webRequestHandler;
            _config = config;
            _context = context;
            _apiClient = apiClient;
            _dbClient = dbClient;
        }
        public IActionResult Index()
        {
            var url = _config.GetValue<string>("GraphAuthUrl");
            var clientId = _config.GetValue<string>("ClientId");
            var redirectUrl = _config.GetValue<string>("RedirectUrl");
            return Redirect($"{url}authorize?client_id={clientId}&response_type=code&redirect_uri={redirectUrl}&response_mode=query&scope=offline_access%20user.read%20mail.read%20Team.ReadBasic.All%20TeamMember.Read.All%20TeamMember.ReadWrite.All%20");

        }
        public async Task<IActionResult> AuthAsync(string code)
        {
         
            accessToken = await new AuthHandler(_config, _webRequestHandler).GetAccessTokenAsync(code);
            HttpContext.Session.SetString("accessToken", accessToken);                      
            return RedirectToAction("User");
        }

        [CustomAuthorizeActionFilter]
        public async Task<IActionResult> UserAsync()
        {
            var graphApi = _config.GetValue<string>("graphApi");
            var accessToken = HttpContext.Session.GetString("accessToken");           
            var userObject = await _apiClient.GetUserDataAsync(accessToken,graphApi, _webRequestHandler);
            if (userObject != null)
            {
                var userDetails = new UserDetails
                {
                    DisplayName = userObject.DisplayName,
                    Email = userObject.UserPrincipalName
                };
                _dbClient.SaveUser(userDetails, _context);
                return View(userObject);
            }
            else
            {
                return View("Error");
            }
        }

    }
}
