using SatanaDLL;

namespace Test
{
    class NodeCreate
    {
        /// <summary>
        /// Схема связей
        ///                          0
        ///                          |
        ///             -----------------------------------------
        ///             |                   |                   |
        ///             1                   2                   3
        ///             |                   |                   |
        ///         -------------------------------------------------
        ///         |   |   |           |       |           |       |
        ///         4   5   6           7       8           9       10
        ///         |       |                   |                   |
        ///    ---------    |                   |                   |
        ///    |       |    |                   |                   |
        ///    11      12   14                  16                  15
        ///                                     |                   |
        ///                                     18                  17
        ///                                                      --------
        ///                                                      |      |
        ///                                                      19     20
        ///                                                      |
        ///                                                      21
        /// </summary>
        public Node CreateNodes()
        {
            Node node = new Node()
            {
                nodeId = 0,
                parentId = null
            };

            StartCreator00(node);
           
            return node;
        }

        private void StartCreator00(Node node)
        {
            Node node01 = new Node()
            {
                nodeId = 1,
                parentId = node.nodeId
            };
            StartCreator01(node01);
            Node node02 = new Node()
            {
                nodeId = 2,
                parentId = node.nodeId
            };
            StartCreator02(node02);
            Node node03 = new Node()
            {
                nodeId = 3,
                parentId = node.nodeId
            };
            StartCreator03(node03);

            node.Add(node01);
            node.Add(node02);
            node.Add(node03);
        }

        private void StartCreator01(Node node)
        {
            Node node04 = new Node()
            {
                nodeId = 4,
                parentId = node.nodeId
            };
            StartCreator04(node04);
            Node node05 = new Node()
            {
                nodeId = 5,
                parentId = node.nodeId
            };
            Node node06 = new Node()
            {
                nodeId = 6,
                parentId = node.nodeId
            };
            StartCreator06(node06);
            node.Add(node04);
            node.Add(node05);
            node.Add(node06);
        }
        private void StartCreator02(Node node)
        {
            Node node07 = new Node()
            {
                nodeId = 7,
                parentId = node.nodeId
            };
          
            Node node08 = new Node()
            {
                nodeId = 8,
                parentId = node.nodeId
            };
            StartCreator08(node08);
            node.Add(node07);
            node.Add(node08);
        }
        private void StartCreator03(Node node)
        {
            Node node09 = new Node()
            {
                nodeId = 9,
                parentId = node.nodeId
            };

            Node node10 = new Node()
            {
                nodeId = 10,
                parentId = node.nodeId
            };
            StartCreator10(node10);
            node.Add(node09);
            node.Add(node10);
        }
        private void StartCreator04(Node node)
        {
            Node node11 = new Node()
            {
                nodeId = 11,
                parentId = node.nodeId
            };
            Node node12 = new Node()
            {
                nodeId = 12,
                parentId = node.nodeId
            };
            node.Add(node11);
            node.Add(node12);
        }
        private void StartCreator06(Node node)
        {
            Node node14 = new Node()
            {
                nodeId = 14,
                parentId = node.nodeId
            };
            node.Add(node14);
        }
        private void StartCreator08(Node node)
        {
            Node node16 = new Node()
            {
                nodeId = 16,
                parentId = node.nodeId
            };
            StartCreator16(node16);
            node.Add(node16);
        }
        private void StartCreator10(Node node)
        {
            Node node15 = new Node()
            {
                nodeId = 15,
                parentId = node.nodeId
            };

            StartCreator15(node15);
            node.Add(node15);
          
        }
        private void StartCreator15(Node node)
        {
            Node node17 = new Node()
            {
                nodeId = 17,
                parentId = node.nodeId
            };
            StartCreator17(node17);
            node.Add(node17);
        }
        private void StartCreator17(Node node)
        {
            Node node19 = new Node()
            {
                nodeId = 19,
                parentId = node.nodeId
            };
            Node node20 = new Node()
            {
                nodeId = 20,
                parentId = node.nodeId
            };
            StartCreator19(node19);
            node.Add(node19);
            node.Add(node20);
        }
        private void StartCreator16(Node node)
        {
            Node node18 = new Node()
            {
                nodeId = 18,
                parentId = node.nodeId
            };
            node.Add(node18);
        }
        private void StartCreator19(Node node)
        {
            Node node21 = new Node()
            {
                nodeId = 21,
                parentId = node.nodeId
            };
            node.Add(node21);
        }
    }
}
