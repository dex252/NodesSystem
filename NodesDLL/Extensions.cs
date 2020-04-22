namespace NodesDLL
{
    public static class Extensions
    {
        /// <summary>
        /// Рекурсивный поиск по нодам
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static INode Find(this INode node, int? nodeId)
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
