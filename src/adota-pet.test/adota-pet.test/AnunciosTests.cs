using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using api_adota_pet.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;

namespace adota_pet.test
{
    [TestClass]
    public sealed class AnunciosTests
    {
        private static readonly WebApplicationFactory<Program> webApplicationFactory = new();

        [TestMethod]
        public async Task Add()
        {
            dynamic testUser = await new Utils().CreateTestUser(false);
            dynamic testAnuncio = await new Utils().CreateTestAnuncio(testUser.token);
            Assert.IsNotNull(testAnuncio);
        }

        [TestMethod]
        public async Task GetById()
        {
            dynamic testUser = await new Utils().CreateTestUser(false);
            dynamic testAnuncio = await new Utils().CreateTestAnuncio(testUser.token);

            var httpClient = webApplicationFactory.CreateDefaultClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", testUser.token);

            var response = httpClient.GetAsync("api/Anuncios/" + testAnuncio.id).Result;
            if (response.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel resgatar o anuncio teste");
            }
            var responseData = response.Content.ReadAsStringAsync().Result;

            Assert.IsNotNull(responseData);
        }

        [TestMethod]
        public async Task GetAll()
        {
            dynamic testUser = await new Utils().CreateTestUser(false);
            dynamic testAnuncio = await new Utils().CreateTestAnuncio(testUser.token);

            var httpClient = webApplicationFactory.CreateDefaultClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", testUser.token);

            var response = httpClient.GetAsync("api/Anuncios/").Result;
            if (response.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel resgatar os anuncios");
            }
            var responseData = response.Content.ReadAsStringAsync().Result;

            Assert.IsNotNull(responseData);
        }

        [TestMethod]
        public async Task Update()
        {
            dynamic testUser = await new Utils().CreateTestUser(false);
            dynamic testAnuncio = await new Utils().CreateTestAnuncio(testUser.token);

            var httpClient = webApplicationFactory.CreateDefaultClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", testUser.token);

            var testAnuncioUpdate = new
            {
                id = testAnuncio.id,
                titulo = "AnimalTesteUpdate",
                idadeAnimal = 5,
                categoriaAnimal = 0,
                racaAnimal = "RacaTesteUpdate",
                descricao = "DescricaoTesteUpdate",
                imagemCapa = "stringUpdate",
            };
            var testAnuncioUpdateJson = JsonConvert.SerializeObject(testAnuncioUpdate);
            var payload = new StringContent(testAnuncioUpdateJson, Encoding.UTF8, "application/json");
            var response = httpClient.PutAsync("api/Anuncios/" + testAnuncio.id, payload).Result;
            if (response.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel atualizar o anúncio de teste");
            }
            var responseData = response.Content.ReadAsStringAsync().Result;

            Assert.IsNotNull(responseData);
        }

        [TestMethod]
        public async Task Like()
        {
            dynamic testUser = await new Utils().CreateTestUser(false);
            dynamic testAnuncio = await new Utils().CreateTestAnuncio(testUser.token);

            var httpClient = webApplicationFactory.CreateDefaultClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", testUser.token);

            var response = httpClient.PostAsync("api/Anuncios/like/" + testAnuncio.id, null).Result;
            if (response.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel favoritar o anúncio de teste");
            }
            var responseData = response.Content.ReadAsStringAsync().Result;

            Assert.IsNotNull(responseData);
        }

        [TestMethod]
        public async Task Dislike()
        {
            dynamic testUser = await new Utils().CreateTestUser(false);
            dynamic testAnuncio = await new Utils().CreateTestAnuncio(testUser.token);

            var httpClient = webApplicationFactory.CreateDefaultClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", testUser.token);

            var likeResponse = httpClient.PostAsync("api/Anuncios/like/" + testAnuncio.id, null).Result;
            if (likeResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel favoritar o anúncio de teste");
            }

            var dislikeResponse = httpClient.DeleteAsync("api/Anuncios/like/" + testAnuncio.id).Result;
            if (dislikeResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel desfavoritar o anúncio de teste");
            }
            var responseData = dislikeResponse.Content.ReadAsStringAsync().Result;

            Assert.IsNotNull(responseData);
        }

