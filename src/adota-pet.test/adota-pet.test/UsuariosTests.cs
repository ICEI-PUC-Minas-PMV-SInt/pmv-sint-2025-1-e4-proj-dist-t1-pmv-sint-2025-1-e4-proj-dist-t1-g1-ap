using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace adota_pet.test
{
    [TestClass]
    public sealed class UsuariosTests
    {
        private static readonly WebApplicationFactory<Program> webApplicationFactory = new();
        public readonly HttpClient httpClient = webApplicationFactory.CreateDefaultClient();

        [TestMethod]
        public async Task POST_Usuario_Creates_And_Returns_A_New_Usuario()
        {
            var newUser = new
            {
                email = "teste@teste.com",
                senha = "testandoAPI",
                eAdmin = false,
                nome = "Teste",
                telefone = "123",
                documento = "123"
            };
            var newUserJson = JsonConvert.SerializeObject(newUser);
            var payload = new StringContent(newUserJson, Encoding.UTF8, "application/json");
            var postResponse = httpClient.PostAsync("api/Usuarios", payload).Result;
            if (postResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel criar um usuário teste");
            }
            var postJson = postResponse.Content.ReadAsStringAsync().Result;
            dynamic postObject = JsonConvert.DeserializeObject(postJson);
            await httpClient.PostAsync("api/Usuarios/" + postObject.id + "/desabilitado", null);
            Assert.IsNotNull(postJson);
        }

        [TestMethod]
        public async Task GET_Usuario_By_Id_Returns_Usuario()
        {
            var newUser = new
            {
                email = "teste@teste.com",
                senha = "testandoAPI",
                eAdmin = false,
                nome = "Teste",
                telefone = "123",
                documento = "123"
            };
            var newUserJson = JsonConvert.SerializeObject(newUser);
            var payload = new StringContent(newUserJson, Encoding.UTF8, "application/json");
            var postResponse = httpClient.PostAsync("api/Usuarios", payload).Result;
            if (postResponse.IsSuccessStatusCode == false) {
                throw new ArgumentException("Não foi possivel criar um usuário teste");
            }
            var postJson = postResponse.Content.ReadAsStringAsync().Result;
            dynamic postObject = JsonConvert.DeserializeObject(postJson);
            await httpClient.PostAsync("api/Usuarios/" + postObject.id + "/desabilitado", null);

            var getResponse = httpClient.GetAsync("api/Usuarios/" + postObject.id).Result;
            if (getResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel achar o usuário teste");
            }
            var getJson = getResponse.Content.ReadAsStringAsync().Result;
            Assert.IsNotNull(getJson);
        }

        [TestMethod]
        public void POST_Usuario_Status_Changes_Usuario_Status()
        {
            var newUser = new
            {
                email = "teste@teste.com",
                senha = "testandoAPI",
                eAdmin = false,
                nome = "Teste",
                telefone = "123",
                documento = "123"
            };
            var newUserJson = JsonConvert.SerializeObject(newUser);
            var payload = new StringContent(newUserJson, Encoding.UTF8, "application/json");
            var creationResponse = httpClient.PostAsync("api/Usuarios", payload).Result;
            if (creationResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel criar um usuário teste");
            }
            var creationJson = creationResponse.Content.ReadAsStringAsync().Result;
            dynamic creationObject = JsonConvert.DeserializeObject(creationJson);

            var postResponseEnabled = httpClient.PostAsync("api/Usuarios/" + creationObject.id + "/habilitado", null).Result;
            if (postResponseEnabled.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel alterar o status do usuário teste para habilitado");
            }

            var postResponseDisabled = httpClient.PostAsync("api/Usuarios/" + creationObject.id + "/desabilitado", null).Result;
            if (postResponseDisabled.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel alterar o status do usuário teste para desabilitado");
            }
            Assert.IsTrue(postResponseEnabled.IsSuccessStatusCode && postResponseDisabled.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task PUT_Usuario_Updates_Usuario()
        {
            var newUser = new
            {
                email = "teste@teste.com",
                senha = "testandoAPI",
                eAdmin = false,
                nome = "Teste",
                telefone = "123",
                documento = "123"
            };
            var newUserJson = JsonConvert.SerializeObject(newUser);
            var postPayload = new StringContent(newUserJson, Encoding.UTF8, "application/json");
            var postResponse = httpClient.PostAsync("api/Usuarios", postPayload).Result;
            if (postResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel criar um usuário teste");
            }
            var postJson = postResponse.Content.ReadAsStringAsync().Result;
            dynamic postObject = JsonConvert.DeserializeObject(postJson);
            await httpClient.PostAsync("api/Usuarios/" + postObject.id + "/desabilitado", null);

            var userUpdate = new
            {
                id = postObject.id,
                email = "testeUpdate@teste.com",
                senha = "testandoUpdateAPI",
                eAdmin = false,
                nome = "TesteUpdate",
                telefone = "123Update",
                documento = "123Update"
            };
            var userUpdateJson = JsonConvert.SerializeObject(userUpdate);
            var putPayload = new StringContent(userUpdateJson, Encoding.UTF8, "application/json");
            var putResponse = httpClient.PutAsync("api/Usuarios/" + postObject.id, putPayload).Result;
            if (putResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel atualizar o usuário de teste");
            }
            Assert.IsTrue(putResponse.IsSuccessStatusCode);
        }
    }
}