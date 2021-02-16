using AutoMapper;
using MediatR;
using ProgChallenge.Application.DTO.TodoList;
using ProgChallenge.Application.Interfaces.Repositories;
using ProgChallenge.Application.Wrappers;
using ProgChallenge.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ProgChallenge.Application.Features.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommand : IRequest<Response<TodoListDto>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, Response<TodoListDto>>
    {
        private readonly ITodoListRepositoryAsync _todoListRepository;
        private readonly IMapper _mapper;
        public CreateTodoListCommandHandler(ITodoListRepositoryAsync todoListRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }

        public async Task<Response<TodoListDto>> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var todoList = _mapper.Map<TodoList>(request);
            await _todoListRepository.AddAsync(todoList);
            var todoListDto = _mapper.Map<TodoListDto>(todoList);
            return new Response<TodoListDto>(todoListDto);
        }
    }
}
