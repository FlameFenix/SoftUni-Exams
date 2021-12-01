using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Export
{
    public class ExportGameByGenresDto
    {

        public string Id { get; set; }

        public string Genre { get; set; }

        public ExportGameDto[] Games { get; set; }
        //        "Id": 4,
        //"Genre": "Violent",
        //"Games": [
        //  {

        //  },

    }
}
