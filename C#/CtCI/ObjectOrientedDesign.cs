using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CtCI
{
    public class ObjectOrientedDesign
    {
        #region 7.1
        /* Design the data structures for a generic deck of cards.
         * Explain how you would subclass the data structures to implement blackjack. */
        public enum Suit
        {
            Hearts,
            Diamonds,
            Clubs,
            Spades
        }

        public enum FaceValue
        {
            Ace,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        }

        public class Card
        {
            public Card(Suit suit, FaceValue faceValue)
            {
                Suit = suit;
                FaceValue = faceValue;
            }

            public Suit Suit { get; }
            public FaceValue FaceValue { get; }
        }

        public class HandOfCards
        {
            public List<Card> Cards { get; set; }

            protected readonly Dictionary<FaceValue, int> CardValues = new Dictionary<FaceValue, int>
            {
                {FaceValue.Ace, 1},
                {FaceValue.Two, 2},
                {FaceValue.Three, 3},
                {FaceValue.Four, 4},
                {FaceValue.Five, 5},
                {FaceValue.Six, 6},
                {FaceValue.Seven, 7},
                {FaceValue.Eight, 8},
                {FaceValue.Nine, 9},
                {FaceValue.Ten, 10},
                {FaceValue.Jack, 10},
                {FaceValue.Queen, 10},
                {FaceValue.King, 10}
            };

            public virtual int StandardFaceValue
            {
                get
                {
                    return Cards.Sum(card => CardValues[card.FaceValue]);
                }
            }
        }

        public class BlackjackHand : HandOfCards
        {
            public IEnumerable<int> SortedPossibleValues
            {
                get
                {
                    int standardValue = base.StandardFaceValue;

                    bool hasAces = Cards.Any(o => o.FaceValue == FaceValue.Ace);
                    yield return standardValue;
                    if (hasAces && standardValue <= 11) yield return standardValue + 10;
                }
            }

            public bool IsBusted => SortedPossibleValues.First() > 21;

            public override int StandardFaceValue => SortedPossibleValues.Last();
        }

        public class DeckOfCards
        {
            private readonly Random _random = new Random();
            private readonly List<Card> _deck = new List<Card>();

            public DeckOfCards()
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (FaceValue value in Enum.GetValues(typeof(FaceValue)))
                    {
                        _deck.Add(new Card(suit, value));
                    }
                }

                // Little wasteful
                Shuffle();
            }

            public void Shuffle()
            {
                for (int i = 0; i < _deck.Count; i++)
                {
                    // Random.Next's "max value" is never reached!
                    int indexToSwap = _random.Next(i, _deck.Count);
                    Card cardToSwap = _deck[indexToSwap];
                    _deck[indexToSwap] = _deck[i];
                    _deck[i] = cardToSwap;
                }
            }

            public bool IsEmpty() => _deck.Count == 0;

            public Card Draw()
            {
                if (IsEmpty()) throw new InvalidOperationException("Deck is empty");

                Card card = _deck[_deck.Count - 1];
                _deck.RemoveAt(_deck.Count - 1);

                return card;
            }

            public Card Peek() => _deck[_deck.Count - 1];
        }
        #endregion

        #region 7.9
        /* Implement a CircularArray class that supports an array-like data structure which
         * can be efficiently rotated. If possible, the class should use a generic type
         * (also called a template), and should support iteration via the standard for
         * (Obj o : circularArray) notation. */
        public class CircularArray<T> : IEnumerable<T>
        {
            private readonly List<T> _list;
            private int _position = 0;

            private int RotatedIndex(int index) => (index + _position) % _list.Count;

            public CircularArray()
            {
                _list = new List<T>();
            }

            public CircularArray(IEnumerable<T> items)
            {
                _list = new List<T>(items);
            }

            public void Rotate(int positions)
            {
                _position = (_position + positions) % _list.Count;
            }

            public void Insert(int index, T item)
            {
                int rotatedIndex = RotatedIndex(index);
                if (rotatedIndex < _position)
                {
                    _position++;
                }
                _list.Insert(rotatedIndex, item);
            }

            public void Add(T item)
            {
                Insert(_position, item);
                _position++;
            }

            public void RemoveAt(int index)
            {
                int rotatedIndex = RotatedIndex(index);
                if (rotatedIndex < _position)
                {
                    _position--;
                }
                _list.RemoveAt(rotatedIndex);
            }

            public T this[int index]
            {
                get { return _list[RotatedIndex(index)]; }
                set
                {
                    Insert(index, value);
                }
            }

            public int Count => _list.Count;

            public IEnumerator<T> GetEnumerator()
            {
                return new CircularArrayEnumerator<T>(this);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class CircularArrayEnumerator<T> : IEnumerator<T>
        {
            private int _currentPosition;
            private readonly CircularArray<T> _circularArray;

            public CircularArrayEnumerator(CircularArray<T> array)
            {
                _circularArray = array;
                _currentPosition = -1;
            }

            /// <summary>Advances the enumerator to the next element of the collection.</summary>
            /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
            /// <filterpriority>2</filterpriority>
            public bool MoveNext()
            {
                bool first = _currentPosition == -1;

                _currentPosition = (_currentPosition + 1) % _circularArray.Count;

                return first || _currentPosition != 0;
            }

            public void Reset()
            {
                _currentPosition = -1;
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                // Nothing
            }

            public T Current => _circularArray[_currentPosition];
        }
        #endregion
    }
}