using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatanaServer
{
    [Table("types")]
    public class Types
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
