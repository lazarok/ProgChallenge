using AutoMapper;
using ProgChallenge.Application.Filters;
using ProgChallenge.Application.Interfaces.Repositories;
using ProgChallenge.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProgChallenge.Application.DTO.TodoItem;

namespace ProgChallenge.Application.Features.TodoItems.Queries.GetAllTodoItems
{
    public class GetAllTodoItemsQuery : IRequest<PagedResponse<IEnumerable<TodoItemAllDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllTodoItemsQueryHandler : IRequestHandler<GetAllTodoItemsQuery, PagedResponse<IEnumerable<TodoItemAllDto>>>
    {
        private readonly ITodoItemRepositoryAsync _todoItemRepository;
        private readonly IMapper _mapper;
        public GetAllTodoItemsQueryHandler(ITodoItemRepositoryAsync todoItemRRepository, IMapper mapper)
        {
            _todoItemRepository = todoItemRRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<TodoItemAllDto>>> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllTodoItemsParameter>(request);
            var todoItem = await _todoItemRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var todoItemAllDto = _mapper.Map<IEnumerable<TodoItemAllDto>>(todoItem);
            return new PagedResponse<IEnumerable<TodoItemAllDto>>(todoItemAllDto, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
