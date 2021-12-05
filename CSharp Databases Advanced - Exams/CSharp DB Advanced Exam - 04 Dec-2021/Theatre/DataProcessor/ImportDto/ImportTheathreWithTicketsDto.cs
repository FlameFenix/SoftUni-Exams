using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Theatre.Common;

namespace Theatre.DataProcessor.ImportDto
{
    public class ImportTheathreWithTicketsDto
    {
        [Required]
        [MinLength(GlobalConstants.NAME_MIN_LENGTH)]
        [MaxLength(GlobalConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
        [Range(GlobalConstants.NUMBEROFHALLS_MIN_RANGE, GlobalConstants.NUMBEROFHALLS_MAX_RANGE)]
        public sbyte NumberOfHalls { get; set; }

        [Required]
        [MinLength(GlobalConstants.DIRECTOR_MIN_LENGTH)]
        [MaxLength(GlobalConstants.DIRECTOR_MAX_LENGTH)]
        public string Director { get; set; }

        public ImportTicketsDto[] Tickets { get; set; }
    }
}
