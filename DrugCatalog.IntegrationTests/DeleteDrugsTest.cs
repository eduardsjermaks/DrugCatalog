using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace DrugCatalog.IntegrationTests
{
    public class DeleteDrugsTest : IClassFixture<WebApplicationFactory<DrugCatalog.Startup>>
    {
        private readonly WebApplicationFactory<DrugCatalog.Startup> _factory;

        public DeleteDrugsTest(WebApplicationFactory<DrugCatalog.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Delete_DeletesDrug()
        {
            // Arrange
            var client = _factory.CreateClient();
            await FixtureHelper.CreateDrug(client);

            // Act
            var response = await client.DeleteAsync("/drugs/1");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }
}
