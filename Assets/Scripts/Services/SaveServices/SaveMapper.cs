using System.Collections.Generic;
using CardMatch.Core.Domain;

namespace CardMatch.Services.SaveServices
{
    public static class SaveMapper
    {
        public static GameSaveData ToSaveData(
            BoardModel board,
            int score,
            int combo,
            float remainingTime,
            int seed)
        {
            var data = new GameSaveData
            {
                Rows = board.Rows,
                Columns = board.Columns,
                Seed = seed,
                CurrentScore = score,
                CurrentCombo = combo,
                RemainingTime = remainingTime,
                Cards = new List<CardSaveData>()
            };

            foreach (var card in board.Cards)
            {
                data.Cards.Add(new CardSaveData
                {
                    Id = card.Id,
                    PairId = card.PairId,
                    State = card.State
                });
            }

            return data;
        }

        public static BoardModel ToBoardModel(GameSaveData data)
        {
            var cards = new List<CardModel>();

            foreach (var c in data.Cards)
            {
                var card = new CardModel(c.Id, c.PairId);

                if (c.State == CardState.Revealed)
                    card.FlipUp();
                else if (c.State == CardState.Matched)
                {
                    card.FlipUp();
                    card.MarkMatched();
                }

                cards.Add(card);
            }

            return new BoardModel(cards, data.Rows, data.Columns);
        }
    }
}