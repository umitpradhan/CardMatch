using System;
using CardMatch.Core.Domain;

namespace CardMatch.Services.ScoreServices
{
    public sealed class ScoreService
    {
        private readonly ScoreCalculator _calculator;
        private readonly ComboTracker _comboTracker;

        private int _score;
        private float _remainingTime;

        public int CurrentScore => _score;
        public int CurrentCombo => _comboTracker.CurrentCombo;

        public event Action<int> OnScoreChanged;
        public event Action<int> OnComboChanged;

        public ScoreService(ScoreCalculator calculator, ComboTracker comboTracker)
        {
            _calculator = calculator;
            _comboTracker = comboTracker;
        }

        public void SetRemainingTime(float time)
        {
            _remainingTime = time;
        }

        public void RegisterMatch()
        {
            _comboTracker.RegisterMatch();

            int gained = _calculator.CalculateMatchScore(
                _comboTracker.CurrentCombo,
                _remainingTime);

            _score += gained;

            OnComboChanged?.Invoke(_comboTracker.CurrentCombo);
            OnScoreChanged?.Invoke(_score);
        }

        public void RegisterMismatch()
        {
            _comboTracker.RegisterMismatch();

            _score -= _calculator.CalculateMismatchPenalty();

            OnComboChanged?.Invoke(_comboTracker.CurrentCombo);
            OnScoreChanged?.Invoke(_score);
        }

        public void Reset()
        {
            _score = 0;
            _comboTracker.Reset();

            OnComboChanged?.Invoke(0);
            OnScoreChanged?.Invoke(0);
        }
    }
}