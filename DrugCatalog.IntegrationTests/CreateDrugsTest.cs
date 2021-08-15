using DrugCatalog.Features.Drugs;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DrugCatalog.IntegrationTests
{
    public class CreateDrugsTest : IClassFixture<WebApplicationFactory<DrugCatalog.Startup>>
    {
        private readonly WebApplicationFactory<DrugCatalog.Startup> _factory;

        public CreateDrugsTest(WebApplicationFactory<DrugCatalog.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_CreatesValidDrug()
        {
            // Arrange
            var client = _factory.CreateClient();
            var payload = Newtonsoft.Json.JsonConvert.SerializeObject(new CreateDrugCommand()
            {
                Code = "CODE-1",
                Label = "Label 1",
                Description = "Sample description",
                Price = 10
            });
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/drugs", content);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }
}
