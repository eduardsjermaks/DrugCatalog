namespace DrugCatalog.Entities
{
    public class Drug
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Label { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
