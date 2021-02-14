using ProgChallenge.Application.Exceptions;
using ProgChallenge.Application.Interfaces.Repositories;
using ProgChallenge.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProgChallenge.Domain.Enums;

namespace ProgChallenge.Application.Features.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommand : IRequest<Response<int>>
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public PriorityLevel Priority { get; set; }
        public DateTime? Scheduled { get; set; }
        public bool Done { get; set; }
    }

    public class UpdateTodoItemByIdCommand : UpdateTodoItemCommand
    {
        public int Id { get; set; }

        public UpdateTodoItemByIdCommand(UpdateTodoItemCommand command, int id)
        {
            Id = id;
            Title = command.Title;
            Priority = command.Priority;
            Scheduled = command.Scheduled;
            Done = command.Done;
        }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemByIdCommand, Response<int>>
    {
        private readonly ITodoItemRepositoryAsync _todoItemRepository;
        public UpdateTodoItemCommandHandler(ITodoItemRepositoryAsync todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }
        public async Task<Response<int>> Handle(UpdateTodoItemByIdCommand command, CancellationToken cancellationToken)
        {
            var todoItem = await _todoItemRepository.GetByIdAsync(command.Id);

            if (todoItem == null)
                throw new ApiException($"TodoItem Not Found.");
            else
            {
                if (command.Title != default)
                    todoItem.Title = command.Title;

                if (command.Note != default)
                    todoItem.Note = command.Note;

                if (command.Scheduled != default)
                    todoItem.Scheduled = command.Scheduled;

                await _todoItemRepository.UpdateAsync(todoItem);
                return new Response<int>(todoItem.Id);
            }
        }
    }
}
