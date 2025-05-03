using MediatR;
using CleanArchitectureDemo.Domain.Entities;
using CleanArchitectureDemo.Domain.Repositories;
namespace CleanArchitectureDemo.Application.Expenses.Commands;


public class CreateExpenseCommand : IRequest<int>
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; }
}

public class GetAllExpensesQuery : IRequest<List<Expense>>
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; }
}
public class UpdateCommand : IRequest<int>
{
    public int ExpenseId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }

}
public class DeleteCommand : IRequest<int>
{
    public int ExpenseId { get; set; }
    public int UserId { get; set; }

}
