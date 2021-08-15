using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace DrugCatalog.IntegrationTests
{
    public class GetDrugsTest : IClassFixture<WebApplicationFactory<DrugCatalog.Startup>>
    {
        private readonly WebApplicationFactory<DrugCatalog.Startup> _factory;

        public GetDrugsTest(WebApplicationFactory<DrugCatalog.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_ReturnsCompleteList()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/drugs");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }


        [Fact]
        public async Task GetSingle_ReturnsSingle()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/drugs/1");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }
}
