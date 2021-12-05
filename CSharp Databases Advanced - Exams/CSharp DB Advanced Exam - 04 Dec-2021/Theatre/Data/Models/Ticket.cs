﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Theatre.Common;

namespace Theatre.Data.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(GlobalConstants.TICKET_ROWNUMBER_MAX_RANGE)]
        public sbyte RowNumber { get; set; }

        [Required]
        [ForeignKey(nameof(Play))]
        public int PlayId { get; set; }

        public Play Play { get; set; }

        [Required]
        [ForeignKey(nameof(Theatre))]
        public int TheatreId { get; set; }

        [Required]
        public Theatre Theatre { get; set; }
    }
}
