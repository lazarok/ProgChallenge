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

namespace ProgChallenge.Application.Features.TodoLists.Queries.GetTodoListById
{
    public class GetTodoListByIdQuery : IRequest<Response<TodoListDto>>
    {
        public int Id { get; set; }
    }

    public class GetTodoListByIdQueryHandler : IRequestHandler<GetTodoListByIdQuery, Response<TodoListDto>>
    {
        private readonly ITodoListRepositoryAsync _todoListRepository;
        private readonly IMapper _mapper;

        public GetTodoListByIdQueryHandler(ITodoListRepositoryAsync todoListRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }
        public async Task<Response<TodoListDto>> Handle(GetTodoListByIdQuery query, CancellationToken cancellationToken)
        {
            var todoList = await _todoListRepository.GetByIdAsync(query.Id);

            if (todoList == null)
                throw new ApiException($"TodoList Not Found.");

            var todoListDto = _mapper.Map<TodoListDto>(todoList);
            return new Response<TodoListDto>(todoListDto);
        }
    }
}
