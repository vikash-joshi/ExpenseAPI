using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR; // for IRequestHandler<>
using CleanArchitectureDemo.Domain.Entities; // for Expense
using CleanArchitectureDemo.Domain.Repositories; // for IExpenseRepository


namespace CleanArchitectureDemo.Application.Expenses.Commands
{
    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, int>
    {
        private readonly IExpenseRepository _expenseRepository;

        public CreateExpenseCommandHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<int> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = new Expense
            {
                UserId = request.UserId,
                Title = request.Title,
                Category = request.Category,
                Amount = request.Amount,
                ExpenseDate = request.ExpenseDate
            };

            await _expenseRepository.AddExpenseAsync(expense);
            return expense.ExpenseId;
        }

    }
    public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQuery, List<Expense>>
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetAllExpensesQueryHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<List<Expense>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
        {
            return await _expenseRepository.GetAllExpensesAsync();
        }
    }

    public class UpdateCommandHandler:IRequestHandler<UpdateCommand, int>
    {
        private readonly IExpenseRepository _expenseRepository;

        public UpdateCommandHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<int> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var expense = new Expense
            {
                ExpenseId = request.ExpenseId,
                UserId = request.UserId,
                Title = request.Title,
                Category = request.Category,
                Amount = request.Amount
            };

            await _expenseRepository.UpdateExpenseAsync(expense);
            return expense.ExpenseId;
        }
    }

    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, int>
    {
        private readonly IExpenseRepository _expenseRepository;

        public DeleteCommandHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<int> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.DeleteExpenseAsync(request.ExpenseId);
            return expense.ExpenseId;
        }
    }
}