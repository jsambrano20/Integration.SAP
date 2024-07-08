using Integration.SAP.Application.Services.Material.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Integration.SAP.Application.Services.ExternalService
{
    public class SapService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _serviceAPIBaseUrl;
        private readonly string _USER;
        private readonly string _PASS;

        public SapService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

            // Configuração da URL base da API de Billing a partir do appsettings.json
            _serviceAPIBaseUrl = configuration.GetSection("ExternalServices:SapApi:BaseUrl").Value;
            _USER = configuration.GetSection("ExternalServices:SapApi:USER").Value;
            _PASS = configuration.GetSection("ExternalServices:SapApi:PASSWORD").Value;
        }

        private HttpClient CreateHttpClientWithCustomHandler()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            return new HttpClient(handler);
        }

        private void AddBasicAuthentication(HttpClient client)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{_USER}:{_PASS}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<HttpResponseMessage> GetMaterialSapDataAsync()
        {
            try
            {
                var httpClient = CreateHttpClientWithCustomHandler();
                AddBasicAuthentication(httpClient);

                var response = await httpClient.GetAsync($"{_serviceAPIBaseUrl}");
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Erro ao obter dados de faturamento", ex);
            }
        }

        public async Task<HttpResponseMessage> PostMaterialSapDataAsync(List<MaterialDto> dtos)
        {
            try
            {
                var httpClient = CreateHttpClientWithCustomHandler();
                AddBasicAuthentication(httpClient);

                var jsonContent = JsonConvert.SerializeObject(dtos);

                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(_serviceAPIBaseUrl, httpContent);
                response.EnsureSuccessStatusCode();

                return response;
            }
            catch (HttpRequestException ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message;
                throw new ApplicationException($"Erro ao enviar dados de material: {innerExceptionMessage}", ex);
            }
        }


    }
}
