using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static CtCI.ObjectOrientedDesign;

namespace CtCI.Tests
{
    [TestClass]
    public class ObjectOrientedDesignTests
    {
        [TestMethod]
        public void BlackjackTests()
        {
            var hand = new BlackjackHand
            {
                Cards = new List<Card>
                {
                    new Card(Suit.Clubs, FaceValue.Three),
                    new Card(Suit.Diamonds, FaceValue.Ace)
                }
            };

            Assert.IsFalse(hand.IsBusted);

            Assert.AreEqual(14, hand.StandardFaceValue);

            // Demeter!
            hand.Cards.Add(new Card(Suit.Hearts, FaceValue.Ace));

            Assert.IsFalse(hand.IsBusted);

            Assert.AreEqual(15, hand.StandardFaceValue);

            hand.Cards.Add(new Card(Suit.Spades, FaceValue.King));

            Assert.IsFalse(hand.IsBusted);
            Assert.AreEqual(15, hand.StandardFaceValue);

            hand.Cards.Add(new Card(Suit.Diamonds, FaceValue.Queen));
            Assert.IsTrue(hand.IsBusted);
            Assert.AreEqual(25, hand.StandardFaceValue);
        }

        [TestMethod]
        public void DeckOfCardsTests()
        {
            var deck = new DeckOfCards();

            var hand1 = new BlackjackHand
            {
                Cards = new List<Card> { deck.Draw(), deck.Draw() }
            };

            var hand2 = new BlackjackHand
            {
                Cards = new List<Card> { deck.Draw(), deck.Draw() }
            };

            Assert.IsFalse(deck.IsEmpty());

            Card top = deck.Peek();
            deck.Shuffle();
            Card newTop = deck.Peek();
            Assert.AreNotEqual(top, newTop);

            deck.Draw();
            Card newNewTop = deck.Peek();
            Assert.AreNotEqual(newTop, newNewTop);
        }

        [TestMethod]
        public void CircularArrayTests()
        {
            var circularArray = new CircularArray<int>(new []{1, 2, 3, 4});
            Assert.AreEqual(1, circularArray[0]);
            circularArray.Rotate(1);
            Assert.AreEqual(2, circularArray[0]);
            circularArray.Insert(0, 5);
            Assert.AreEqual(5, circularArray[0]);
            Assert.AreEqual(5, circularArray.Count);

            List<int> list = new List<int>();
            foreach (int item in circularArray)
            {
                list.Add(item);
            }

            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(5, list[0]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(3, list[2]);
            Assert.AreEqual(4, list[3]);
            Assert.AreEqual(1, list[4]);
        }
    }
}