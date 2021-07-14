using DeckofCards.Models;
using System.Collections.Generic;

namespace DeckofCards.Interfaces
{
    public interface IDeckService
    {
        IList<Card> GetCards();
    }
}