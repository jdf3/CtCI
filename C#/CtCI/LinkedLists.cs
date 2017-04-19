using System;
using System.Collections.Generic;
using System.Text;

namespace CtCI
{
    public static class LinkedLists
    {
        #region 2.1
        public static void RemoveDups(Node nodeIn)
        {
            if (nodeIn == null) return;

            var seen = new HashSet<string>();
            for (Node n = nodeIn; n != null; n = n.Next)
            {
                seen.Add(n.Label);

                Node next = n.Next;
                while (next != null && seen.Contains(next.Label))
                {
                    n.Next = next.Next;
                    next = n.Next;
                }
            }
        }

        public static void RemoveDupsNoAuxiliaryStructure(Node nodeIn)
        {
            for (Node iter = nodeIn; iter != null; iter = iter.Next)
            {
                for (Node iter2 = iter; iter2?.Next != null; iter2 = iter2.Next)
                {
                    if (string.Equals(iter.Label, iter2.Next.Label))
                    {
                        iter2.Next = iter2.Next.Next;
                    }
                }
            }
        }
        #endregion

        #region 2.2
        // A simple two-runner solution to this: let one runner get a k-length headstart on running over the other. when the first one hits the end, the second one will be the kth-to-last element.
        // This is O(N) time and O(1) space.
        // That sounds fun to write, so let's do it
        public static string KthToLast(Node n, int k)
        {
            Node iter1 = n;
            Node iter2 = n;
            // ("1th to last" should be the last, so k - 1)
            for (int i = 0; i < k - 1; i++)
            {
                if (iter1.Next == null)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    iter1 = iter1.Next;
                }
            }
            while (iter1.Next != null)
            {
                iter1 = iter1.Next;
                iter2 = iter2.Next;
            }
            return iter2.Label;
        }
        #endregion

        #region 2.3
        public static void RemoveFromMiddle(Node n)
        {
            if (n?.Next == null)
            {
                throw new InvalidOperationException();
            }
            n.Value = n.Next.Value;
            n.Label = n.Next.Label;
            n.Next = n.Next.Next;
        }
        #endregion

        #region 2.4
        // Write code to partition a linked list around a value x, such that all nodes less than x come before all nodes greater than or equal to x. If x is contained within the list, the values of x only need to be after the elements less than x.
        // One idea would be to go through the list, element by element, and add them to two other lists...
        // This is awkward as a static method, since it's not stable...
        public static Node Partition(Node n, int x)
        {
            var lesserAnchor = new Node();
            Node lesserTail = lesserAnchor;
            var greaterAnchor = new Node();
            Node greaterTail = greaterAnchor;

            for (Node iter = n; iter != null; iter = iter.Next)
            {
                if (iter.Value <= x)
                {
                    lesserTail.Next = iter;
                    lesserTail = lesserTail.Next;
                }
                else
                {
                    greaterTail.Next = iter;
                    greaterTail = greaterTail.Next;
                }
            }

            lesserTail.Next = greaterAnchor.Next;

            greaterTail.Next = null;

            return lesserAnchor.Next;
        }
        #endregion

        #region 2.5
        // You have two numbers represented by a linked list, where each node contains a single digit.
        // The digits are stored in reverse order, such that the 1's digit is at the head of the list.
        // Write a function that adds the two numbers and returns the sum as a linked list.
        // Ok, solution looks longer than I imagined it being in my head, so let's try this out
        public static int Sum(Node list1, Node list2)
        {
            if (list1 == null && list2 == null)
            {
                return 0;
            }

            int firstValue = list1?.Value ?? 0;
            int secondValue = list2?.Value ?? 0;

            return firstValue + secondValue + 10 * Sum(list1?.Next, list2?.Next);
        }

