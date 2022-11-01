using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client.HttpHandlers
{
    public class AuthentificationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ClientCredentialsTokenRequest _request;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthentificationDelegatingHandler(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _request = new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:5001/connect/token",
                ClientId = "movieClient",
                ClientSecret = "secret",
                Scope = "movieAPI"
            };
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //var client = _httpClientFactory.CreateClient("IdentityClient");

            //var token = await client.RequestClientCredentialsTokenAsync(_request, cancellationToken);
            //if (token.IsError)
            //{
            //    throw new HttpRequestException("Something went wront while requesting the access token.");
            //}

            var token = await _httpContextAccessor.HttpContext
                        .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            if (!string.IsNullOrEmpty(token))
            {
                request.SetBearerToken(token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
