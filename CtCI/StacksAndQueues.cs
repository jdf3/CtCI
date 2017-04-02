using System;
using System.Collections.Generic;
using System.Text;

namespace CtCI
{
    public static class StacksAndQueues
    {
        #region 3.1
        // Describe how you could use a single array to implement three stacks
        // Could refactor out a sort of ResizeBy function, but going to move on to other questions
        public class TripleStack<T>
        {
            private const int InitialSize = 30;
            private T[] _array;
            private int _start1;
            private int _head1;
            private int _start2;
            private int _head2;
            private int _start3;
            private int _head3;

            public TripleStack()
            {
                _array = new T[InitialSize];
                _start1 = 0;
                _head1 = _start1 - 1;
                _start2 = InitialSize / 3;
                _head2 = _start2 - 1;
                _start3 = 2 * InitialSize / 3;
                _head3 = _start3 - 1;
            }

            public void Push1(T item)
            {
                if (_head1 + 1 == _start2)
                {
                    int increase = _start2 - _start1;
                    var newArray = new T[_array.Length + increase];
                    for (int i = _start1; i < _start2; i++)
                    {
                        newArray[i] = _array[i];
                    }
                    for (int i = _start2; i < _array.Length; i++)
                    {
                        newArray[i + increase] = _array[i];
                    }
                    _head2 += increase;
                    _head3 += increase;
                    _start2 += increase;
                    _start3 += increase;
                    _array = newArray;
                }
                _head1 += 1;
                _array[_head1] = item;
            }

            public T Pop1()
            {
                if (_head1 < _start1) throw new InvalidOperationException("Stack is empty");
                T item = _array[_head1];
                _array[_head1] = default(T);
                _head1 -= 1;
                return item;
            }

            public T Peek1()
            {
                if (_head1 < _start1) throw new InvalidOperationException("Stack is empty");
                return _array[_head1];
            }

            public bool IsEmpty1(T item)
            {
                return _head1 >= _start1;
            }

            public void Push2(T item)
            {
                if (_head2 + 1 == _start3)
                {
                    int increase = _start3 - _start2;
                    var newArray = new T[_array.Length + increase];
                    for (int i = _start1; i < _start3; i++)
                    {
                        newArray[i] = _array[i];
                    }
                    for (int i = _start3; i < _array.Length; i++)
                    {
                        newArray[i + increase] = _array[i];
                    }
                    _head3 += increase;
                    _start3 += increase;
                    _array = newArray;
                }
                _head2 += 1;
                _array[_head2] = item;

            }

            public T Pop2()
            {
                if (_head2 < _start2) throw new InvalidOperationException("Stack is empty");
                T item = _array[_head2];
                _array[_head2] = default(T);
                _head2 -= 1;
                return item;
            }

            public T Peek2()
            {
                if (_head2 < _start2) throw new InvalidOperationException("Stack is empty");
                return _array[_head2];
            }

            public bool IsEmpty2(T item)
            {
                return _head2 >= _start2;
            }

            public void Push3(T item)
            {
                if (_head3 + 1 == _array.Length)
                {
                    int increase = _array.Length - _start3;
                    var newArray = new T[_array.Length + increase];
                    for (int i = _start1; i < _array.Length; i++)
                    {
                        newArray[i] = _array[i];
                    }
                    _array = newArray;
                }
                _head3 += 1;
                _array[_head3] = item;

            }

            public T Pop3()
            {
                if (_head3 < _start3) throw new InvalidOperationException("Stack is empty");
                T item = _array[_head3];
                _array[_head3] = default(T);
                _head3 -= 1;
                return item;
            }

            public T Peek3()
            {
                if (_head3 < _start3) throw new InvalidOperationException("Stack is empty");
                return _array[_head3];
            }

            public bool IsEmpty3(T item)
            {
                return _head3 >= _array.Length;
            }

            private void ExpandArray(
        }
        #endregion

        #region 3.2
        // How would you design a stack which, in addition to push and pop, has a function min which
        // returns the minimum element? Push, pop, and min should all operate in O(1) time
        // I don't see a way to implement this without taking O(N) additional space to support an O(1) time min operation.
        // So, just use a side stack of the min, and whenever we push or pop, if the element is sufficiently big (care for equality case),
        // we'll push or pop from the side stack, too
        public class MinStack<T> where T : IComparable<T>
        {
            private readonly Stack<T> _mainStack;
            private readonly Stack<T> _minStack;

            public MinStack()
            {
                _mainStack = new Stack<T>();
                _minStack = new Stack<T>();
            }

            public void Push(T item)
            {
                _mainStack.Push(item);
                if (_minStack.Count == 0 || _minStack.Count > 0 && _minStack.Peek().CompareTo(item) > 0)
                {
                    _minStack.Push(item);
                }
            }

            public T Min()
            {
                return _minStack.Peek();
            }

            public T Pop()
            {
                T item = _mainStack.Pop();
                if (_minStack.Count > 0 && _minStack.Peek().CompareTo(item) > 0)
                {
                    _minStack.Pop();
                }
                return item;
            }

            public T Peek()
            {
                return _mainStack.Peek();
            }

