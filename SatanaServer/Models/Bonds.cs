using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatanaServer.Models
{
    [Table("bonds")]
    class Bonds
    {
        [Key]
        public int? id { get; set; }
        public int? nodeId { get; set; }
        public int? parentId { get; set; }
        public int groupId { get; set; }
    }
}
