using System;
using System.Collections.Generic;

namespace CardMatch.Core.Domain
{
    public sealed class BoardGenerator
    {
        public IEnumerable<CardModel> Generate(int rows, int columns, int seed)
        {
            int totalCards = rows * columns;

            if (totalCards % 2 != 0)
                throw new ArgumentException("Board must contain even number of cards.");

            int pairCount = totalCards / 2;

            var rng = new Random(seed);
            var pairIds = new List<int>(totalCards);

            // Create pair ids
            for (int i = 0; i < pairCount; i++)
            {
                pairIds.Add(i);
                pairIds.Add(i);
            }

            // Fisher-Yates shuffle
            for (int i = pairIds.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (pairIds[i], pairIds[j]) = (pairIds[j], pairIds[i]);
            }

            for (int i = 0; i < totalCards; i++)
            {
                yield return new CardModel(i, pairIds[i]);
            }
        }
    }
}