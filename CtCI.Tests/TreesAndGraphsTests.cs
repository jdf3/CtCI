using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static CtCI.TreesAndGraphs;

namespace CtCI.Tests
{
    [TestClass]
    public class TreesAndGraphsTests
    {
        [TestMethod]
        public void PathExistsTests()
        {
            var n1 = new Node<int>();
            var n2 = new Node<int>();
            var n3 = new Node<int>();
            var n4 = new Node<int>();
            var n5 = new Node<int>();
            var n6 = new Node<int>();

            Assert.IsFalse(PathExists(n1, n6));

            n1.AddNeighbors(n2, n3, n4);
            n2.AddNeighbors(n3, n4);
            n4.AddNeighbors(n5);
            n5.AddNeighbors(n6);
            n6.AddNeighbors(n1, n2, n3, n4, n5);

            Assert.IsTrue(PathExists(n1, n6));
        }

        [TestMethod]
        public void MakeBSTFromSortedArrayTests()
        {
            var array = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7
            };

            BSTNode bst = MakeBSTFromSortedArray(array);
            Assert.AreEqual(4, bst.Value);
            Assert.AreEqual(2, bst.Left.Value);
            Assert.AreEqual(1, bst.Left.Left.Value);
            Assert.AreEqual(3, bst.Left.Right.Value);
            Assert.AreEqual(6, bst.Right.Value);
            Assert.AreEqual(5, bst.Right.Left.Value);
            Assert.AreEqual(7, bst.Right.Right.Value);
        }

        [TestMethod]
        public void GetLevelsTests()
        {
            var array = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7
            };

            BSTNode bst = MakeBSTFromSortedArray(array);

            List<List<int>> levels = GetLevels(bst);

            Assert.AreEqual(4, levels[0][0]);
            Assert.AreEqual(7, levels[2][3]);
        }

        [TestMethod]
        public void IsBalancedTests()
        {
            var bst = new BSTNode { Left = new BSTNode { Left = new BSTNode() } };

            Assert.IsFalse(IsBalanced(bst), "1");

            bst.Right = new BSTNode();

            Assert.IsTrue(IsBalanced(bst), "2");

            var bst2 = new BSTNode() { Right = bst };

            Assert.IsFalse(IsBalanced(bst2), "3");
        }

        [TestMethod]
        public void IsBinarySearchTreeTests()
        {
            var array = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7
            };

            BSTNode bst = MakeBSTFromSortedArray(array);

            Assert.IsTrue(IsBinarySearchTree(bst));

            array[3] = 6;
            bst = MakeBSTFromSortedArray(array);

            Assert.IsFalse(IsBinarySearchTree(bst));
        }

        [TestMethod]
        public void GetSuccessorTests()
        {
            var bst = new ParentBSTNode()
            {
                Value = 4
            };
            var node2 = new ParentBSTNode()
            {
                Value = 6,
                Parent = bst
            };
            bst.Right = node2;
            var node3 = new ParentBSTNode()
            {
                Value = 5,
                Parent = node2
            };
            node2.Left = node3;

            Assert.AreEqual(node2, GetSuccessor(node3));
            Assert.AreEqual(node3, GetSuccessor(bst));
        }

        [TestMethod]
        public void GetOrderTests()
        {
            var projects = new List<string>
            {
                "a", "b", "c", "d", "e", "f"
            };
            var dependencies = new List<(string, string)>
            {
                ("a", "b"), ("b", "c"), ("c", "d"), ("d", "e"), ("e", "f")
            };

            IList<string> buildOrder = GetOrder(projects, dependencies);

            Assert.AreEqual("a", buildOrder[0]);
            Assert.AreEqual("f", buildOrder[5]);

            dependencies.Add(("f", "a"));

            buildOrder = GetOrder(projects, dependencies);

            Assert.IsNull(buildOrder);
        }

        [TestMethod]
        public void FirstCommonAncestorTests()
        {
            var a = new BSTNode() { Value = 1 };
            var b = new BSTNode() { Value = 2 };
            var c = new BSTNode() { Value = 3 };
            var d = new BSTNode() { Value = 4 };
            var e = new BSTNode() { Value = 5 };
            var f = new BSTNode() { Value = 6 };
            var g = new BSTNode() { Value = 7 };
            a.Left = b;
            a.Right = c;
            b.Left = d;
            b.Right = e;
            c.Left = f;
            c.Right = g;

            Assert.AreEqual(a, FirstCommonAncestor(a, d, g), "simple");
            Assert.AreEqual(c, FirstCommonAncestor(a, f, g), "simple 2");
            Assert.AreEqual(a, FirstCommonAncestor(a, a, f), "includes root");
            Assert.AreEqual(a, FirstCommonAncestor(a, b, g), "simple 3");
        }

        [TestMethod]
        public void GetGeneratingArraysTests()
        {
            var root = new BSTNode
            {
                Value = 2,
                Left = new BSTNode { Value = 1 },
                Right = new BSTNode { Value = 3 }
            };

            var generatingArrays = new List<int[]>(GetGeneratingArrays(root));

            Assert.AreEqual(2, generatingArrays.Count);
            Assert.AreEqual(2, generatingArrays[0][0]);
            Assert.AreEqual(1, generatingArrays[0][1]);
            Assert.AreEqual(3, generatingArrays[0][2]);

            Assert.AreEqual(2, generatingArrays[1][0]);
            Assert.AreEqual(3, generatingArrays[1][1]);
            Assert.AreEqual(1, generatingArrays[1][2]);

            var root2 = new BSTNode
            {
                Value = 2,
                Left = new BSTNode
                {
                    Value = 2
                },
                Right = new BSTNode
                {
                    Value = 4,
                    Left = new BSTNode
                    {
                        Value = 3
                    },
                    Right = new BSTNode
                    {
                        Value = 5
                    }
                }
            };

            Assert.AreEqual(8, GetGeneratingArrays(root2).Count());
        }

        [TestMethod]
        public void ContainsSubtreeAtTests()
        {
            var root1 = new BSTNode
            {
                Value = 2,
                Left = new BSTNode
                {
                    Value = 2
                },
                Right = new BSTNode
                {
                    Value = 4,
                    Left = new BSTNode
                    {
                        Value = 3
                    },
                    Right = new BSTNode
                    {
                        Value = 5
                    }
                }
            };

            var root2 = new BSTNode
            {
                Value = 4,
                Left = new BSTNode { Value = 3 },
                Right = new BSTNode { Value = 5 }
            };

            Assert.IsTrue(ContainsSubtree(root1, root2));

            root2.Right = null;

            Assert.IsTrue(ContainsSubtree(root1, root2));

            root2.Right = new BSTNode {Value = 6};

            Assert.IsFalse(ContainsSubtree(root1, root2));
        }
    }
}
