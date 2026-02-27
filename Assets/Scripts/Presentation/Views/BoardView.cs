using UnityEngine;
using System.Collections.Generic;
using CardMatch.Core.Domain;
using CardMatch.Presentation.Pooling;
using CardMatch.Presentation.Layout;

namespace CardMatch.Presentation.Views
{
    public sealed class BoardView : MonoBehaviour
    {
        [SerializeField] private CardView _cardPrefab;
        [SerializeField] private GridAutoScaler _gridScaler;

        private CardViewPool _pool;
        private readonly Dictionary<int, CardView> _activeViews = new();

        private void Awake()
        {
            _pool = new CardViewPool(_cardPrefab, transform);
        }

        public void Generate(BoardModel board, System.Action<int> onCardSelected)
        {
            Clear();

            _gridScaler.Configure(board.Rows, board.Columns);

            foreach (var card in board.Cards)
            {
                var view = _pool.Get();
                view.Initialize(card, onCardSelected);
                _activeViews.Add(card.Id, view);
            }
        }

        public CardView GetView(int cardId)
        {
            return _activeViews.TryGetValue(cardId, out var view) ? view : null;
        }

        private void Clear()
        {
            foreach (var view in _activeViews.Values)
            {
                _pool.Release(view);
            }

            _activeViews.Clear();
        }
    }
}