using DeckofCards.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeckOfCards.UnitTest.Mocking
{
    public class MockDataForTestClass
    {
        public List<FaceNumber> GetAllFaceNumbers()
        {
            return Enum.GetValues(typeof(FaceNumber)).Cast<FaceNumber>().ToList();
        }

        public List<Suit> GetAllSuits()
        {
            return Enum.GetValues(typeof(Suit)).Cast<Suit>().ToList();
        }
    }
}