﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared
{
    public class Rate
    {
        public int Id { get; set; }
        public int ParamedicId { get; set; }
        public DateTime Date { get; set; }
        public float PayPerHour { get; set; }
    }
}
