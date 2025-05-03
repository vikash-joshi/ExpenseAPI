using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureDemo.Domain.Entities;

namespace CleanArchitectureDemo.Domain.Repositories
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> GetAllExpensesAsync();
        Task<Expense> AddExpenseAsync(Expense expense);
        Task<Expense> UpdateExpenseAsync(Expense expense);
        Task<Expense> DeleteExpenseAsync(int expenseId);
    }
}