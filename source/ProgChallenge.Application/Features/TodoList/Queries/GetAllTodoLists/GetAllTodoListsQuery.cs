using AutoMapper;
using MediatR;
using ProgChallenge.Application.DTO.TodoList;
using ProgChallenge.Application.Interfaces.Repositories;
using ProgChallenge.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgChallenge.Application.Features.TodoList.Queries.GetAllTodoLists
{
    public class GetAllTodoListsQuery : IRequest<PagedResponse<IEnumerable<TodoListAllDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllTodoListsQueryHandler : IRequestHandler<GetAllTodoListsQuery, PagedResponse<IEnumerable<TodoListAllDto>>>
    {
        private readonly ITodoListRepositoryAsync _todoListRepository;
        private readonly IMapper _mapper;
        public GetAllTodoListsQueryHandler(ITodoListRepositoryAsync todoListRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<TodoListAllDto>>> Handle(GetAllTodoListsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllTodoListsParameter>(request);
            var todoItem = await _todoListRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var todoItemAllDto = _mapper.Map<IEnumerable<TodoListAllDto>>(todoItem);
            return new PagedResponse<IEnumerable<TodoListAllDto>>(todoItemAllDto, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
