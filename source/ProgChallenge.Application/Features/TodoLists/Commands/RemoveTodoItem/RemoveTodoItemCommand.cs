﻿using AutoMapper;
using MediatR;
using ProgChallenge.Application.Interfaces.Repositories;
using ProgChallenge.Application.Wrappers;
using ProgChallenge.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgChallenge.Application.Features.TodoLists.Commands.RemoveTodoItem
{
    public class RemoveTodoItemCommand : IRequest<Response<bool>>
    {
        public int TodoListId { get; set; }
        public int TodoItemId { get; set; }
    }

    public class RemoveTodoItemCommandHandler : IRequestHandler<RemoveTodoItemCommand, Response<bool>>
    {
        private readonly ITodoListRepositoryAsync _todoListRepository;
        private readonly ITodoItemRepositoryAsync _todoItemRepository;
        private readonly IMapper _mapper;
        public RemoveTodoItemCommandHandler(ITodoListRepositoryAsync todoListRepository, ITodoItemRepositoryAsync todoItemRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _todoItemRepository = todoItemRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(RemoveTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoListId = request.TodoListId;
            var todoItemId = request.TodoItemId;

            var todoList = _todoListRepository.GetByIdAsync(todoListId);
            if (todoList == default)
                throw new ApiException($"TodoList Not Found.");

            var todoItem = _todoItemRepository.GetByIdAsync(todoItemId);
            if (todoItem == default)
                throw new ApiException($"TodoItem Not Found.");

            var exists = await _todoListRepository.ExistListItemAsync(todoListId, todoItemId);
            if (!exists) throw new ApiException($"The TodoItem '{todoItemId}' is not already associated with the TodoList '{todoListId}'");

            var success = await _todoListRepository.RemoveTodoItemAsync(todoListId, todoItemId);
            return new Response<bool>(success);
        }
    }
}
