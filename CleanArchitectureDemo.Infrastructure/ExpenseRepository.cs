using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CleanArchitectureDemo.Domain.Entities;
using CleanArchitectureDemo.Domain.Repositories;
using CleanArchitectureDemo.Infrastructure.Persistence;

namespace CleanArchitectureDemo.Infrastructure
{
    public class ExpenseRepository :IExpenseRepository
    {
        
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext _context)
        {
            this._context = _context;
        }

        public async Task<List<Expense>> GetAllExpensesAsync()
        {
            return await _context.Expenses.ToListAsync();
        }
        public async Task<Expense> AddExpenseAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<Expense> UpdateExpenseAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
            return expense;
        }
        public async Task<Expense> DeleteExpenseAsync(int expenseId)
        {
            var expense = await _context.Expenses.FindAsync(expenseId);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
            return expense;
        }
    }
}