        public static int SumBigEndian(Node list1, Node list2)
        {
            int length1 = GetLength(list1);
            int length2 = GetLength(list2);

            if (length1 > length2)
            {
                var head = new Node();
                Node current = head;
                for (int i = 0; i < length1 - length2 - 1; i++)
                {
                    current.Next = new Node();
                    current = current.Next;
                }
                current.Next = list2;
                list2 = head;
            }
            else if (length2 > length1)
            {
                var head = new Node();
                Node current = head;
                for (int i = 0; i < length2 - length1 - 1; i++)
                {
                    current.Next = new Node();
                    current = current.Next;
                }
                current.Next = list1;
                list1 = head;
            }

            (int total, int d) = SumBigEndianWithDepth(list1, list2);

            return total;

            (int sum, int depth) SumBigEndianWithDepth(Node l1, Node l2)
            {
                if (l1 == null && l2 == null)
                {
                    return (0, -1);
                }

                (int sum, int depth) = SumBigEndianWithDepth(l1?.Next, l2?.Next);

                int value1 = l1?.Value ?? 0;
                int value2 = l2?.Value ?? 0;

                depth += 1;
                sum += (value1 + value2) * (int)Math.Pow(10, depth);
                return (sum, depth);
            }

            int GetLength(Node n)
            {
                int i = 1;
                while ((n = n.Next) != null) i++;
                return i;
            }
        }
        #endregion

        #region 2.6
        // Implement a function to check if a linked list is a palindrome.
        // Find the mid-point using a fast runner and a slow runner. While navigating to the mid point, add what you see to a stack.
        // Then, use the half-runner to run the last half of the list, and ensure that whatever you pop off the stack matches as you go.
        public static bool IsPalindrome(Node n)
        {
            if (n?.Next == null) return true;

            var seen = new Stack<int>();
            Node fast = n;
            Node slow = n;

            seen.Push(slow.Value);
            while (fast.Next != null && fast.Next.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
                seen.Push(slow.Value);
            }
            if (fast.Next != null) slow = slow.Next;

            while (seen.Count > 0)
            {
                int current = seen.Pop();
                if (slow.Value != current) return false;
                slow = slow.Next;
            }
            return true;
        }
        #endregion

        #region 2.7
        // Intersection: Given two (singly) linked lists, determine if the two lists intersect. Return the intersecting node.
        // Note that the intersection is defined based on reference, not value. That is, if the kth node of the first linked
        // list is the exact same node (by reference) as the jth node of the second linked list, then they are intersecting.
        // Note: Returns null if no intersection
        public static Node GetIntersectingNode(Node list1, Node list2)
        {
            (Node last1, int length1) = GetLastNodeAndLength(list1);
            (Node last2, int length2) = GetLastNodeAndLength(list2);

            if (last1 != last2)
            {
                return null;
            }

            for (int i = 0; i < length1 - length2; i++)
            {
                list1 = list1.Next;
            }
            for (int i = 0; i < length2 - length1; i++)
            {
                list2 = list2.Next;
            }

            while (list1 != list2)
            {
                list1 = list1.Next;
                list2 = list2.Next;
            }
            return list1;

            (Node, int) GetLastNodeAndLength(Node n)
            {
                int i = 1;
                while (n.Next != null)
                {
                    n = n.Next;
                    i += 1;
                }
                return (n, i);
            }
        }
        #endregion

        #region 2.8
        // Given a linked list, determine if it has a cycle, and if it does, return the entry point of the cycle
        public static Node GetEntryPointOfCorruptedList(Node n)
        {
            Node fast = n;
            Node slow = n;

            if (n.Next == null)
            {
                return null;
            }

            do
            {
                if (n.Next.Next == null) return null;
                slow = slow.Next;
                fast = fast.Next.Next;
            } while (fast != slow);

            slow = n;
            while (fast != slow)
            {
                fast = fast.Next;
                slow = slow.Next;
            }
            return slow;
        }
        #endregion
    }

    public class Node
    {
        public string Label { get; set; }
        public int Value { get; set; }
        public Node Next { get; set; }

        public void Append(Node newNode)
        {
            Node n = this;
            while (n.Next != null)
            {
                n = n.Next;
            }
            n.Next = newNode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("{ ");

            for (Node n = this; n != null; n = n.Next)
            {
                if (string.IsNullOrEmpty(n.Label))
                {
                    sb.Append(GetHashCode());
                }
                else
                {
                    sb.Append(n.Label);
                }
                sb.Append(", ");
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}