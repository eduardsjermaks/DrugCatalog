using Microsoft.AspNetCore.Mvc.Testing;
using System;
using Xunit;

namespace DrugCatalog.IntegrationTests
{
    public class GetDrugsTest: IClassFixture<WebApplicationFactory<DrugCatalog.Startup>>
    {
        private readonly WebApplicationFactory<DrugCatalog.Startup> _factory;

        public GetDrugsTest(WebApplicationFactory<DrugCatalog.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void GetList_Given_no_parameters_Then_returns_complete_list()
        {
            _factory

        }
    }
}
