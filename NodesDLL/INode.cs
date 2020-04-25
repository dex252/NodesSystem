using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NodesDLL
{
    public interface INode
    {
        [Key]
        int id { get; set; }
        [DisplayName("Содержимое ноды")]
        Unit Unit { get; set; }
        [DisplayName("id ноды")]
        int? nodeId { get; set; }
        [DisplayName("id родителя")]
        int? parentId { get; set; }
        [DisplayName("Уровень вложенности узла")]
        int level { get; set; }
        [DisplayName("Дочерние ноды"), NonSerialized]
        List<INode> nodes { get; set; }
        [DisplayName("Группа ноды")]
        int? groupId { get; set; }

        /// <summary>
        /// Добавить ноду в список (вернет false, если такая нода с таким id уже существует в коллекции этой ноды).
        /// Корректно заполняет уровни вложенности только при одиночном добавлении узлов, в остальных случаях воспользуйтесь методом RefreshLevels.
        /// </summary>
        /// <param name="node"></param>
        bool Add(INode node);
        /// <summary>
        /// Редактировать Unit, присвоить node новый nodeId, всем дочерним нодам новый parentId.
        /// Осторожно! Нужно убедиться, что редактируемый id не содержится в коллекции нод.
        /// </summary>
        /// <param name="unit"></param>
        void Edit(Unit unit);
        /// <summary>
        /// Безопасное удаление узла из коллекции нод.
        /// Null - элемент с указанным id отсутсвует в коллекции. 
        /// False - элемент не удален, т.к. он содержит дочерние узлы. 
        /// True - элемент удален
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        bool? SafeRemove(int? nodeId);
        /// <summary>
        /// Небезопасное удаление узла из коллекции нод (узел будет удален вместе с дочерними узлами). 
        /// False - узел не найден.
        /// True - узел удален.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        bool UnsafeRemove(int? nodeId);
        /// <summary>
        /// Небезопасное удаление узла из коллекции нод (узел будет удален вместе с дочерними узлами).
        /// Null - узел не найден.
        /// List<int?> - список id всех удаленных узлов, включая удаляемый.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        List<int?> UnsafeRemoveWithList(int? nodeId);
        /// <summary>
        /// Обход всех узлов в ноде и возврат списка их id.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        void MoveNode(ref List<int?> collection);
        /// <summary>
        /// Обход всех узлов в ноде и возврат их списка.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        void MoveNode(ref List<Node> collection);
        /// <summary>
        /// Обход всех узлов в ноде и возврат их списка с обновлением уровней вложенности.
        /// Уровень вложенности для первого узла указывается вторым параметром
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="count">начальный уровень вложенности</param>
        void RefreshLevels(ref List<Node> collection, int count = 0);
        /// <summary>
        /// Вывод числа узлов на каждом из уровней вложнности в соответствии с их параметром level.
        /// </summary>
        /// <param name="layers"></param>
        /// <returns></returns>
        Dictionary<int, int> NodeLayers(Dictionary<int, int> layers);
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
