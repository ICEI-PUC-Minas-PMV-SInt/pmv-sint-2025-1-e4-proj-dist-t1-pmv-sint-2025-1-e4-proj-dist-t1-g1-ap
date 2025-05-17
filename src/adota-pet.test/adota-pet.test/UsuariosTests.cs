using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace adota_pet.test
{
    [TestClass]
    public sealed class UsuariosTests
    {
        private static readonly WebApplicationFactory<Program> webApplicationFactory = new();

        [TestMethod]
        public async Task Add()
        {
            dynamic testUser = await new Utils().CreateTestUser(false);
            Assert.IsNotNull(testUser.id);
        }
        
        [TestMethod]
        public async Task Login()
        {
            dynamic testUser = await new Utils().CreateTestUser(false); 
            Assert.IsNotNull(testUser.token);
        }

        [TestMethod]
        public async Task GetById()
        {
            var httpClient = webApplicationFactory.CreateDefaultClient();

            dynamic testUser = await new Utils().CreateTestUser(false);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", testUser.token);

            var response = httpClient.GetAsync("api/Usuarios/" + testUser.id).Result;
            if (response.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel resgatar o usuário teste");
            }
            var responseData = response.Content.ReadAsStringAsync().Result;

            Assert.IsNotNull(responseData);
        }

        [TestMethod]
        public async Task Update()
        {
            var httpClient = webApplicationFactory.CreateDefaultClient();

            dynamic testUser = await new Utils().CreateTestUser(false);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", testUser.token);

            dynamic updatedTestUser = new {
                id = testUser.id,
                email = "updateTest@teste.com",
                senha = "updateTest@teste.com",
                eAdmin = false,
                nome = "updateTest",
                telefone = "321",
                documento = testUser.documento,
            };
            updatedTestUser = JsonConvert.SerializeObject(updatedTestUser);
            var payload = new StringContent(updatedTestUser, Encoding.UTF8, "application/json");

            var response = httpClient.PutAsync("api/Usuarios/" + testUser.id, payload).Result;
            if (response.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel resgatar o usuário teste");
            }
            var responseData = response.Content.ReadAsStringAsync().Result;

            Assert.IsNotNull(responseData);
        }

        [TestMethod]
        public async Task UpdateStatus()
        {
            var httpClient = webApplicationFactory.CreateDefaultClient();

            dynamic testUser = await new Utils().CreateTestUser(false);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", testUser.token);

            var enabledStatusResponse = httpClient.PostAsync("api/Usuarios/" + testUser.id + "/habilitado", null).Result;
            if (enabledStatusResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel alterar o status do usuário teste para habilitado");
            }

            var disabledStatusResponse = httpClient.PostAsync("api/Usuarios/" + testUser.id + "/desabilitado", null).Result;
            if (disabledStatusResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel alterar o status do usuário teste para desabilitado");
            }

            Assert.IsTrue(enabledStatusResponse.IsSuccessStatusCode && disabledStatusResponse.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task UpdateStatusAdmin()
        {
            var httpClient = webApplicationFactory.CreateDefaultClient();

            dynamic adminTestUser = await new Utils().CreateTestUser(true);
            dynamic nonAdminTestUser = await new Utils().CreateTestUser(false);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminTestUser.token);

            var adminEnabledStatusResponse = httpClient.PostAsync("api/Usuarios/admin/" + nonAdminTestUser.id + "/habilitado", null).Result;
            var adminDisabledStatusResponse = httpClient.PostAsync("api/Usuarios/admin/" + nonAdminTestUser.id + "/desabilitado", null).Result;

            if (adminEnabledStatusResponse.IsSuccessStatusCode == false || adminDisabledStatusResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel alterar o status do usuário teste");
            }

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", nonAdminTestUser.token);

            var nonAdminEnabledStatusResponse = httpClient.PostAsync("api/Usuarios/admin/" + adminTestUser.id + "/habilitado", null).Result;
            var nonAdminDisabledStatusResponse = httpClient.PostAsync("api/Usuarios/admin/" + adminTestUser.id + "/desabilitado", null).Result;

            if (nonAdminEnabledStatusResponse.IsSuccessStatusCode == true && nonAdminDisabledStatusResponse.IsSuccessStatusCode == true)
            {
                throw new ArgumentException("O Usuário comum é capaz de alterar o status de outras contas");
            }

            Assert.IsTrue(adminEnabledStatusResponse.IsSuccessStatusCode == true
                && adminDisabledStatusResponse.IsSuccessStatusCode == true 
                && nonAdminEnabledStatusResponse.IsSuccessStatusCode == false 
                && nonAdminDisabledStatusResponse.IsSuccessStatusCode == false);
        }
    }
}