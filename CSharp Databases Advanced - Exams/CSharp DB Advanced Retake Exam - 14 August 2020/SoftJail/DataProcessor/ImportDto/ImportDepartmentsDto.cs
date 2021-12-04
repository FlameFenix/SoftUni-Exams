using SoftJail.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportDepartmentsDto
    {
        [MinLength(GlobalConstants.DEPARTMENT_NAME_MIN_LENGTH)]
        [MaxLength(GlobalConstants.DEPARTMENT_NAME_MAX_LENGTH)]
        public string Name { get; set; }

        public ImportCellsDto[] Cells { get; set; }
    }
}
