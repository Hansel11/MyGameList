using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Domain.PsdDB
{
    [Table("MsGame")]
    public class MsGame
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        [Column("UserId")]
        public Guid UserId { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Rating")]
        public decimal Rating { get; set; }
        [Column("Genre")]
        public string Genre { get; set; }
    }
}
