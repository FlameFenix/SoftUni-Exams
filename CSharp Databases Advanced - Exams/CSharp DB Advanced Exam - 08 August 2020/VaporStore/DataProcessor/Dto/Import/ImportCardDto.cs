using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VaporStore.Common;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportCardDto
    {
        [RegularExpression(GlobalConstants.CARD_NUMBER_REGEX)]
        public string Number { get; set; }

        [RegularExpression(GlobalConstants.CARD_CVC_REGER)]
        public string CVC { get; set; }

        public string Type { get; set; }
    }
}
