using DrugCatalog.Features.Drugs;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DrugCatalog.IntegrationTests
{
    public class UpdateDrugsTest : IClassFixture<WebApplicationFactory<DrugCatalog.Startup>>
    {
        private readonly WebApplicationFactory<DrugCatalog.Startup> _factory;

        public UpdateDrugsTest(WebApplicationFactory<DrugCatalog.Startup> factory)
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
                Code = "CODE-2",
                Label = "Label 2",
                Description = "Sample description changed",
                Price = 20
            });
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync("/drugs/1", content);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }
}
