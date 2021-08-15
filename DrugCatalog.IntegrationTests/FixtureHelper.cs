﻿using DrugCatalog.Features.Drugs;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrugCatalog.IntegrationTests
{
    public static class FixtureHelper
    {
        public static async Task CreateDrug(HttpClient client, string label = "Label 1", string code = "CODE-1")
        {
            var payload = Newtonsoft.Json.JsonConvert.SerializeObject(new CreateDrugCommand()
            {
                Code = code,
                Label = label,
                Description = "Sample description",
                Price = 10
            });
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

            await client.PostAsync("/drugs", content);
        }
    }
}
