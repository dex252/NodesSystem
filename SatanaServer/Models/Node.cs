using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatanaServer
{
    [Table("Bonds")]
    public sealed class Node
    {
        [Key]
        public int key { get; set; }
        public int? id { get; set; }
        [DisplayName("Содержимое ноды"), NonSerialized]
        public Unit Unit { get; set; }
        [DisplayName("id ноды")]
        public int? nodeId { get; set; }
        [DisplayName("id родителя")]
        public int? parentId { get; set; }

        [DisplayName("Уровень вложенности узла"), NonSerialized]
        public int level { get; set; } = 0;
        [DisplayName("Дочерние ноды"), NonSerialized]
        public List<Node> nodes { get; set; } = new List<Node>();
    }
    [AttributeUsage(System.AttributeTargets.Property, Inherited = false)]
    [System.Runtime.InteropServices.ComVisible(true)]
    public sealed class NonSerializedAttribute : Attribute
    {
    }
}
