using BlazorExpenseTracker.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace BlazorExpenseTracker.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private SqlConfiguration _connectionString;

        public CategoryRepository(SqlConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection DBConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var db = DBConnection();
            var sql = @"Delete From Categories where Id = @Id";

            int result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var db = DBConnection();
            var sql = @"Select Id, Name From Categories";

            return await db.QueryAsync<Category>(sql, new { });
        }

        public async Task<Category> GetCategoryDetails(int id)
        {
            var db = DBConnection();
            var sql = @"Select Id, Name From Categories Where Id = @Id";

            return await db.QueryFirstOrDefaultAsync<Category>(sql, new { Id=id });
        }

        public async Task<bool> InsertCategory(Category category)
        {
            var db = DBConnection();
            var sql = @"Insert Into Categories (Name) Values (@Name)";

            int result = await db.ExecuteAsync(sql, new { category.Name }); //Como "Name" esta en mayuscula, no necesito especificar Name=category.Name
            return result > 0;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            var db = DBConnection();
            var sql = @"Update Categories Set Name = @Name Where Id = @Id";

            int result = await db.ExecuteAsync(sql, new { category.Name, category.Id });
            return result > 0;
        }
    }
}