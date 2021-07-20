using DeckofCards.Interfaces;
using DeckofCards.Models;
using DeckofCards.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeckofCards.Service
{
    public class DeckService : IDeckService
    {
        public IList<Card> Cards { get; set; }

        public IList<Card> GetCards()
        {
            Cards = Enumerable.Range(1, 4)
                .SelectMany(s => Enum.GetValues(typeof(FaceNumber)).Cast<FaceNumber>().Select
                                    (c => new Card()
                                    {
                                        Suit = (Suit)s,
                                        FaceNumber = c
                                    }
                                            )
                            )
                   .ToList();
            return Cards;
        }

        public string test()
        {
            return "hi";
        }
    }
}