using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SatanaDLL
{
    public class Node
    {
        [DisplayName("Содержимое ноды"), NonSerialized]
        public Unit Unit { get; set; }
        [DisplayName("id ноды")]
        public int? nodeId { get; set; }
        [DisplayName("id родителя")]
        public int? parentId { get; set; }
        [DisplayName("Дочерние ноды"), NonSerialized]
        public List<Node> nodes { get; set; } = new List<Node>();

        /// <summary>
        /// Добавить ноду в список (вернет false, если такая нода с таким id уже существует в коллекции этой ноды)
        /// </summary>
        /// <param name="node"></param>
        public bool Add(Node node)
        {
            var item = node.nodes.FirstOrDefault(e => e.nodeId == node.nodeId);

            if (item != null) return false;

            nodes.Add(node);
            return true;
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

        /// <summary>
        /// Безопасное удаление узла из коллекции нод.
        /// Null - элемент с указанным id отсутсвует в коллекции. 
        /// False - элемент не удален, т.к. он содержит дочерние узлы. 
        /// True - элемент удален
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public bool? SafeRemove(int? nodeId)
        {
            var item = this.Find(nodeId);

            if (item != null)
            {
                if (item.nodes == null || item.nodes.Count == 0)
                {
                    var parent = this.Find(item.parentId);
                    parent.nodes.Remove(item);
                    return true;
                } 

                return false;
            }
            
            return null;
        }

        /// <summary>
        /// Небезопасное удаление узла из коллекции нод (узел будет удален вместе с дочерними узлами). 
        /// False - узел не найден.
        /// True - узел удален.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public bool UnsafeRemove(int? nodeId)
        {
            var item = this.Find(nodeId);

            if (item != null)
            {
                var parent = this.Find(item.parentId);
                parent.nodes.Remove(item);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Небезопасное удаление узла из коллекции нод (узел будет удален вместе с дочерними узлами).
        /// Null - узел не найден.
        /// List<int?> - список id всех удаленных узлов, включая удаляемый
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public List<int?> UnsafeRemoveWithList(int? nodeId)
        {
            var item = this.Find(nodeId);

            if (item != null)
            {
                List<int?> collection = new List<int?>(); ;
                item.MoveNode(ref collection);
                var parent = this.Find(item.parentId);
                parent.nodes.Remove(item);
                return collection;
            }

            return null;
        }

        /// <summary>
        /// Обход всех узлов в ноде и возврат списка их id
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public void MoveNode(ref List<int?> collection)
        {
            if (collection == null) collection = new List<int?>();
            collection.Add(this.nodeId);
            foreach (var n in this.nodes)
            {
                n.MoveNode(ref collection);
            }
        }
        /// <summary>
        /// Обход всех узлов в ноде и возврат их списка
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
    }


    /// <summary>
    /// Атрибут с запретом на сериализацию полей
    /// </summary>
    [AttributeUsage(System.AttributeTargets.Property, Inherited = false)]
    [System.Runtime.InteropServices.ComVisible(true)]
    public sealed class NonSerializedAttribute : Attribute
    {
    }
}
