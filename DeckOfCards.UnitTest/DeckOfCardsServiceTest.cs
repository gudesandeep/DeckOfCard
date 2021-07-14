using DeckofCards.Interfaces;
using DeckofCards.Models;
using DeckofCards.Models.Enums;
using DeckofCards.Service;
using DeckOfCards.UnitTest.Mocking;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DeckofCards.UnitTest
{
    [TestFixture]
    public class DeckofCardsServiceTest
    {
        private IDeckofCardsService _deckofCardsService;

        private IDeckService _deckService;

        private IList<Card> _card;

        private MockDataForTestClass _mockDataForTestClass;

        public DeckofCardsServiceTest()
        {
            _deckService = new DeckService();
            _card = _deckService.GetCards();
            _deckofCardsService = new DeckofCardsService(_card);
            _mockDataForTestClass = new MockDataForTestClass();
        }

        [Test]
        public void ScenarioWhereTotalDeckisCounted_returnsTotalDeckCount_52()
        {
            int count = 52;
            int totalDeckCount = _deckofCardsService.GetTotalDeckCount();
            Assert.That(totalDeckCount == count);
        }

        [Test]
        public void ScenarioWhereTotalDeckSuitsisCounted_returnsTotalSuitsCount_4()
        {
            int count = 4;
            IList<Suit> totalDeckSuits = _deckofCardsService.GetTotalDeckSuits();
            Assert.That(totalDeckSuits.Count == count);
        }

        [Test]
        public void ScenarioWhereTotalDeckSuitsisCounted_returnsTotalSuitsList()
        {
            List<Suit> suitsAvailable = _mockDataForTestClass.GetAllSuits();
            IList<Suit> totalDeckSuits = _deckofCardsService.GetTotalDeckSuits();
            bool allsuitsExist = totalDeckSuits.All(x => suitsAvailable.Contains(x));

            Assert.IsTrue(allsuitsExist);
        }

        [Test]
        public void ScenarioWhereAnySuitisCounted_returnsTotalSuitCount13()
        {
            IList<Card> singleDeckSuit = GetSingleDeckCard();
            Assert.That(singleDeckSuit.Count == 13);
        }

        [Test]
        public void ScenarioWhereAnySuitisCounted_returnsTotalSuitList()
        {
            List<FaceNumber> cardsAvailable = _mockDataForTestClass.GetAllFaceNumbers();
            IList<Card> singleDeckSuit = GetSingleDeckCard();
            bool allcardsExist = cardsAvailable.All(x => singleDeckSuit.Select(y => y.FaceNumber).Contains(x));

            Assert.IsTrue(allcardsExist);
        }

        [Test]
        [TestCase("Ace", 1)]
        [TestCase("Jack", 10)]
        [TestCase("Queen", 10)]
        [Parallelizable(ParallelScope.All)]
        public void ScenarioWhereFaceValueisprovider_returnsCardValue(string cardValue, int expectedFaceValue)
        {
            int actualFaceValue = _deckofCardsService.GetCardFaceValue(cardValue);
            Assert.That(actualFaceValue == expectedFaceValue);
        }

        [Test]
        public void ScenarioWhereJacksQueensandKingsareSorted_returnsSortedFaceValue()
        {
            List<FaceNumber> expectedfaceNumberList = new List<FaceNumber> { FaceNumber.Jack, FaceNumber.Queen, FaceNumber.King };

            List<FaceNumber> ActualfaceNumberList = _deckofCardsService.GetCardFaceValueforJacksQueensandKinginSortedList().Select(x => x.FaceNumber).ToList();

            Assert.That(expectedfaceNumberList, Is.EquivalentTo(ActualfaceNumberList));
        }
        
        private IList<Card> GetSingleDeckCard()
        {
            return _deckofCardsService.GetTotalDeckCardsWhenSuitIsProvided(Suit.Club);
        }
    }
}