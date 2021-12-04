using SoftJail.Common;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportPrisonerDto
    {
        [MinLength(GlobalConstants.PRISONER_FULL_NAME_MIN_LENGTH)]
        [MaxLength(GlobalConstants.PRISONER_FULLNAME_MAX_LENGTH)]
        [Required]
        public string FullName { get; set; }

        [RegularExpression(GlobalConstants.PRISONER_NICKNAME_REGEX)]
        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Age { get; set; }

        public string ReleaseDate { get; set; }

        [Required]
        public string IncarcerationDate { get; set; }
        public string Bail { get; set; }

        public string CellId { get; set; }

        public ImportMailsDto[] Mails { get; set; }

        //"FullName": "",
        //"Nickname": "The Wallaby",
        //"Age": 32,
        //"IncarcerationDate": "29/03/1957",
        //"ReleaseDate": "27/03/2006",
        //"Bail": null,
        //"CellId": 5,
    }
}
