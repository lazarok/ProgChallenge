using ProgChallenge.Domain.Common;
using ProgChallenge.Domain.Enums;
using System;

namespace ProgChallenge.Domain.Entities
{
    public class TodoItem : AuditableBaseEntity
    {
        public TodoList List { get; set; }

        public int ListId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public PriorityLevel Priority { get; set; }

        public DateTime? Scheduled { get; set; }

        public bool Done { get; set; }
    }
}
