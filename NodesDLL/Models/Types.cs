using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NodesDLL
{
    [Table("types")]
    public class Groups
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
