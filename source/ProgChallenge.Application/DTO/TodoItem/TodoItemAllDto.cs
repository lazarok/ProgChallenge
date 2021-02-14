﻿using ProgChallenge.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgChallenge.Application.DTO.TodoItem
{
    public class TodoItemAllDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public PriorityLevel Priority { get; set; }

        public DateTime? Scheduled { get; set; }

        public bool Done { get; set; }
    }
}
