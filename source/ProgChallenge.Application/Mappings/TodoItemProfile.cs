using AutoMapper;
using ProgChallenge.Application.DTO.TodoItem;
using ProgChallenge.Application.Features.TodoItems.Commands.CreateTodoItem;
using ProgChallenge.Application.Features.TodoItems.Queries;
using ProgChallenge.Application.Features.TodoItems.Queries.GetAllTodoItems;
using ProgChallenge.Domain.Entities;

namespace FleetTracking.Application.Mappings
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, TodoItemAllDto>().ReverseMap();
            CreateMap<TodoItem, TodoItemDto>().ReverseMap();
            CreateMap<CreateTodoItemCommand, TodoItem>();
            CreateMap<GetAllTodoItemsQuery, GetAllTodoItemsParameter>();
        }
    }
}
