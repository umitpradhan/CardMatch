using UnityEngine;
using CardMatch.Core.Domain;
using CardMatch.Services.ScoreServices;
using CardMatch.Services.AudioServices;
using CardMatch.Presentation.UI;

namespace CardMatch.Core.GameFlow
{
    public sealed class GameFlowController : MonoBehaviour
    {
        [SerializeField] private AudioService _audioService;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private ComboView _comboView;
        [SerializeField] private GameOverView _gameOverView;

        private ScoreService _scoreService;
        private GameSession _session;

        public void Initialize(BoardModel board, TurnController turnController)
        {
            var scoreConfig = new ScoreConfig();
            var calculator = new ScoreCalculator(scoreConfig);
            var combo = new ComboTracker();

            _scoreService = new ScoreService(calculator, combo);

            _scoreView.Bind(_scoreService);
            _comboView.Bind(_scoreService);

            _session = new GameSession(board, turnController);

            HookTurnController(turnController);
        }

        private void HookTurnController(TurnController turnController)
        {
            turnController.OnMatch += HandleMatch;
            turnController.OnMismatch += HandleMismatch;
        }

        private void HandleMatch()
        {
            _scoreService.RegisterMatch();
            _audioService.Play(AudioEvent.Match);

            if (_session.IsGameOver())
            {
                _audioService.Play(AudioEvent.GameOver);
                _gameOverView.Show();
            }
        }

        private void HandleMismatch()
        {
            _scoreService.RegisterMismatch();
            _audioService.Play(AudioEvent.Mismatch);
        }
    }
}