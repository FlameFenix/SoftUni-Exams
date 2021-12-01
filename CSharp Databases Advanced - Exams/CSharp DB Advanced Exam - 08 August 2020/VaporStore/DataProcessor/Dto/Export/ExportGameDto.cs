using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Export
{
    public class ExportGameDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Developer { get; set; }

        public string Tags { get; set; }

        public string Players { get; set; }

        //    "Id": 49,
        //    "Title": "Warframe",
        //    "Developer": "Digital Extremes",
        //    "Tags": "Single-player, In-App Purchases, Steam Trading Cards, Co-op, Multi-player, Partial Controller Support",
        //    "Players": 6
    }
}
