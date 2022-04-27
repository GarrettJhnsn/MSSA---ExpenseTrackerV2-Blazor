using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace MyApp.Client.Services.ExpenseService
{
    public class ExpenseService : IExpenseService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public ExpenseService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager; 
        }

        public List<Expense> Expenses { get; set; } = new();
        public List<ItemSet> ItemSets { get; set; } = new();

        /* Calls API To Fill ItemSets List */
        public async Task GetItemTypes()
        {
            var result = await _http.GetFromJsonAsync<List<ItemSet>>("api/expense/itemtypes");
            if (result != null)
                ItemSets = result;
                
        }
        /* Calls API To Fill Expenses List */
        public async Task GetExpenses()
        {
            var result = await _http.GetFromJsonAsync<List<Expense>>("api/expense");
            if (result != null)
                Expenses = result;
                
        }
        /* Calls API To Grab Specific Expenses From List Based On id */
        public async Task<Expense> GetSingleExpense(int id)
        {
            var result = await _http.GetFromJsonAsync<Expense>($"api/expense/{id}");
            if (result != null)
                return result;  
            throw new Exception("Expense not found!");
        }
        /* Called After Create/Update/Delete Methods Successfully Call API/Make Changes. Sends User Back To Main List */
        private async Task SetItems(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Expense>>();
            if(response != null)
                
            _navigationManager.NavigateTo("list", forceLoad:true);
        }
        /* Calls API To Add New Expense To List //ToDB */
        public async Task CreateItem(Expense expense)
        {
            var result = await _http.PostAsJsonAsync("api/expense", expense);
            await SetItems(result);

        }
        /* Calls API To Update Single Expense Based On expense.Id //ToDB */
        public async Task UpdateItem(Expense expense)
        {
            var result = await _http.PutAsJsonAsync($"api/expense/{expense.Id}", expense);
            await SetItems(result);
        }
        /* Calls API To Delete Single Expense Based On id //FromDB */
        public async Task DeleteItem(int id)
        {
            var result = await _http.DeleteAsync($"api/expense/{id}");
            await SetItems(result);   
        }
    }
}
