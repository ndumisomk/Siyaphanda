using System.Text;
using System.Text.Json;

namespace WebScrapperApp.HttpClients
{
    public class GamesApiClient
    {
        public static async Task CreateGameAsync(Object game)
        {

            try
            {
                var handler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    }
                };

                var client = new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://games.api:8081/game")
                };

                var jsonContent = new StringContent (
                    JsonSerializer.Serialize(game),
                    Encoding.UTF8,
                    "application/json"
                );

                //var dataAsString = JsonSerializer.Serialize(game);
                //var jsonContent = new StringContent(dataAsString, Encoding.Default, "application/json");

                var response = client.PostAsync("", jsonContent).Result;

                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{jsonResponse}\n");
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
