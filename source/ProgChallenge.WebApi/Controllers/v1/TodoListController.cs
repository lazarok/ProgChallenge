using Microsoft.AspNetCore.Mvc;
using ProgChallenge.Application.Features.TodoLists.Commands.CreateTodoList;
using ProgChallenge.Application.Features.TodoLists.Commands.DeleteTodoListById;
using ProgChallenge.Application.Features.TodoLists.Commands.UpdateTodoList;
using ProgChallenge.Application.Features.TodoLists.Queries.GetAllTodoLists;
using ProgChallenge.Application.Features.TodoLists.Queries.GetTodoListById;
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

        [HttpPost]
        public async Task<IActionResult> Post(CreateTodoListCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateTodoListCommand command)
        {
            return Ok(await Mediator.Send(new UpdateTodoListByIdCommand(command, id)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteTodoListByIdCommand { Id = id }));
        }
    }
}
