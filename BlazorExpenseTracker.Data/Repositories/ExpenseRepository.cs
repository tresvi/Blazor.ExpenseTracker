using BlazorExpenseTracker.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.Data.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private SqlConfiguration _connectionString;

        public ExpenseRepository(SqlConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteExpense(int id)
        {
            var db = dbConnection();
            var sql = @"Delete Expenses Where Id = @Id";
            int result = await db.ExecuteAsync(sql, new { id });
            return result > 0;
        }


        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT e.Id, Amount, CategoryId, ExpenseType, TransactionDate, 
                               c.Id, c.Name
                        FROM Expenses e
                            INNER JOIN Categories c ON e.CategoryId = c.Id ";

            var result = await db.QueryAsync<Expense, Category, Expense>(sql,
                ((expense, category) =>
                {
                    expense.Category = category;
                    return expense;
                }), new { }, splitOn: "Id");

            return result;
        }


        public async Task<Expense> GetExpenseDetails(int id)
        {
            var db = dbConnection();
            var sql = @"Select Id, Amount, CategoryId, ExpenseType, TransactionDate
                        From Expenses
                        Where Id = @Id";

            return await db.QueryFirstOrDefaultAsync<Expense>(sql, new { Id = id });
        }


        public async Task<bool> InsertExpenseDetails(Expense expense)
        {
            var db = dbConnection();
            var sql = @"Insert Into Expenses (Amount, CategoryId, ExpenseType, TransactionDate)
                        Values (@Amount, @CategoryId, @ExpenseType, @TransactionDate) ";

            var result = await db.ExecuteAsync(sql,
                new { expense.Amount, expense.CategoryId, expense.ExpenseType, expense.TransactionDate });

            return result > 0;
        }

        public async Task<bool> UpdateExpense(Expense expense)
        {
            var db = dbConnection();
            var sql = @" Update Expenses
                        Set Amount = @Amount, CategoryId = @CategoryId, ExpenseType = @ExpenseType, 
                        TransactionDate = @TransactionDate
                        Where Id = @Id";

            var result = await db.ExecuteAsync(sql,
                new
                {
                    expense.Id,
                    expense.Amount,
                    expense.CategoryId,
                    expense.ExpenseType
                    ,
                    expense.TransactionDate
                });
            return result > 0;
        }
    }
}
