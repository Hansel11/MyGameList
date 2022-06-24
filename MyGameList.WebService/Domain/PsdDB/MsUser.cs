using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Domain.PsdDB
{
    [Table("MsUser")]
    public class MsUser
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        [Column("Username")]
        public string Username { get; set; }
        [Column("Password")]
        public string Password { get; set; }
    }
}
