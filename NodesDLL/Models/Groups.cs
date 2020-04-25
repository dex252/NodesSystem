using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NodesDLL.Models
{
    [Table("groupsnodes")]
    public class Groups
    {
        [Key]
        public int? id { get; set; }
        [DisplayName("Название дерева"), Required]
        public string name { get; set; }
        public DateTime date { get; set; }
    }
}
