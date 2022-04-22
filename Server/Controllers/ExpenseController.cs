using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MyApp.Server.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly DataContext _context;

        public ExpenseController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Expense>>> GetExpenses()
        {
            var expenses = await _context.Expenses.Include(i => i.ItemSet).ToListAsync();
            return Ok(expenses);
        }

        [HttpGet("itemtypes")]
        public async Task<ActionResult<List<ItemSet>>> GetItemTypes()
        {
            var itemTypes = await _context.ItemTypes.ToListAsync();
            return Ok(itemTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetSingleItem(int id)
        {
            var expense = await _context.Expenses
                .Include(i => i.ItemSet)
                .FirstOrDefaultAsync(i => i.Id == id);
            if(expense == null)
            {
                return NotFound("Sorry, no expense found.");
            }
            return Ok(expense);
        }

        [HttpPost]
        public async Task<ActionResult<List<Expense>>> CreateItem(Expense expense)
        {
            expense.ItemSet = null;
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return Ok(await GetDBExpenses());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Expense>>> UpdateItem(Expense expense, int id)
        {
            var dbExpense = await _context.Expenses
                .Include(i => i.ItemSet)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (dbExpense == null)
                return NotFound("Nothing Found");

            dbExpense.Name = expense.Name;
            dbExpense.Amount = expense.Amount;
            dbExpense.ItemSetId = expense.ItemSetId;

            await _context.SaveChangesAsync();

            return Ok(await GetDBExpenses());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Expense>>> DeleteItem(int id)
        {
            var dbExpense = await _context.Expenses
                .Include(i => i.ItemSet)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (dbExpense == null)
                return NotFound("Nothing Found");

            _context.Expenses.Remove(dbExpense);

            await _context.SaveChangesAsync();

            return Ok(await GetDBExpenses());
        }



        private async Task<List<Expense>> GetDBExpenses()
        {
            return await _context.Expenses.Include(i => i.ItemSet).ToListAsync();
        }

    }
}
