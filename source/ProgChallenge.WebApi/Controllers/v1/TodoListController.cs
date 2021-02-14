using Microsoft.AspNetCore.Mvc;
using ProgChallenge.Application.Features.TodoList.Queries.GetAllTodoLists;
using ProgChallenge.Application.Features.TodoList.Queries.GetTodoListById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgChallenge.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TodoListController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllTodoListsParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllTodoListsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetTodoListByIdQuery { Id = id }));
        }
    }
}
