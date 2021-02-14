using ProgChallenge.Application.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgChallenge.Application.Features.TodoLists.Queries.GetAllTodoItemsById
{
    public class GetAllTodoItemsParameter: RequestParameter
    {

    }

    public class GetAllTodoItemsByIdParameter : GetAllTodoItemsParameter
    {
        public int Id { get; set; }
    }
}
