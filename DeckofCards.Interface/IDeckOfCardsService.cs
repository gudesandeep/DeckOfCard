using DeckofCards.Models;
using DeckofCards.Models.Enums;
using System.Collections.Generic;

namespace DeckofCards.Interfaces
{
    public interface IDeckofCardsService
    {
        int GetTotalDeckCount();

        IList<Suit> GetTotalDeckSuits();

        IList<Card> GetTotalDeckCardsWhenSuitIsProvided(Suit suit);

        int GetCardFaceValue(string cardValue);

        List<Card> GetCardFaceValueforJacksQueensandKinginSortedList();
    }
}