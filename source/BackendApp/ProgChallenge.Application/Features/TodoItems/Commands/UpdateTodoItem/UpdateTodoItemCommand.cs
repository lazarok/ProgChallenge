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
using ProgChallenge.Application.DTO.TodoItem;
using AutoMapper;

namespace ProgChallenge.Application.Features.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommand : IRequest<Response<TodoItemDto>>
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public PriorityLevel Priority { get; set; }
        public DateTime? Scheduled { get; set; }
        public bool? Done { get; set; }
    }

    public class UpdateTodoItemByIdCommand : UpdateTodoItemCommand
    {
        public int Id { get; set; }

        public UpdateTodoItemByIdCommand(UpdateTodoItemCommand command, int id)
        {
            Id = id;
            Title = command.Title;
            Note = command.Note;
            Priority = command.Priority;
            Scheduled = command.Scheduled;
            Done = command.Done;
        }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemByIdCommand, Response<TodoItemDto>>
    {
        private readonly ITodoItemRepositoryAsync _todoItemRepository;
        private readonly IMapper _mapper;

        public UpdateTodoItemCommandHandler(ITodoItemRepositoryAsync todoItemRepository, IMapper mapper)
        {
            _todoItemRepository = todoItemRepository;
            _mapper = mapper;
    }
        public async Task<Response<TodoItemDto>> Handle(UpdateTodoItemByIdCommand command, CancellationToken cancellationToken)
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

                if (command.Done != null)
                    todoItem.Done = command.Done.Value;

                await _todoItemRepository.UpdateAsync(todoItem);
                var todoItemDto = _mapper.Map<TodoItemDto>(todoItem);
                return new Response<TodoItemDto>(todoItemDto);
            }
        }
    }
}
