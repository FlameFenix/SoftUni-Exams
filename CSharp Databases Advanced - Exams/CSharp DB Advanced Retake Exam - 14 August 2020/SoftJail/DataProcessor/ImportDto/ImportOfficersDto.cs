using SoftJail.Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class ImportOfficersDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(GlobalConstants.OFFICER_FULLNAME_MIN_LENGTH)]
        [MaxLength(GlobalConstants.OFFICER_FULLNAME_MAX_LENGTH)]
        public string Name { get; set; }
        
        [XmlElement("Money")]
        [Required]
        public string Money { get; set; }

        [XmlElement("Position")]
        [Required]
        public string Position { get; set; }

        [XmlElement("Weapon")]
        [Required]
        public string Weapon { get; set; }

        [XmlElement("DepartmentId")]
        [Required]
        public string DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public PrisonersIdsImportDto[] PrisonersId { get; set; }

    }
}
