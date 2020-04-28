using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatanaServer
{
    [Table("Bonds")]
    public class Node
    {
        [Key]
        public int? id { get; set; }
        [DisplayName("Содержимое ноды")]
        public Unit Unit { get; set; }
        [DisplayName("id ноды")]
        public int? nodeId { get; set; }
        [DisplayName("id родителя")]
        public int? parentId { get; set; }

        [DisplayName("Уровень вложенности узла"), NonSerialized]
        public int level { get; set; } = 0;
        [DisplayName("Дочерние ноды")]
        public List<Node> nodes { get; set; } = new List<Node>();

        public int? groupId { get; set; }

        /// <summary>
        /// Обход всех узлов в ноде и возврат их списка.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public void MoveNode(ref List<Node> collection)
        {
            if (collection == null) collection = new List<Node>();
            collection.Add(this);
            foreach (var n in this.nodes)
            {
                n.MoveNode(ref collection);
            }
        }

        /// <summary>
        /// Редактировать Unit, присвоить node новый nodeId, всем дочерним нодам новый parentId.
        /// Осторожно! Нужно убедиться, что редактируемый id не содержится в коллекции нод.
        /// </summary>
        /// <param name="unit"></param>
        public void Edit(Unit unit)
        {
            nodeId = unit.id;
            Unit = unit;

            foreach (var n in nodes)
            {
                n.parentId = nodeId;
            }
        }
    }
    [AttributeUsage(System.AttributeTargets.Property, Inherited = false)]
    [System.Runtime.InteropServices.ComVisible(true)]
    public sealed class NonSerializedAttribute : Attribute
    {
    }
}
