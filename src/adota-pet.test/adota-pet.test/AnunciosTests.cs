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
        public readonly HttpClient httpClient = webApplicationFactory.CreateDefaultClient();

        [TestMethod]
        public async Task POST_Anuncio_Creates_And_Returns_A_New_Anuncio()
        {
            var newAnuncio = new
            {
                titulo = "AnimalTeste",
                idadeAnimal = 5,
                categoriaAnimal = 0,
                racaAnimal = "RacaTeste",
                descricao = "DescricaoTeste",
                imagemCapa = "string",
                usuarioId = 3
            };
            var newAnuncioJson = JsonConvert.SerializeObject(newAnuncio);
            var payload = new StringContent(newAnuncioJson, Encoding.UTF8, "application/json");
            var postResponse = httpClient.PostAsync("api/Anuncios", payload).Result;
            if (postResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel criar um anúncio teste");
            }
            var postJson = postResponse.Content.ReadAsStringAsync().Result;
            dynamic postObject = JsonConvert.DeserializeObject(postJson);
            await httpClient.PostAsync("api/Anuncios/" + postObject.id + "/deletado", null);
            Assert.IsNotNull(postJson);
        }

        [TestMethod]
        public async Task GET_Anuncio_By_Id_Returns_Anuncio()
        {
            var newAnuncio = new
            {
                titulo = "AnimalTeste",
                idadeAnimal = 5,
                categoriaAnimal = 0,
                racaAnimal = "RacaTeste",
                descricao = "DescricaoTeste",
                imagemCapa = "string",
                usuarioId = 3
            };
            var newAnuncioJson = JsonConvert.SerializeObject(newAnuncio);
            var payload = new StringContent(newAnuncioJson, Encoding.UTF8, "application/json");
            var postResponse = httpClient.PostAsync("api/Anuncios", payload).Result;
            if (postResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel criar um anúncio teste");
            }
            var postJson = postResponse.Content.ReadAsStringAsync().Result;
            dynamic postObject = JsonConvert.DeserializeObject(postJson);
            await httpClient.PostAsync("api/Anuncios/" + postObject.id + "/deletado", null);

            var getResponse = httpClient.GetAsync("api/Anuncios/" + postObject.id).Result;
            if (getResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel achar o anúncio teste");
            }
            var getJson = getResponse.Content.ReadAsStringAsync().Result;
            Assert.IsNotNull(getJson);
        }

        [TestMethod]
        public void POST_Anuncio_Status_Changes_Anuncio_Status()
        {
            var newAnuncio = new
            {
                titulo = "AnimalTeste",
                idadeAnimal = 5,
                categoriaAnimal = 0,
                racaAnimal = "RacaTeste",
                descricao = "DescricaoTeste",
                imagemCapa = "string",
                usuarioId = 3
            };
            var newAnuncioJson = JsonConvert.SerializeObject(newAnuncio);
            var payload = new StringContent(newAnuncioJson, Encoding.UTF8, "application/json");
            var creationResponse = httpClient.PostAsync("api/Anuncios", payload).Result;
            if (creationResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel criar um anúncio teste");
            }
            var creationJson = creationResponse.Content.ReadAsStringAsync().Result;
            dynamic creationObject = JsonConvert.DeserializeObject(creationJson);

            var postResponseEnabled = httpClient.PostAsync("api/Anuncios/" + creationObject.id + "/publicado", null).Result;
            if (postResponseEnabled.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel alterar o status do anúncio teste para publicado");
            }

            var postResponseDisabled = httpClient.PostAsync("api/Anuncios/" + creationObject.id + "/deletado", null).Result;
            if (postResponseDisabled.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel alterar o status do anúncio teste para deletado");
            }
            Assert.IsTrue(postResponseEnabled.IsSuccessStatusCode && postResponseDisabled.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task PUT_Anuncio_Updates_Anuncio()
        {
            var newAnuncio = new
            {
                titulo = "AnimalTeste",
                idadeAnimal = 5,
                categoriaAnimal = 0,
                racaAnimal = "RacaTeste",
                descricao = "DescricaoTeste",
                imagemCapa = "string",
                usuarioId = 3
            };
            var newAnuncioJson = JsonConvert.SerializeObject(newAnuncio);
            var postPayload = new StringContent(newAnuncioJson, Encoding.UTF8, "application/json");
            var postResponse = httpClient.PostAsync("api/Anuncios", postPayload).Result;
            if (postResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel criar um anúncio teste");
            }
            var postJson = postResponse.Content.ReadAsStringAsync().Result;
            dynamic postObject = JsonConvert.DeserializeObject(postJson);
            await httpClient.PostAsync("api/Anuncios/" + postObject.id + "/deletado", null);

            var anuncioUpdate = new
            {
                id = postObject.id,
                titulo = "AnimalTesteUpdate",
                idadeAnimal = 5,
                categoriaAnimal = 0,
                racaAnimal = "RacaTesteUpdate",
                descricao = "DescricaoTesteUpdate",
                imagemCapa = "stringUpdate",
                usuarioId = 3
            };
            var anuncioUpdateJson = JsonConvert.SerializeObject(anuncioUpdate);
            var putPayload = new StringContent(anuncioUpdateJson, Encoding.UTF8, "application/json");
            var putResponse = httpClient.PutAsync("api/Anuncios/" + postObject.id, putPayload).Result;
            if (putResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel atualizar o anúncio de teste");
            }
            await httpClient.PostAsync("api/Anuncios/" + postObject.id + "/deletado", null);
            Assert.IsTrue(putResponse.IsSuccessStatusCode);
        }

        [TestMethod]
        public void Get_Anuncios_Returns_All_Existing_Anuncios()
        {
            var getResponse = httpClient.GetAsync("api/Anuncios/").Result;
            if (getResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException("Não foi possivel achar os anúncios");
            }
            var getJson = getResponse.Content.ReadAsStringAsync().Result;
            dynamic getObject = JsonConvert.DeserializeObject(getJson);
            Assert.IsTrue(getObject is IEnumerable<object>);
        }
    }
}