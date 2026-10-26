using DetailingStudio.Data;
using DetailingStudio.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DetailingStudio.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly DetailingStudioDbContext context;

        public ExpensesController(DetailingStudioDbContext context)
        {
            this.context = context;
        }


        public async Task<IActionResult> Index()
        {
            List<Expense> expenses = await context.Expenses
                .Include(e => e.ExpenseCategory)
                .OrderByDescending(e => e.ExpenseDate)
                .ToListAsync();


            return View(expenses);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.ExpenseCategories = await context.ExpenseCategories
                .ToListAsync();

            Expense expense = new Expense
            {
                ExpenseDate = DateTime.Today
            };

            return View(expense);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense expense)
        {
            ModelState.Remove(nameof(Expense.ExpenseCategory));

            if (!ModelState.IsValid)
            {
                ViewBag.ExpenseCategories =
                    await context.ExpenseCategories.ToListAsync();

                return View(expense);
            }

            expense.CreatedOn = DateTime.Now;

            await context.Expenses.AddAsync(expense);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Expense? expense = await context.Expenses
                .Include(e => e.ExpenseCategory)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Expense? expense = await context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id);

            if (expense == null)
            {
                return NotFound();
            }

            ViewBag.ExpenseCategories =
                await context.ExpenseCategories.ToListAsync();

            return View(expense);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            ModelState.Remove(nameof(Expense.ExpenseCategory));

            if (!ModelState.IsValid)
            {
                ViewBag.ExpenseCategories =
                    await context.ExpenseCategories.ToListAsync();

                return View(expense);
            }

            Expense? existingExpense = await context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id);

            if (existingExpense == null)
            {
                return NotFound();
            }

            existingExpense.Description = expense.Description;
            existingExpense.Amount = expense.Amount;
            existingExpense.ExpenseDate = expense.ExpenseDate;
            existingExpense.ExpenseCategoryId = expense.ExpenseCategoryId;
            existingExpense.Notes = expense.Notes;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Expense? expense = await context.Expenses
                .Include(e => e.ExpenseCategory)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Expense? expense = await context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id);

            if (expense == null)
            {
                return NotFound();
            }

            context.Expenses.Remove(expense);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    }
}
