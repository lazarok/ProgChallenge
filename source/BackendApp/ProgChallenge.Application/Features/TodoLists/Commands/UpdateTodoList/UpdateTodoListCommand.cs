using AutoMapper;
using MediatR;
using ProgChallenge.Application.DTO.TodoList;
using ProgChallenge.Application.Exceptions;
using ProgChallenge.Application.Interfaces.Repositories;
using ProgChallenge.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgChallenge.Application.Features.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommand : IRequest<Response<TodoListDto>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UpdateTodoListByIdCommand : UpdateTodoListCommand
    {
        public int Id { get; set; }

        public UpdateTodoListByIdCommand(UpdateTodoListCommand command, int id)
        {
            Id = id;
            Title = command.Title;
            Description = command.Description;
        }
    }

    public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListByIdCommand, Response<TodoListDto>>
    {
        private readonly ITodoListRepositoryAsync _todoListRepository;
        private readonly IMapper _mapper;

        public UpdateTodoListCommandHandler(ITodoListRepositoryAsync todoListRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }
        public async Task<Response<TodoListDto>> Handle(UpdateTodoListByIdCommand command, CancellationToken cancellationToken)
        {
            var todoList = await _todoListRepository.GetByIdAsync(command.Id);

            if (todoList == null)
                throw new ApiException($"TodoList Not Found.");
            else
            {
                if (command.Title != default)
                    todoList.Title = command.Title;

                if (command.Description != default)
                    todoList.Description = command.Description;

                await _todoListRepository.UpdateAsync(todoList);
                var todoListDto = _mapper.Map<TodoListDto>(todoList);
                return new Response<TodoListDto>(todoListDto);
            }
        }
    }
}
