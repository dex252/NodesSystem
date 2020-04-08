using System;
using System.Collections.Generic;

namespace SatanaDLL
{
    public static class Extentions
    {
        /// <summary>
        /// Рекурсивный поиск по нодам
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static Node Find(this Node node, int? nodeId)
        {
            if (node.nodeId == nodeId) return node;

            foreach (var n in node.nodes)
            {
                var item = n.Find(nodeId);
                if (item != null) return item;
            }

            return null;
        }

    }
}
