using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace adota_pet.test
{
    public class Utils
    {
        private static readonly WebApplicationFactory<Program> webApplicationFactory = new();

        public async Task<dynamic> CreateTestUser(Boolean admin)
        {
            var random = new Random();
            var httpClient = webApplicationFactory.CreateDefaultClient();

            string documento = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 40).Select(s => s[random.Next(s.Length)]).ToArray());
            string senha = "testandoAPI";

            var creationObject = new
            {
                email = "teste@teste.com",
                senha = senha,
                eAdmin = admin,
                nome = "Teste",
                telefone = "123",
                documento = documento
            };

            var creationJson = JsonConvert.SerializeObject(creationObject);
            var creationPayload = new StringContent(creationJson, Encoding.UTF8, "application/json");
            var creationResponse = httpClient.PostAsync("api/Usuarios", creationPayload).Result;

            if (creationResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel criar um usuário teste");
            }

            var creationResponseJson = creationResponse.Content.ReadAsStringAsync().Result;
            dynamic creationResponseObject = JsonConvert.DeserializeObject(creationResponseJson);


            var loginObject = new
            {
                documento = documento,
                senha = senha,
            };

            var loginJson = JsonConvert.SerializeObject(loginObject);
            var loginPayload = new StringContent(loginJson, Encoding.UTF8, "application/json");
            var loginResponse = httpClient.PostAsync("api/Usuarios/authenticate", loginPayload).Result;

            if (loginResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel gerar o Token de autentificação do usuário teste");
            }

            var loginResponseJson = loginResponse.Content.ReadAsStringAsync().Result;
            dynamic loginResponseObject = JsonConvert.DeserializeObject(loginResponseJson);

            dynamic token = loginResponseObject.jwtToken;
            token = token.ToString();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await httpClient.PostAsync("/api/Usuarios/" + creationResponseObject.id + "/desabilitado", null);

            return new {
                id = creationResponseObject.id,
                documento = documento,
                token = token,
            };
        }

        public async Task<dynamic> CreateTestAnuncio(string token)
        {
            var httpClient = webApplicationFactory.CreateDefaultClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var creationObject = new
            {
                titulo = "AnimalTeste",
                idadeAnimal = 5,
                categoriaAnimal = 0,
                racaAnimal = "RacaTeste",
                descricao = "DescricaoTeste",
                imagemCapa = "string",
            };

            var creationJson = JsonConvert.SerializeObject(creationObject);
            var payload = new StringContent(creationJson, Encoding.UTF8, "application/json");
            var postResponse = httpClient.PostAsync("api/Anuncios", payload).Result;
            if (postResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel criar um anúncio teste");
            }
            var postJson = postResponse.Content.ReadAsStringAsync().Result;
            dynamic postObject = JsonConvert.DeserializeObject(postJson);
            await httpClient.PostAsync("api/Anuncios/" + postObject.id + "/deletado", null);

            return postObject;
        }
    }
}
