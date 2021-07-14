using DeckofCards.Interfaces;
using DeckofCards.Models;
using DeckofCards.Models.Enums;
using DeckofCards.Service;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlowProject.Steps.DeckofCardsStepDefinitions
{
    [Binding]
    public class DeckofCardsSteps
    {
        private IDeckofCardsService _deckofCardsService;

        private IDeckService _deckService;

        private IList<Card> _card;

        private IList<Suit> _suitlist;

        private int _totaldeckCount;
        private int _faceValueforCard;

        [Given(@"a deck of cards")]
        public void GivenADeckofCards()
        {
            _deckService = new DeckService();
            _card = _deckService.GetCards();
            _deckofCardsService = new DeckofCardsService(_card);
        }

        [When(@"I count each card")]
        public void WhenICountEachCard()
        {
            _totaldeckCount = _deckofCardsService.GetTotalDeckCount();
        }

        [Then(@"I have a total of (.*) cards")]
        public void ThenIHaveATotalOfCards(int p0)
        {
            _totaldeckCount.Should().Be(p0);
        }

        [When(@"I check for suits")]
        public void WhenICheckForSuits()
        {
            _suitlist = _deckofCardsService.GetTotalDeckSuits();
        }

        [Then(@"I see hearts, clubs, spades, and diamonds")]
        public void ThenISeeHeartsClubsSpadesAndDiamonds()
        {
            List<Suit> suitsAvailable = GetAllSuits();
            bool allsuitsExist = suitsAvailable.All(x => _suitlist.Contains(x));
            _suitlist.Should().HaveCount(4);
            allsuitsExist.Should().BeTrue();
        }

        private static List<Suit> GetAllSuits()
        {
            return Enum.GetValues(typeof(Suit)).Cast<Suit>().ToList();
        }

        private static List<FaceNumber> GetAllFaceNumbers()
        {
            return Enum.GetValues(typeof(FaceNumber)).Cast<FaceNumber>().ToList();
        }

        [When(@"I count all the cards in a single suit")]
        public void WhenICountAllTheCardsInASingleSuit()
        {
            _card = _deckofCardsService.GetTotalDeckCardsWhenSuitIsProvided(Suit.Club);
        }

        [Then(@"I have (.*) cards ace, (.*), (.*), (.*), (.*), (.*), (.*), (.*), (.*), (.*), Jack, Queen, King")]
        public void ThenIHaveCardsAceJackQueenKing(int p0, int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8, int p9)
        {
            List<FaceNumber> cardsAvailable = GetAllFaceNumbers();
            bool allcardsExist = cardsAvailable.All(x => _card.Select(y => y.FaceNumber).Contains(x));
            _card.Should().HaveCount(13);
            allcardsExist.Should().BeTrue();
        }

        [When(@"I have a card with (.*)")]
        public void WhenIHaveACardWith(string p0)
        {
            _faceValueforCard = _deckofCardsService.GetCardFaceValue(p0);
        }

        [Then(@"the card is worth (.*)")]
        public void ThenTheCardIsWorth(string p0)
        {
            _faceValueforCard.Should().Be(int.Parse(p0));
        }

        [Then(@"the face cards are ordered Jack, Queen, King")]
        public void ThenTheFaceCardsAreOrderedJackQueenKing()
        {
            List<Card> sortedFaceList = _deckofCardsService.GetCardFaceValueforJacksQueensandKinginSortedList();

            sortedFaceList.Select(c => c.FaceNumber).Should().NotBeEmpty()
               .And.HaveCount(3)
               .And.ContainInOrder(new[] { FaceNumber.Jack, FaceNumber.Queen, FaceNumber.King });
        }
    }
}