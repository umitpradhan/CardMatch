using UnityEngine;
using CardMatch.Core.Domain;

namespace CardMatch.Presentation.Views
{
    public sealed class BoardController : MonoBehaviour
    {
        [SerializeField] private BoardView _boardView;

        private BoardModel _board;

        public void Initialize(BoardModel board)
        {
            _board = board;
            _boardView.Generate(_board, OnCardSelected);
        }

        private void OnCardSelected(int cardId)
        {
            var card = _board.GetCard(cardId);

            if (card == null || !card.CanFlip)
                return;

            card.FlipUp();

            var view = _boardView.GetView(cardId);
            view.PlayFlipUp();

        }
    }
}