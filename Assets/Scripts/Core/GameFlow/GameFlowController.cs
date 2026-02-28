using UnityEngine;
using CardMatch.Core.Domain;
using CardMatch.Services.ScoreServices;
using CardMatch.Services.AudioServices;
using CardMatch.Services.SaveServices;
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
        private SaveService _saveService;
        private int _seed;
        private float _remainingTime;

        public void Initialize(BoardModel board, TurnController turnController, int seed)
        {
            var scoreConfig = new ScoreConfig();
            var calculator = new ScoreCalculator(scoreConfig);
            var combo = new ComboTracker();

            _scoreService = new ScoreService(calculator, combo);

            _scoreView.Bind(_scoreService);
            _comboView.Bind(_scoreService);

            _session = new GameSession(board, turnController);

            _saveService = new SaveService();
            _seed = seed;

            HookTurnController(turnController);
        }

        private void HookTurnController(TurnController turnController)
        {
            turnController.OnMatch += HandleMatch;
            turnController.OnMismatch += HandleMismatch;
        }

        private void AutoSave()
        {
            var data = SaveMapper.ToSaveData(
                _session.Board,
                _scoreService.CurrentScore,
                _scoreService.CurrentCombo,
                _remainingTime,
                _seed);

            _saveService.Save(data);
        }

        private void HandleMatch()
        {
            _scoreService.RegisterMatch();
            _audioService.Play(AudioEvent.Match);

            AutoSave();

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

            AutoSave();
        }
    }
}