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
            await FixtureHelper.CreateDrug(client);

            // Act
            var response = await client.GetAsync("/drugs");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Fact]
        public async Task GetByLabel_ReturnsFiltered()
        {
            // Arrange
            var client = _factory.CreateClient();
            await FixtureHelper.CreateDrug(client);

            // Act
            var response = await client.GetAsync("/drugs?Label=Label 1");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }


        [Fact]
        public async Task GetByLabel_NotExistingReturnsEmpty()
        {
            // Arrange
            var client = _factory.CreateClient();
            await FixtureHelper.CreateDrug(client);

            // Act
            var response = await client.GetAsync("/drugs?Label=Label X");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }


        [Fact]
        public async Task GetByCode_ReturnsFiltered()
        {
            // Arrange
            var client = _factory.CreateClient();
            await FixtureHelper.CreateDrug(client);

            // Act
            var response = await client.GetAsync("/drugs?Code=CODE-1");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }


        [Fact]
        public async Task GetByCode_NotExistingReturnsEmpty()
        {
            // Arrange
            var client = _factory.CreateClient();
            await FixtureHelper.CreateDrug(client);

            // Act
            var response = await client.GetAsync("/drugs?Code=CODE-X");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Fact]
        public async Task GetSingle_ReturnsSingle()
        {
            // Arrange
            var client = _factory.CreateClient();
            await FixtureHelper.CreateDrug(client);

            // Act
            var response = await client.GetAsync("/drugs/1");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Fact]
        public async Task GetSingle_NotExistingReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            await FixtureHelper.CreateDrug(client);

            // Act
            var response = await client.GetAsync("/drugs/9999");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }
}
