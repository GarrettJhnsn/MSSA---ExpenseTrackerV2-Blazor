namespace MyApp.Client.Services.ExpenseService
{
    public interface IExpenseService
    {
        List<Expense> Expenses { get; set; }
        List<ItemSet> ItemSets { get; set; }

        Task GetItemTypes();
        Task GetExpenses();
        Task<Expense> GetSingleExpense(int id);
        Task CreateItem(Expense expense);
        Task UpdateItem(Expense expense);
        Task DeleteItem(int id);

    }
}