            public bool IsEmpty()
            {
                return _mainStack.Count > 0;
            }
        }
        #endregion

        #region 3.3
        // Implement a "stack of plates"
        public class StackOfPlates<T>
        {
            private const int Capacity = 4;
            private int _current;
            private readonly List<Stack<T>> _rows = new List<Stack<T>>();

            public StackOfPlates()
            {
                _current = 0;
                _rows.Add(new Stack<T>());
            }

            public void Push(T item)
            {
                if (_rows[_current].Count >= Capacity)
                {
                    _rows.Add(new Stack<T>());
                    _current = 0;
                }
                _rows[_current].Push(item);
            }

            public T Pop()
            {
                if (_rows[_current].Count == 0)
                {
                    _rows.RemoveAt(_current);
                    _current -= 1;
                }
                return _rows[_current].Pop();
            }

            public T Peek()
            {
                return _rows[_current].Peek();
            }

            public bool IsEmpty()
            {
                return _current == 0 && _rows[0].Count == 0;
            }
        }
        #endregion

        #region 3.4
        // Implement a MyQueue class which implements a queue using two stacks
        public class MyQueue<T>
        {
            private enum State { Enqueuable, Dequeueable };

            private State _state;
            private readonly Stack<T> _enqueueable;
            private readonly Stack<T> _dequeueable;

            public MyQueue()
            {
                _state = State.Enqueuable;
                _enqueueable = new Stack<T>();
                _dequeueable = new Stack<T>();
            }

            public void Enqueue(T item)
            {
                if (_state != State.Enqueuable)
                {
                    while (_dequeueable.Count > 0)
                    {
                        _enqueueable.Push(_dequeueable.Pop());
                    }
                    _state = State.Enqueuable;
                }
                _enqueueable.Push(item);
            }

            public T Dequeue()
            {
                if (_state != State.Dequeueable)
                {
                    MakeDequeueable();
                }

                return _dequeueable.Pop();
            }

            public T Peek()
            {
                if (_state != State.Dequeueable)
                {
                    MakeDequeueable();
                }

                return _dequeueable.Peek();
            }

            public bool IsEmpty()
            {
                return _enqueueable.Count == 0 && _dequeueable.Count == 0;
            }

            private void MakeDequeueable()
            {
                while (_enqueueable.Count > 0)
                {
                    _dequeueable.Push(_enqueueable.Pop());
                }
                _state = State.Dequeueable;
            }
        }
        #endregion

        #region 3.5
        // Write a program to sort a stack such that the smallest items are on top.
        // You can use an additional temporary stack, but you may not copy the elements into any other data structure.
        public static void StackSort(Stack<int> stack)
        {
            var tempStack = new Stack<int>();

            while (stack.Count > 0)
            {
                int heldValue = stack.Pop();
                int pushed = 0;
                while (tempStack.Count > 0 && tempStack.Peek() >= heldValue)
                {
                    stack.Push(tempStack.Pop());
                    pushed++;
                }

                tempStack.Push(heldValue);

                for (int i = 0; i < pushed; i++)
                {
                    tempStack.Push(stack.Pop());
                }
            }

            while (tempStack.Count > 0)
            {
                stack.Push(tempStack.Pop());
            }
        }
        #endregion

        #region 3.6
        // An animal shelter, which holds only dogs and cats, operates on a structly "first in, first out" basis. People
        // must adopt either the "oldest" (based on arrival time) of all animals at the shelter, or they can select whether
        // they would prefer a dog or a cat (and will receive the oldest animal of that type). They cannot select which
        // specific animal they would like. Create the data structures to maintain this system and implement operations
        // such as enqueue, dequeueAny, dequeueDog, and dequeueCat. You may use the built-in LinkedList data structure.
        public class AnimalShelter
        {
            public enum Animal
            {
                Dog,
                Cat
            }

            private int _counter = 0;
            private readonly Queue<(string, int)> _catQueue = new Queue<ValueTuple<string, int>>();
            private readonly Queue<(string, int)> _dogQueue = new Queue<ValueTuple<string, int>>();

            public void Enqueue(Animal animal, string name)
            {
                if (animal == Animal.Cat)
                {
                    _catQueue.Enqueue((name, _counter));
                }
                else
                {
                    _dogQueue.Enqueue((name, _counter));
                }
                _counter++;
            }

            public string DequeueAny()
            {
                if (_catQueue.Count == 0)
                {
                    if (_dogQueue.Count == 0)
                    {
                        throw new InvalidOperationException();
                    }
                    return _dogQueue.Dequeue().Item1;
                }
                if (_dogQueue.Count == 0)
                {
                    return _catQueue.Dequeue().Item1;
                }

                (string catName, int catStamp) = _catQueue.Peek();
                (string dogName, int dogStamp) = _dogQueue.Peek();

                return catStamp < dogStamp ? _catQueue.Dequeue().Item1 : _dogQueue.Dequeue().Item1;
            }

            public string DequeueDog()
            {
                return _dogQueue.Dequeue().Item1;
            }

            public string DequeueCat()
            {
                return _catQueue.Dequeue().Item1;
            }
        }
        #endregion
    }
}
