using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VaporStore.Data.Models
{
    public class GameTag
    {
        [ForeignKey(nameof(Game))]
        [Required]
        public int GameId { get; set; }

        public Game Game { get; set; }

        [ForeignKey(nameof(Tag))]
        [Required]
        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
