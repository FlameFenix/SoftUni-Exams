﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Theatre.DataProcessor.ExportDto
{
    public class ExportTheatresDto
    {
        public string Name { get; set; }

        public sbyte Halls { get; set; }

        public decimal TotalIncome { get; set; }

        public ExportTicketsDto[] Tickets { get; set; }
    }
}
