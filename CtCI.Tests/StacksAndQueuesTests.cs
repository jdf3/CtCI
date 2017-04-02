using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static CtCI.StacksAndQueues;

namespace CtCI.Tests
{
    [TestClass]
    public class StacksAndQueuesTests
    {
        [TestMethod]
        public void TripleStackResizePop1()
        {
            var ts = new TripleStack<int>();
            ts.Push2(13);
            ts.Push3(23);

            ts.Push1(1);
            ts.Push1(2);
            ts.Push1(3);
            ts.Push1(4);
            ts.Push1(5);
            ts.Push1(6);
            ts.Push1(7);
            ts.Push1(8);
            ts.Push1(9);
            ts.Push1(10);
            ts.Push1(11);
            ts.Push1(12);

            Assert.AreEqual(12, ts.Pop1());
            Assert.AreEqual(13, ts.Pop2());
            Assert.AreEqual(23, ts.Pop3());
        }

        [TestMethod]
        public void TripleStackResizePop2()
        {
            var ts = new TripleStack<int>();
            ts.Push1(13);
            ts.Push3(23);

            ts.Push2(1);
            ts.Push2(2);
            ts.Push2(3);
            ts.Push2(4);
            ts.Push2(5);
            ts.Push2(6);
            ts.Push2(7);
            ts.Push2(8);
            ts.Push2(9);
            ts.Push2(10);
            ts.Push2(11);
            ts.Push2(12);

            Assert.AreEqual(12, ts.Pop2());
            Assert.AreEqual(13, ts.Pop1());
            Assert.AreEqual(23, ts.Pop3());
        }

        [TestMethod]
        public void TripleStackResizePop3()
        {
            var ts = new TripleStack<int>();
            ts.Push1(13);
            ts.Push2(23);

            ts.Push3(1);
            ts.Push3(2);
            ts.Push3(3);
            ts.Push3(4);
            ts.Push3(5);
            ts.Push3(6);
            ts.Push3(7);
            ts.Push3(8);
            ts.Push3(9);
            ts.Push3(10);
            ts.Push3(11);
            ts.Push3(12);

            Assert.AreEqual(12, ts.Pop3());
            Assert.AreEqual(13, ts.Pop1());
            Assert.AreEqual(23, ts.Pop2());
        }

        [TestMethod]
        public void MinStackTests()
        {
            var ms = new MinStack<int>();
            ms.Push(2);
            ms.Push(6);
            ms.Push(-1);
            ms.Push(10);
            ms.Push(-3);
            ms.Push(15);

            Assert.AreEqual(-3, ms.Min());
        }

        [TestMethod]
        public void StackOfPlatesTests()
        {
            var stackOfPlates = new StackOfPlates<int>();

            stackOfPlates.Push(1);
            stackOfPlates.Push(2);
            stackOfPlates.Push(3);
            stackOfPlates.Push(4);
            stackOfPlates.Push(5);
            stackOfPlates.Pop();
            stackOfPlates.Pop();
            stackOfPlates.Pop();

            Assert.AreEqual(2, stackOfPlates.Peek());
        }

        [TestMethod]
        public void MyQueueTests()
        {
            var queue = new MyQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Dequeue();
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Peek();
            queue.Dequeue();
            queue.Dequeue();

            Assert.AreEqual(4, queue.Peek());
        }

        [TestMethod]
        public void StackSortTests()
        {
            var stack = new Stack<int>();
            stack.Push(4);
            stack.Push(2);
            stack.Push(3);
            stack.Push(1);

            StackSort(stack);

            Assert.AreEqual(1, stack.Pop());
            Assert.AreEqual(2, stack.Pop());
            Assert.AreEqual(3, stack.Pop());
            Assert.AreEqual(4, stack.Pop());
        }

        [TestMethod]
        public void AnimalShelterTests()
        {
            var animalShelter = new AnimalShelter();
            animalShelter.Enqueue(AnimalShelter.Animal.Cat, "Sadie");
            animalShelter.Enqueue(AnimalShelter.Animal.Dog, "Tucker");
            animalShelter.Enqueue(AnimalShelter.Animal.Cat, "Snickers");
            animalShelter.Enqueue(AnimalShelter.Animal.Dog, "Cooper");

            Assert.AreEqual("Sadie", animalShelter.DequeueAny());
            Assert.AreEqual("Snickers", animalShelter.DequeueCat());
            Assert.AreEqual("Tucker", animalShelter.DequeueDog());
            Assert.AreEqual("Cooper", animalShelter.DequeueAny());
        }
    }
}
