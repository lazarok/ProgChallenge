using AutoMapper;
using ProgChallenge.Application.DTO.TodoList;
using ProgChallenge.Application.Features.TodoLists.Commands.CreateTodoList;
using ProgChallenge.Application.Features.TodoLists.Queries.GetAllTodoItemsById;
using ProgChallenge.Application.Features.TodoLists.Queries.GetAllTodoLists;
using ProgChallenge.Domain.Entities;

namespace FleetTracking.Application.Mappings
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            CreateMap<TodoList, TodoListAllDto>().ReverseMap();
            CreateMap<TodoList, TodoListDto>().ReverseMap();
            CreateMap<CreateTodoListCommand, TodoList>();
            CreateMap<GetAllTodoListsQuery, GetAllTodoListsParameter>();
            CreateMap<GetAllTodoItemsByIdQuery, GetAllTodoItemsByIdParameter>();
        }
    }
}
