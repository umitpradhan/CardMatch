using System.Collections.Generic;

namespace CardMatch.Core.Domain
{
    public sealed class BoardModel
    {
        private readonly Dictionary<int, CardModel> _cards;

        public int Rows { get; }
        public int Columns { get; }

        public IReadOnlyCollection<CardModel> Cards => _cards.Values;

        public BoardModel(IEnumerable<CardModel> cards, int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            _cards = new Dictionary<int, CardModel>();

            foreach (var card in cards)
            {
                _cards.Add(card.Id, card);
            }
        }

        public CardModel GetCard(int id)
        {
            return _cards.TryGetValue(id, out var card) ? card : null;
        }

        public bool AllMatched()
        {
            foreach (var card in _cards.Values)
            {
                if (card.State != CardState.Matched)
                    return false;
            }

            return true;
        }

        public int TotalCards => _cards.Count;
    }
}