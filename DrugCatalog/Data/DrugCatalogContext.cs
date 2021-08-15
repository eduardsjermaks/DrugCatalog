using Microsoft.EntityFrameworkCore;

namespace DrugCatalog.Data
{
    public class DrugCatalogContext: DbContext
    {
        public DrugCatalogContext(DbContextOptions<DrugCatalogContext> options) : base(options)
        {
        }
    }
}
