using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatanaServer.Models
{
    [Table("groupsnodes")]
    public class Groups
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
    }
}
