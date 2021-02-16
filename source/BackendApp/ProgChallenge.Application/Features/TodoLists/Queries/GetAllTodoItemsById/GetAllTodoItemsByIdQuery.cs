using AutoMapper;
using MediatR;
using ProgChallenge.Application.DTO.TodoItem;
using ProgChallenge.Application.Exceptions;
using ProgChallenge.Application.Interfaces.Repositories;
using ProgChallenge.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgChallenge.Application.Features.TodoLists.Queries.GetAllTodoItemsById
{
    public class GetAllTodoItemsByIdQuery : IRequest<PagedResponse<IEnumerable<TodoItemAllDto>>>
    {
        public int Id { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllTodoItemsByIdQueryHandler : IRequestHandler<GetAllTodoItemsByIdQuery, PagedResponse<IEnumerable<TodoItemAllDto>>>
    {
        private readonly ITodoListRepositoryAsync _todoListRepository;
        private readonly IMapper _mapper;
        public GetAllTodoItemsByIdQueryHandler(ITodoListRepositoryAsync todoListRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<TodoItemAllDto>>> Handle(GetAllTodoItemsByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllTodoItemsByIdParameter>(request);

            var todoList = await _todoListRepository.GetByIdAsync(validFilter.Id);

            if (todoList == null)
                throw new ApiException($"TodoList Not Found.");

            var todoItems = await _todoListRepository.GetAllTodoItemsAsync(validFilter.Id, validFilter.PageNumber, validFilter.PageSize);
            var todoItemAllDto = _mapper.Map<IEnumerable<TodoItemAllDto>>(todoItems);
            return new PagedResponse<IEnumerable<TodoItemAllDto>>(todoItemAllDto, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
