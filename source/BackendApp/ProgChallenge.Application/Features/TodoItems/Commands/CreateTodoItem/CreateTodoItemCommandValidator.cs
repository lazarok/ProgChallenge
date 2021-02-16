using ProgChallenge.Application.Interfaces.Repositories;
using ProgChallenge.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgChallenge.Application.Features.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        private readonly ITodoListRepositoryAsync _todoListRepository;

        public CreateTodoItemCommandValidator(ITodoListRepositoryAsync todoListRepository)
        {
            _todoListRepository = todoListRepository;

            RuleFor(p => p.TodoListId)
               .MustAsync(ExistsTodoList).WithMessage("{PropertyName} not exists.");


            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Note)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        }

        private async Task<bool> ExistsTodoList(int todoListId, CancellationToken cancellationToken)
        {
            var todoList = await _todoListRepository.GetByIdAsync(todoListId);
            return todoList != default;
        }
    }
}
