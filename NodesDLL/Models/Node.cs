using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace NodesDLL
{
    [Table("Bonds")]
    public sealed class Node: INode
    {
        [Key]
        public int id { get; set; }
        [DisplayName("Содержимое ноды")]
        public Unit Unit { get; set; }
        [DisplayName("id ноды")]
        public int? nodeId { get; set; }
        [DisplayName("id родителя")]
        public int? parentId { get; set; }

        [DisplayName("Уровень вложенности узла")]
        public int level { get; set; } = 0;
        [DisplayName("Дочерние ноды"), NonSerialized]
        public List<INode> nodes { get; set; } = new List<INode>();

        /// <summary>
        /// Добавить ноду в список (вернет false, если такая нода с таким id уже существует в коллекции этой ноды).
        /// Корректно заполняет уровни вложенности только при одиночном добавлении узлов, в остальных случаях воспользуйтесь методом RefreshLevels.
        /// </summary>
        /// <param name="node"></param>
        public bool Add(Node node)
        {
            var item = nodes.FirstOrDefault(e => e.nodeId == node.nodeId);

            if (item != null) return false;

            node.level = this.level + 1;
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
        /// List<int?> - список id всех удаленных узлов, включая удаляемый.
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
        /// Обход всех узлов в ноде и возврат списка их id.
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
        /// Обход всех узлов в ноде и возврат их списка с обновлением уровней вложенности.
        /// Уровень вложенности для первого узла указывается вторым параметром
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="count">начальный уровень вложенности</param>
        /// <returns></returns>
        public void RefreshLevels(ref List<Node> collection, int count = 0)
        {
            if (collection == null) collection = new List<Node>();
            this.level = count;
            count++;
            collection.Add(this);
            foreach (var n in this.nodes)
            {
                n.RefreshLevels(ref collection, count);
            }
        }

        /// <summary>
        /// Вывод числа узлов на каждом из уровней вложнности в соответствии с их параметром level.
        /// </summary>
        /// <param name="layers"></param>
        /// <returns></returns>
        public Dictionary<int, int> NodeLayers(Dictionary<int, int> layers)
        {
            if (!layers.ContainsKey(this.level))
            {
                layers.Add(this.level, 1);
            }
            else
            {
                layers[this.level] += 1;
            }

            foreach (var n in this.nodes)
            {
                layers = n.NodeLayers(layers);
            }

            return layers;
        }
    }
}
