using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CtCI
{
    public static class TreesAndGraphs
    {
        #region 4.1

        /* Given a directed graph, design an algorithm to find out whether there is a route between two nodes. */
        // Simple DFS
        public class Node<T>
        {
            private readonly List<Node<T>> _neighbors = new List<Node<T>>();

            public T Value { get; set; }

            public IEnumerable<Node<T>> Neighbors => _neighbors.AsReadOnly();

            public void AddNeighbors(params Node<T>[] neighbors)
            {
                foreach (Node<T> neighbor in neighbors)
                {
                    _neighbors.Add(neighbor);
                }
            }
        }

        public static bool PathExists(Node<int> n1, Node<int> n2)
        {
            return PathExistsHelper(n1, n2, new HashSet<Node<int>>());

            bool PathExistsHelper(Node<int> start, Node<int> target, HashSet<Node<int>> visited)
            {
                if (start == target)
                {
                    return true;
                }

                visited.Add(start);
                foreach (Node<int> node in start.Neighbors)
                {
                    if (visited.Contains(node))
                    {
                        continue;
                    }
                    visited.Add(node);
                    if (PathExistsHelper(node, target, visited))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        #endregion

        #region 4.2

        /* Given a sorted (increasing order) array with unique integer elements, write an algorithm
                 * to create a binary search tree with minimal height. */
        // Idea: pick the middle element as the root, then recurse for the left
        // and right sides of the tree with the left and right sides of the array
        public class BSTNode
        {
            public int Value { get; set; }
            public BSTNode Left { get; set; }
            public BSTNode Right { get; set; }
        }

        public static BSTNode MakeBSTFromSortedArray(int[] sortedArray)
        {
            return Helper(sortedArray, 0, sortedArray.Length - 1);

            BSTNode Helper(int[] array, int start, int end)
            {
                if (start > end) return null;
                if (start == end) return new BSTNode {Value = array[start]};

                int middle = start + (end - start) / 2;
                return new BSTNode
                {
                    Value = array[middle],
                    Left = Helper(array, start, middle - 1),
                    Right = Helper(array, middle + 1, end)
                };
            }
        }

        #endregion

        #region 4.3

        /* Given a binary tree, design an algorithm which creates a linked list of all the nodes at each depth
                 * (e.g., if you have a tree with depth D, you'll have D linked lists) */
        // Hooray! I improved this drastically - seems constant >=10x speedup! :)
        public static List<List<int>> GetLevels(BSTNode root)
        {
            var pairs = new List<List<int>>();
            AddValuesDFS(root, 0);
            return pairs;

            void AddValuesDFS(BSTNode r, int depth)
            {
                if (pairs.Count <= depth) pairs.Add(new List<int>());
                pairs[depth].Add(r.Value);

                if (r.Left != null) AddValuesDFS(r.Left, depth + 1);
                if (r.Right != null) AddValuesDFS(r.Right, depth + 1);
            }
        }

        #endregion

        #region 4.4

        /* Implement a function to check if a binary tree is balanced. For the purposes of this question, a balanced
                 * tree is defined to be a tree such that the heights of the two subtrees of any node never differ by more than one */
        // TODO: this little helper function is smelly (SRP), but i'm blanking on the "correct" way to clean it up...
        public static bool IsBalanced(BSTNode root)
        {
            return GetHeightIfBalanced(root) != -1;

            int GetHeightIfBalanced(BSTNode node)
            {
                if (node == null) return 0;

                int leftHeight = GetHeightIfBalanced(node.Left);
                if (leftHeight == -1)
                {
                    return -1;
                }

                int rightHeight = GetHeightIfBalanced(node.Right);
                if (rightHeight == -1)
                {
                    return -1;
                }

                if (Math.Abs(leftHeight - rightHeight) > 1)
                {
                    return -1;
                }
                else
                {
                    return Math.Max(leftHeight, rightHeight) + 1;
                }
            }
        }

        #endregion

        #region 4.5

        /* Implement a function to check if a binary tree is a binary search tree */
        // It's a binary search tree iff everything left of the root is less than or equal to the root,
        // and everything to the right of the root is greater than or equal to the root,
        // and each subtree is also a binary search tree
        public static bool IsBinarySearchTree(BSTNode root)
        {
            return IsBinarySearchTreeHelper(root, out int min, out int max);

            bool IsBinarySearchTreeHelper(BSTNode node, out int minInTree, out int maxInTree)
            {
                if (node == null)
                {
                    minInTree = int.MinValue;
                    maxInTree = int.MaxValue;
                    return true;
                }

                if (node.Left == null && node.Right == null)
                {
                    minInTree = node.Value;
                    maxInTree = node.Value;
                    return true;
                }

                if (node.Left == null)
                {
                    bool isRightBS = IsBinarySearchTreeHelper(node.Right, out minInTree, out maxInTree);
                    if (!isRightBS || minInTree < node.Value) return false;
                    else
                    {
                        minInTree = node.Value;
                        return true;
                    }
                }

                if (node.Right == null)
                {
                    bool isLeftBS = IsBinarySearchTreeHelper(node.Left, out minInTree, out maxInTree);
                    if (!isLeftBS || maxInTree > node.Value) return false;
                    else
                    {
                        maxInTree = node.Value;
                        return true;
                    }
                }

                bool isLeftOK = IsBinarySearchTreeHelper(node.Left, out int leftMin, out int leftMax);
                if (!isLeftOK || leftMax > node.Value) return false;

                bool isRightOK = IsBinarySearchTreeHelper(node.Right, out int rightMin, out int rightMax);
                if (!isRightOK || rightMin < node.Value) return false;

                minInTree = leftMin;
                maxInTree = rightMin;
                return true;
            }
        }

        #endregion

        #region 4.6
        /* Write an algorithm to find the "next" node (i.e., in-order successor) of a given node in a binary search tree.
         * You may assume that each node has a link to its parent. */
        public class ParentBSTNode
        {
            public int Value { get; set; }
            public ParentBSTNode Left { get; set; }
            public ParentBSTNode Right { get; set; }
            public ParentBSTNode Parent { get; set; }
        }

        public static ParentBSTNode GetSuccessor(ParentBSTNode node)
        {
            ParentBSTNode successor;
            if (node.Right != null)
            {
                successor = node.Right;
                while (successor.Left != null)
                {
                    successor = successor.Left;
                }
                return successor;
            }

            successor = node.Parent;
            while (successor != null && successor.Value < node.Value)
            {
                successor = successor.Parent;
            }
            return successor;
        }
        #endregion

        #region 4.7
        /* You are given a list of projects and a list of dependencies (which is a list of pairs of projects, where the
         * second project is dependent on the first project). All of a project's dependencies must be built before
         * the project is. Find a build order that will allow the projects to be built. If there is no valid build order, return an error.
         * EXAMPLE:
         * In:
         *   Projects: a, b, c, d, e, f
         *   Deps:     (a, d), (f, b), (b, d), (f, a), (d, c) 
         * Out:
         *   f, e, a, b, d, c
         */
        
        #endregion

        #region 4.8

        #endregion

        #region 4.9

        #endregion

        #region 4.10

        #endregion

        #region 4.11

        #endregion

        #region 4.12

        #endregion
    }
}