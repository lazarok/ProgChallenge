using ProgChallenge.Application.Exceptions;
using ProgChallenge.Application.Interfaces.Repositories;
using ProgChallenge.Application.Wrappers;
using ProgChallenge.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ProgChallenge.Application.DTO.TodoItem;

namespace ProgChallenge.Application.Features.TodoItems.Queries.GetTodoItemById
{
    public class GetTodoItemByIdQuery : IRequest<Response<TodoItemDto>>
    {
        public int Id { get; set; }
    }

    public class GetTodoItemByIdQueryHandler : IRequestHandler<GetTodoItemByIdQuery, Response<TodoItemDto>>
    {
        private readonly ITodoItemRepositoryAsync _todoItemRepository;
        private readonly IMapper _mapper;

        public GetTodoItemByIdQueryHandler(ITodoItemRepositoryAsync todoItemRepository, IMapper mapper)
        {
            _todoItemRepository = todoItemRepository;
            _mapper = mapper;
        }
        public async Task<Response<TodoItemDto>> Handle(GetTodoItemByIdQuery query, CancellationToken cancellationToken)
        {
            var todoItem = await _todoItemRepository.GetByIdAsync(query.Id);

            if (todoItem == null)
                throw new ApiException($"TodoItem Not Found.");

            var todoItemDto = _mapper.Map<TodoItemDto>(todoItem);
            return new Response<TodoItemDto>(todoItemDto);
        }
    }
}
