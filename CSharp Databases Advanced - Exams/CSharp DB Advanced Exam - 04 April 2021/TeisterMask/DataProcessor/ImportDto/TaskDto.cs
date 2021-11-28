using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using TeisterMask.Common;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Task")]
    public class TaskDto
    {
        [MinLength(GlobalConstants.TASK_NAME_MIN_LENGTH)]
        [MaxLength(GlobalConstants.TASK_NAME_MAX_LENGTH)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [Range(GlobalConstants.TASK_EXEC_TYPE_RANGE_FROM, GlobalConstants.TASK_EXEC_TYPE_RANGE_TO)]
        [XmlElement("ExecutionType")]
        public int ExecutionType { get; set; }

        [Range(GlobalConstants.TASK_LABEL_TYPE_RANGE_FROM, GlobalConstants.TASK_LABEL_TYPE_RANGE_TO)]
        [XmlElement("LabelType")]
        public int LabelType { get; set; }
    }
}
