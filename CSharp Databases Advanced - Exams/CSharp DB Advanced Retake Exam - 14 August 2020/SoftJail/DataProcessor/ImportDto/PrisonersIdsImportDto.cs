using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Prisoner")]
    public class PrisonersIdsImportDto
    {
        [XmlAttribute("id")]
        [Required]
        public string Id { get; set; }
    }
}