        [TestMethod]
        public async Task GetAllLikes()
        {
            dynamic testUser = await new Utils().CreateTestUser(false);
            dynamic testAnuncio = await new Utils().CreateTestAnuncio(testUser.token);

            var httpClient = webApplicationFactory.CreateDefaultClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", testUser.token);

            var likeResponse = httpClient.PostAsync("api/Anuncios/like/" + testAnuncio.id, null).Result;
            if (likeResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel favoritar o anúncio de teste");
            }

            var getAllResponse = httpClient.GetAsync("api/Anuncios/like").Result;
            if (getAllResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel resgatar os anuncios curtidos");
            }
            var responseData = getAllResponse.Content.ReadAsStringAsync().Result;

            Assert.IsNotNull(responseData);
        }

        [TestMethod]
        public async Task Report()
        {
            dynamic testUser = await new Utils().CreateTestUser(false);
            dynamic testAnuncio = await new Utils().CreateTestAnuncio(testUser.token);

            var httpClient = webApplicationFactory.CreateDefaultClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", testUser.token);

            var response = httpClient.PostAsync("api/Anuncios/report/" + testAnuncio.id, null).Result;
            if (response.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel denunciar o anúncio de teste");
            }
            var responseData = response.Content.ReadAsStringAsync().Result;

            Assert.IsNotNull(responseData);
        }

        [TestMethod]
        public async Task GetAllReports()
        {
            dynamic testUser = await new Utils().CreateTestUser(true);
            dynamic testAnuncio = await new Utils().CreateTestAnuncio(testUser.token);

            var httpClient = webApplicationFactory.CreateDefaultClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", testUser.token);

            var reportResponse = httpClient.PostAsync("api/Anuncios/report/" + testAnuncio.id, null).Result;
            if (reportResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel denunciar o anúncio de teste");
            }

            var getAllResponse = httpClient.GetAsync("api/Anuncios/report").Result;
            if (getAllResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel resgatar os anuncios denunciados");
            }
            var responseData = getAllResponse.Content.ReadAsStringAsync().Result;

            Assert.IsNotNull(responseData);
        }

        [TestMethod]
        public async Task UpdateStatus()
        {
            dynamic testUser = await new Utils().CreateTestUser(false);
            dynamic testAnuncio = await new Utils().CreateTestAnuncio(testUser.token);

            var httpClient = webApplicationFactory.CreateDefaultClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", testUser.token);

            var postResponseEnabled = httpClient.PostAsync("api/Anuncios/" + testAnuncio.id + "/publicado", null).Result;
            if (postResponseEnabled.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel alterar o status do anúncio teste para publicado");
            }

            var postResponseDisabled = httpClient.PostAsync("api/Anuncios/" + testAnuncio.id + "/deletado", null).Result;
            if (postResponseDisabled.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel alterar o status do anúncio teste para deletado");
            }

            Assert.IsTrue(postResponseEnabled.IsSuccessStatusCode == true && postResponseDisabled.IsSuccessStatusCode == true);
        }

        [TestMethod]
        public async Task UpdateStatusAdmin()
        {

            dynamic adminTestUser = await new Utils().CreateTestUser(true);
            dynamic nonAdminTestUser = await new Utils().CreateTestUser(false);
            dynamic testAnuncio = await new Utils().CreateTestAnuncio(nonAdminTestUser.token);

            var httpClient = webApplicationFactory.CreateDefaultClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminTestUser.token);

            var postResponseEnabled = httpClient.PostAsync("api/Anuncios/admin/" + testAnuncio.id + "/publicado", null).Result;
            if (postResponseEnabled.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel alterar o status do anúncio teste para publicado");
            }

            var postResponseDisabled = httpClient.PostAsync("api/Anuncios/admin/" + testAnuncio.id + "/deletado", null).Result;
            if (postResponseDisabled.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel alterar o status do anúncio teste para deletado");
            }

            Assert.IsTrue(postResponseEnabled.IsSuccessStatusCode == true && postResponseDisabled.IsSuccessStatusCode == true);
        }
    }
}