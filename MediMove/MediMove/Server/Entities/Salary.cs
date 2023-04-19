﻿using MediMove.Shared.Entities;

namespace MediMove.Server.Entities
{
    public class Salary
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public decimal Income { get; set; }


        public int DispatcherId { get; set; }
        public Dispatcher Dispatcher { get; set; }


    }
}
