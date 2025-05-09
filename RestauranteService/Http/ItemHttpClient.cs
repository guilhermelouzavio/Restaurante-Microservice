using System.Text;
using System.Text.Json;
using RestauranteService.Dtos;

namespace RestauranteService.Http
{
    public class ItemHttpClient : IItemHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ItemHttpClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task EnviaRestauranteParaItem(RestauranteReadDto readDto)
        {
            var httpContent = new StringContent(
                   JsonSerializer.Serialize(readDto),
                   Encoding.UTF8,
                   "application/json");

            _httpClient.DefaultRequestHeaders.ConnectionClose = true;

            //var caminho = "http://localhost:5001/api/item/Restaurante"; // $"{_configuration["ItemService"]}";
            var caminho =  $"{_configuration["ItemService"]}";

            try
            {
                var response = await _httpClient.PostAsync(caminho, httpContent);
            }
            catch (Exception ex)
            {

                Console.Write(ex);
            }
            
        }
    }
}