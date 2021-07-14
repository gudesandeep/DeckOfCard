using DeckofCards.Interfaces;
using DeckofCards.Models;
using DeckofCards.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeckofCards.Service
{
    public class DeckofCardsService : IDeckofCardsService
    {
        private IList<Card> _cards;

        public DeckofCardsService(IList<Card> Cards)
        {
            _cards = Cards;
        }

        public int GetTotalDeckCount()
        {
            return _cards.Count;
        }

        public IList<Suit> GetTotalDeckSuits()
        {
            return _cards.GroupBy(c => c.Suit).Select(x => x.Key).ToList();
        }

        public IList<Card> GetTotalDeckCardsWhenSuitIsProvided(Suit suit)
        {
            return _cards.Where(x => x.Suit == suit).ToList();
        }

        public int GetCardFaceValue(string cardValue)
        {
            FaceNumber FaceNumber = (FaceNumber)Enum.Parse(typeof(FaceNumber), cardValue);

            return facenumberDictionary[FaceNumber];
        }

        public List<Card> GetCardFaceValueforJacksQueensandKinginSortedList()
        {
            var sortedList = _cards.Where(x => (int)x.FaceNumber > 10 && x.Suit == Suit.Club)
      .OrderBy(x => (int)(x.FaceNumber))
      .ToList();

            return sortedList;
        }

        private Dictionary<FaceNumber, int> facenumberDictionary = new Dictionary<FaceNumber, int>()
        {
            [FaceNumber.Ace] = 1,
            [FaceNumber.Two] = 2,
            [FaceNumber.Three] = 3,
            [FaceNumber.Four] = 4,
            [FaceNumber.Five] = 5,
            [FaceNumber.Six] = 6,
            [FaceNumber.Seven] = 7,
            [FaceNumber.Eight] = 8,
            [FaceNumber.Nine] = 9,
            [FaceNumber.Ten] = 10,
            [FaceNumber.Jack] = 10,
            [FaceNumber.Queen] = 10,
            [FaceNumber.King] = 10,
        };
    }
}