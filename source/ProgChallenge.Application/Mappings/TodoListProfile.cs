using AutoMapper;
using ProgChallenge.Application.DTO.TodoList;
using ProgChallenge.Application.Features.TodoList.Queries.GetAllTodoLists;
using ProgChallenge.Domain.Entities;

namespace FleetTracking.Application.Mappings
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            CreateMap<TodoList, TodoListAllDto>().ReverseMap();
            CreateMap<TodoItem, TodoListDto>().ReverseMap();
            //CreateMap<CreateTodoListCommand, TodoList>();
            CreateMap<GetAllTodoListsQuery, GetAllTodoListsParameter>();
        }
    }
}
