using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VaporStore.Common;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportUserDto
	{
        [RegularExpression(GlobalConstants.FULLNAME_REGEX)]
        public string FullName { get; set; }

        [MinLength(GlobalConstants.USERNAME_MINLENGTH)]
        public string Username { get; set; }

        public string Email { get; set; }
        public string Age { get; set; }

        public ImportCardDto[] Cards { get; set; }
    }
}
