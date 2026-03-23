using System;                     
using System.Collections;          
using System.Collections.Generic;    
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    public class MyCollection<T> : ICollection<T>, ICloneable
        where T : IComparable<T>
    {
        protected Point<T> root;

        protected int count;

        public int Count => count;

        public bool IsReadOnly => false;

        protected class Point<T> where T : IComparable<T>
        {
            public T Data { get; set; }
            public Point<T> Left { get; set; }
            public Point<T> Right { get; set; }

            public int Balance { get; set; }

            public Point(T data)
            {
                Data = data;
                Left = null;
                Right = null;
                Balance = 0;
            }
        }

        public MyCollection()
        {
            root = null;
            count = 0;
        }
        
        protected Point<T> RotateRight(Point<T> p)
        {
            Point<T> q = p.Left;
            p.Left = q.Right;
            q.Right = p;

            return q;
        }

        protected Point<T> RotateLeft(Point<T> q)
        {
            Point<T> p = q.Right;
            q.Right = p.Left;
            p.Left = q;

            return p;
        }

        protected Point<T> BalanceNode(Point<T> p)
        {
            if (p.Balance == 2)
            {
                if (p.Right.Balance < 0)
                {
                    p.Right = RotateRight(p.Right);
                }
                return RotateLeft(p);
            }

            if (p.Balance == -2)
            {
                if (p.Left.Balance > 0)
                {
                    p.Left = RotateLeft(p.Left);
                }
                return RotateRight(p);
            }

            return p;
        }

        private int GetHeight(Point<T> node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        }

        public void Add(T item)
        {
            if (item == null) return;

            root = AddRecursive(root, item);
            count++;
        }

        private Point<T> AddRecursive(Point<T> node, T item)
        {
            if (node == null)
                return new Point<T>(item);

            int comparison = item.CompareTo(node.Data);

            if (comparison < 0)
            {
                node.Left = AddRecursive(node.Left, item);
            }
            else if (comparison > 0)
            {
                node.Right = AddRecursive(node.Right, item);
            }
            else
            {
                return node;
            }

            node.Balance = GetHeight(node.Right) - GetHeight(node.Left);

            return BalanceNode(node);
        }

        public bool Contains(T item)
        {
            if (item == null) 
                return false;
            return FindRecursive(root, item);
        }

        private bool FindRecursive(Point<T> current, T item)
        {
            if (current == null) 
                return false;

            int comparison = item.CompareTo(current.Data);

            if (comparison == 0)
            {
                return true; // Нашли
            }

            if (comparison < 0)
            {
                return FindRecursive(current.Left, item);
            }
            else
            {
                return FindRecursive(current.Right, item);
            }
        }
        // начало обхода
        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal(root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerable<T> InOrderTraversal(Point<T> node)
        {
            if (node != null)
            {
                foreach (var leftItem in InOrderTraversal(node.Left))
                    yield return leftItem;

                yield return node.Data;

                foreach (var rightItem in InOrderTraversal(node.Right))
                    yield return rightItem;
            }
        }

        public object Clone()
        {
            MyCollection<T> newCollection = new MyCollection<T>();
            newCollection.root = CopyNodes(this.root);
            newCollection.count = this.count;
            return newCollection;
        }

        private Point<T> CopyNodes(Point<T> node)
        {
            if (node == null) 
                return null;

            Point<T> newNode = new Point<T>(node.Data);

            newNode.Balance = node.Balance;

            newNode.Left = CopyNodes(node.Left);
            newNode.Right = CopyNodes(node.Right);

            return newNode;
        }

        public void Clear()
        {
            root = null;
            count = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) 
                throw new ArgumentNullException();
            if (arrayIndex < 0) 
                throw new ArgumentOutOfRangeException();

            foreach (var item in this)
            {
                array[arrayIndex++] = item;
            }
        }
        private Point<T> FindMin(Point<T> node)
        {
            if (node.Left == null)
                return node;

            return FindMin(node.Left);
        }

        public bool Remove(T item)
        {
            if (item == null || !Contains(item)) return false;

            root = RemoveRecursive(root, item);
            count--;
            return true;
        }

        private Point<T> RemoveRecursive(Point<T> node, T item)
        {
            if (node == null) return null;

            int comparison = item.CompareTo(node.Data);

            if (comparison < 0)
            {
                node.Left = RemoveRecursive(node.Left, item);
            }
            else if (comparison > 0)
            {
                node.Right = RemoveRecursive(node.Right, item);
            }

            else
            {
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;

                Point<T> successor = FindMin(node.Right);
                node.Data = successor.Data;
                node.Right = RemoveRecursive(node.Right, successor.Data);
            }

            node.Balance = GetHeight(node.Right) - GetHeight(node.Left);
            return BalanceNode(node);
        }
    }
}
