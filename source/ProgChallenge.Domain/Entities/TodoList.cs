using ProgChallenge.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgChallenge.Domain.Entities
{
    public class TodoList : AuditableBaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
    }
}
