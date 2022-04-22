namespace MyApp.Shared
{
    public class Expense
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int Amount { get; set; }
        public ItemSet? ItemSet { get; set; }
        public int ItemSetId { get; set; }
    }
}
