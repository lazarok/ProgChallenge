using ProgChallenge.Application.Exceptions;
using ProgChallenge.Application.Interfaces.Repositories;
using ProgChallenge.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProgChallenge.Application.DTO.TodoItem;
using AutoMapper;

namespace ProgChallenge.Application.Features.TodoItems.Commands.DeleteTodoItemById
{
    public class DeleteTodoItemByIdCommand : IRequest<Response<TodoItemDto>>
    {
        public int Id { get; set; }
        public class DeleteTodoItemByIdCommandHandler : IRequestHandler<DeleteTodoItemByIdCommand, Response<TodoItemDto>>
        {
            private readonly ITodoItemRepositoryAsync _todoItemRepository;
            private readonly IMapper _mapper;
            public DeleteTodoItemByIdCommandHandler(ITodoItemRepositoryAsync todoItemRepository, IMapper mapper)
            {
                _todoItemRepository = todoItemRepository;
                _mapper = mapper;
            }
            public async Task<Response<TodoItemDto>> Handle(DeleteTodoItemByIdCommand command, CancellationToken cancellationToken)
            {
                var todoItem = await _todoItemRepository.GetByIdAsync(command.Id);
                if (todoItem == null) throw new ApiException($"TodoItem Not Found.");
                await _todoItemRepository.DeleteAsync(todoItem);
                var todoItemDto = _mapper.Map<TodoItemDto>(todoItem);
                return new Response<TodoItemDto>(todoItemDto);
            }
        }
    }
}
