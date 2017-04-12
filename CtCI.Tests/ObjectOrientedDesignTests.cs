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
    }
}