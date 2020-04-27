using NodesDLL;
using System;
using System.Collections.Generic;
using Test;

namespace Tester
{

        class Program
        {
            static void Main(string[] args)
            {
                //  Создание нод
                Node node = new NodeCreate().CreateNodes();
                Console.WriteLine(node);

                //Обновление уровней вложенности

                List<Node> refresh = new List<Node>();
                node.RefreshLevels(ref refresh);

                Console.WriteLine(refresh);

                //Просмотр числа узлов на каждом из уровней вложенности

                var layers = node.NodeLayers(new Dictionary<int, int>());

                //  Поиск нод

                var searchNode = node.Find(2);
                Console.WriteLine(searchNode);

                //  Редактирование нод

                Unit unit = new Unit()
                {
                    id = 55
                };
                searchNode.Edit(unit);
                Console.WriteLine(node);

                //Безопасное удаление нод

                var isSuccess1 = node.SafeRemove(4); // не удалит узел, т.к. 4 не пуст
                var isSuccess2 = node.SafeRemove(24); // не удалит узел, т.к. 24 не существует
                var isSuccess3 = node.SafeRemove(18); // удалит, т.к. 18 пуст

                Console.WriteLine(isSuccess1);
                Console.WriteLine(isSuccess2);
                Console.WriteLine(isSuccess3);

                Console.WriteLine(node);

                // Небезопасное удаление нод

                var isSuccess = node.UnsafeRemove(111); // узел не удален, т.к. не существует
                var _isSuccess = node.UnsafeRemove(55); // узел удален со всеми дочерними узлами

                Console.WriteLine(isSuccess);
                Console.WriteLine(_isSuccess);

                Console.WriteLine(node);

                //Обход узлов в ноде (возврат списка id узлов)

                List<int?> collection = new List<int?>();
                node.MoveNode(ref collection); // обход в корневом узле

                Console.WriteLine(collection);

                collection.Clear();

                searchNode = node.Find(15);
                searchNode.MoveNode(ref collection); // обход в одном из дочерних узлов

                Console.WriteLine(collection);

                // Обход узлов в ноде (возврат списка узлов)

                List<Node> _collection = new List<Node>();

                node.MoveNode(ref _collection);
                Console.WriteLine(_collection);

                _collection.Clear();

                searchNode.MoveNode(ref _collection);
                Console.WriteLine(_collection);

                Console.WriteLine(node);

                // Небезопасное удаление узлов с возвратом id удаленных узлов

                searchNode = node.Find(1);

                var _collection1 = searchNode.UnsafeRemoveWithList(40); // узел не найден
                var _collection2 = searchNode.UnsafeRemoveWithList(4); // узел успешно удален из 1 вместе с 11 и 12

                Console.WriteLine(_collection1);
                Console.WriteLine(_collection2);

                Console.WriteLine(node);
            }
        }
    
}
