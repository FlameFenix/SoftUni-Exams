using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TeisterMask.Common;
using TeisterMask.DataProcessor.ImportDto;

namespace TeisterMask.ImportDtos
{
    [XmlType("Project")]
    public class ProjectDto
    {
        [MinLength(GlobalConstants.PROJECT_NAME_MIN_LENGTH)]
        [MaxLength(GlobalConstants.PROJECT_NAME_MAX_LENGTH)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [XmlArray("Tasks")]
        public TaskDto[] Tasks { get; set; }
    }
}
