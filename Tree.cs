using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Collections;

namespace FractalTree
{
    class Tree
    {
        public class Node
        {
            public Node leftBottom = null;
            public Node leftTop = null;
            public Node rightBottom = null;
            public Node rightTop = null;
            public Node parent = null;
            public int x;
            public int y;
            public int pos;
            public int depth;

            public Node(int inputX, int inputY)
            {
                x = inputX;
                y = inputY;
            }
        }

        public Node root = null;
        static public int size = 0;

        public void Delete() {
            root = null;
        }

        public List<Node> BFS() {
            List<Node> nodeList = new List<Node>();
            if (root == null) {
                return nodeList;
            }
            var queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0) {
                var node = queue.Dequeue();
                nodeList.Add(node);

                if (node.leftTop != null) queue.Enqueue(node.leftTop);
                if (node.leftBottom != null) queue.Enqueue(node.leftBottom);
                if (node.rightBottom != null) queue.Enqueue(node.rightBottom);
                if (node.rightTop != null) queue.Enqueue(node.rightTop);
            }
            return nodeList;
        }

        public void Add(int x, int y)
        {
            Node child = new Node(x, y);

            if (root == null)
            {
                root = child;
                size++;
                return;
            }

            Node iterator = root;
            int depth = 1;
            while (true)
            {
                if (iterator.x > child.x && iterator.y < child.y)
                {
                    if (iterator.leftTop != null)
                    {
                        iterator = iterator.leftTop;
                        depth++;
                        continue;
                    }
                    else
                    {
                        iterator.leftTop = child;
                        child.parent = iterator;
                        child.pos = 0;
                        child.depth = depth;
                        depth = 1;
                        size++;
                        break;
                    }
                }
                if (iterator.x > child.x && iterator.y > child.y)
                {
                    if (iterator.leftBottom != null)
                    {
                        iterator = iterator.leftBottom;
                        depth++;
                        continue;
                    }
                    else
                    {
                        iterator.leftBottom = child;
                        child.parent = iterator;
                        child.pos = 1;
                        child.depth = depth;
                        depth = 1;
                        size++;
                        break;
                    }
                }
                if (iterator.x < child.x && iterator.y > child.y)
                {
                    if (iterator.rightBottom != null)
                    {
                        iterator = iterator.rightBottom;
                        depth++;
                        continue;
                    }
                    else
                    {
                        iterator.rightBottom = child;
                        child.parent = iterator;
                        child.pos = 2;
                        child.depth = depth;
                        depth = 1;
                        size++;
                        break;
                    }
                }
                if (iterator.x < child.x && iterator.y < child.y)
                {
                    if (iterator.rightTop != null)
                    {
                        iterator = iterator.rightTop;
                        depth++;
                        continue;
                    }
                    else
                    {
                        iterator.rightTop = child;
                        child.parent = iterator;
                        child.pos = 3;
                        child.depth = depth;
                        depth = 1;
                        size++;
                        break;
                    }
                }
            }
        }

        public int[ , ] GetEdges() {
            int[,] arr = new int[4, 2];
            Node iterator = root;
            while (iterator.leftTop != null) {
                iterator = iterator.leftTop;
            }
            arr[0, 0] = iterator.x;
            arr[0, 1] = iterator.y;
            iterator = root;
            while (iterator.leftBottom != null)
            {
                iterator = iterator.leftBottom;
            }
            arr[1, 0] = iterator.x;
            arr[1, 1] = iterator.y;
            iterator = root;
            while (iterator.rightBottom != null)
            {
                iterator = iterator.rightBottom;
            }
            arr[2, 0] = iterator.x;
            arr[2, 1] = iterator.y;
            iterator = root;
            while (iterator.rightTop!= null)
            {
                iterator = iterator.rightTop;
            }
            arr[3, 0] = iterator.x;
            arr[3, 1] = iterator.y;
            return arr;
        }

    }
}
