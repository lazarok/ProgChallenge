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

namespace ProgChallenge.Application.Features.TodoLists.Commands.DeleteTodoListById
{
    public class DeleteTodoListByIdCommand : IRequest<Response<TodoListDto>>
    {
        public int Id { get; set; }
    }

    public class DeleteTodoListByIdCommandHandler : IRequestHandler<DeleteTodoListByIdCommand, Response<TodoListDto>>
    {
        private readonly ITodoListRepositoryAsync _todoListRepository;
        private readonly IMapper _mapper;
        public DeleteTodoListByIdCommandHandler(ITodoListRepositoryAsync todoListRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }
        public async Task<Response<TodoListDto>> Handle(DeleteTodoListByIdCommand command, CancellationToken cancellationToken)
        {
            var todoList = await _todoListRepository.GetByIdAsync(command.Id);
            if (todoList == null) throw new ApiException($"TodoList Not Found.");
            await _todoListRepository.DeleteAsync(todoList);
            var todoListDto = _mapper.Map<TodoListDto>(todoList);
            return new Response<TodoListDto>(todoListDto);
        }
    }
}
