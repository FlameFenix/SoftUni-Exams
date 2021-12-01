using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VaporStore.Common;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportGameDto
	{
        public string Name { get; set; }
        public string Price { get; set; }

        public string ReleaseDate { get; set; }

        public string Developer { get; set; }

        public string Genre { get; set; }

        public string[] Tags { get; set; }
	}
}
