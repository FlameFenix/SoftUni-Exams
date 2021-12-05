using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Theatre.Common;

namespace Theatre.DataProcessor.ImportDto
{
    [XmlType("Play")]
    public class ImportPlayDto
    {
        [Required]
        [XmlElement("Title")]
        [MinLength(GlobalConstants.TITLE_MIN_LENGTH)]
        [MaxLength(GlobalConstants.TITLE_MAX_LENGTH)]
        public string Title { get; set; }

        [Required]
        [XmlElement("Duration")]
        public string Duration { get; set; }

        [Required]
        [XmlElement("Rating")]
        public string Rating { get; set; }

        [Required]
        [XmlElement("Genre")]
        public string Genre { get; set; }

        [Required]
        [XmlElement("Description")]
        [MaxLength(GlobalConstants.DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; }

        [Required]
        [XmlElement("Screenwriter")]
        [MinLength(GlobalConstants.SCREENWRITER_MIN_LENGTH)]
        [MaxLength(GlobalConstants.SCREENWRITER_MAX_LENGTH)]
        public string Screenwriter { get; set; }
    }
}
