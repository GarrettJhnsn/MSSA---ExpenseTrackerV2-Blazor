namespace MyApp.Shared
{
    public class Expense
    {
        /* Properties For Expenses Set */
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public ItemSet? ItemSet { get; set; }
        public int ItemSetId { get; set; }
    }
}
