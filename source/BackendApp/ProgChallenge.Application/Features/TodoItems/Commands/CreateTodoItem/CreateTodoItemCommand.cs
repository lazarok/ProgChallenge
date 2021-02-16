using AutoMapper;
using ProgChallenge.Application.Interfaces.Repositories;
using ProgChallenge.Application.Wrappers;
using ProgChallenge.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using ProgChallenge.Domain.Enums;
using ProgChallenge.Application.DTO.TodoItem;

namespace ProgChallenge.Application.Features.TodoItems.Commands.CreateTodoItem
{
    public partial class CreateTodoItemCommand : IRequest<Response<TodoItemDto>>
    {
        public int TodoListId { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public PriorityLevel Priority { get; set; }
        public DateTime? Scheduled { get; set; }
        public bool Done { get; set; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Response<TodoItemDto>>
    {
        private readonly ITodoItemRepositoryAsync _todoItemRepository;
        private readonly IMapper _mapper;
        public CreateTodoItemCommandHandler(ITodoItemRepositoryAsync todoItemRepository, IMapper mapper)
        {
            _todoItemRepository = todoItemRepository;
            _mapper = mapper;
        }

        public async Task<Response<TodoItemDto>> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = _mapper.Map<TodoItem>(request);
            await _todoItemRepository.AddAsync(todoItem, request.TodoListId);
            var todoItemDto = _mapper.Map<TodoItemDto>(todoItem);
            return new Response<TodoItemDto>(todoItemDto);            
        }
    }
}
