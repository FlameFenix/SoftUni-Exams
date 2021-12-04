using SoftJail.Common;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportMailsDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        [RegularExpression(GlobalConstants.MAIL_ADDRESS_REGEX)]
        public string Address { get; set; }
    }
}
