using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgChallenge.Application.DTO.TodoItem;
using ProgChallenge.Application.Features.TodoItems.Commands.CreateTodoItem;
using ProgChallenge.Application.Features.TodoItems.Commands.DeleteTodoItemById;
using ProgChallenge.Application.Features.TodoItems.Commands.UpdateTodoItem;
using ProgChallenge.Application.Features.TodoItems.Queries;
using ProgChallenge.Application.Features.TodoItems.Queries.GetAllTodoItems;
using ProgChallenge.Application.Features.TodoItems.Queries.GetTodoItemById;
using ProgChallenge.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgChallenge.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TodoItemController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllTodoItemsParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllTodoItemsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetTodoItemByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateTodoItemCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateTodoItemCommand command)
        {
            return Ok(await Mediator.Send(new UpdateTodoItemByIdCommand(command, id)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteTodoItemByIdCommand { Id = id }));
        }
    }
}
