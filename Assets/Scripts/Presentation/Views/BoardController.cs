using UnityEngine;
using CardMatch.Core.Domain;
using CardMatch.Core.GameFlow;

namespace CardMatch.Presentation.Views
{
    public sealed class BoardController : MonoBehaviour
    {
        [SerializeField] private BoardView _boardView;
        [SerializeField] private Infrastructure.CoroutineRunner _coroutineRunner;

        private GameSession _session;

        public void Initialize(BoardModel board)
        {
            var resolver = new MatchResolver();
            var comboTracker = new ComboTracker();
            var scoreConfig = new ScoreConfig();
            var scoreCalculator = new ScoreCalculator(scoreConfig);

            var turnController = new TurnController(
                board,
                resolver,
                scoreCalculator,
                comboTracker,
                _coroutineRunner,
                _boardView);

            _session = new GameSession(board, turnController);

            _boardView.Generate(board, OnCardSelected);
        }

        private void OnCardSelected(int cardId)
        {
            var card = _session.Board.GetCard(cardId);

            if (card == null)
                return;

            var view = _boardView.GetView(cardId);

            if (!card.CanFlip)
                return;

            view.PlayFlipUp();

            _session.TurnController.OnCardFlipped(card);
        }
    }
}