using BlazorExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.Data.Repositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllExpenses();

        Task<Expense> GetExpenseDetails(int id);

        Task<bool> InsertExpenseDetails(Expense expense);

        Task<bool> UpdateExpense(Expense expense);

        Task<bool> DeleteExpense(int id);
    }
}
